using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Infrastructure.Replicators
{
    public class ApartmentReplicationService : BackgroundService
    {
        private ILogger<ApartmentReplicationService> _logger;
        private DbReplicationSettings _settings;
        private IServiceScopeFactory _serviceScopeFactory;

        public ApartmentReplicationService(
            IOptions<DbReplicationSettings> settings,
            ILogger<ApartmentReplicationService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _settings = settings.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"{nameof(ApartmentReplicationService)} is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug("Closing service..."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Syncing Apartments...");

                await SyncApartmentsAsync(stoppingToken);

                await Task.Delay(_settings.CheckApartmentsUpdateTimeMs, stoppingToken);
            }

            _logger.LogDebug("Closing background task...");
        }

        private async Task SyncApartmentsAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IApartmentReservationDbContext>();
                var queryDb = scope.ServiceProvider.GetRequiredService<IQueryDbContext>();

                var dbApartments = await db.Apartments
                    .Include("ApartmentAmenities.Amenity")
                    .Include(a => a.ForRentalDates)
                    .Include(a => a.Images)
                    .Include(a => a.Location).ThenInclude(l => l.Address)
                    .Include(a => a.Host).ThenInclude(h => h.User)
                    .Include(a => a.Comments).ThenInclude(c=>c.Guest).ThenInclude(c=>c.User)
                    .Where(a => a.IsSyncNeeded)
                    .Take(_settings.CheckApartmentsUpdateTimeMs)
                    .ToListAsync(stoppingToken);

                var tasksToAwait = new List<Task>();

                foreach (var dbApartment in dbApartments)
                {
                    Task task = SyncApartmentAsync(queryDb, dbApartment, stoppingToken);

                    tasksToAwait.Add(task);

                    dbApartment.IsSyncNeeded = false;
                }

                tasksToAwait.Add(db.SaveChangesAsync(stoppingToken));

                await Task.WhenAll(tasksToAwait).ConfigureAwait(false);
            }
        }

        private Task SyncApartmentAsync(IQueryDbContext queryDb, Domain.Entities.Apartment dbApartment, CancellationToken stoppingToken)
        {
            var filter = Builders<ApartmentModel>.Filter.Eq(u => u.Id, dbApartment.Id);

            if (dbApartment.IsDeleted)
            {
                return queryDb.Apartments.DeleteOneAsync(filter, stoppingToken);
            }

            var replacement = MapToQueryModel(dbApartment);
            var options = new ReplaceOptions() { IsUpsert = true };
            return queryDb.Apartments.ReplaceOneAsync(filter, replacement, options, stoppingToken);
        }

        #region Mapping
        private ApartmentModel MapToQueryModel(Domain.Entities.Apartment dbApartment)
        {
            var amenities = dbApartment.Amenities.Where(a=>!a.IsDeleted).Select(MapAmenity).ToArray();
            var availableDates = dbApartment.GetAvailableDates();
            var forRentalDates = dbApartment.ForRentalDates.Where(frd=>!frd.IsDeleted).Select(d => d.Date).ToArray();
            var images = dbApartment.Images.Where(i=>!i.IsDeleted).Select(MapImage).ToArray();
            var location = MapLocation(dbApartment.Location);
            var comments = dbApartment.Comments.Where(c => !c.IsDeleted).Select(MapComment).ToArray();
            var rating = comments.DefaultIfEmpty(new CommentModel { Rating = 0 }).Average(c=>(double)c.Rating);

            return new ApartmentModel
            {
                Id = dbApartment.Id,
                HostId = dbApartment.HostId,
                IsHostBanned = dbApartment.Host.User.IsBanned,
                ActivityState = dbApartment.ActivityState,
                ApartmentType = dbApartment.ApartmentType,
                CheckInTime = dbApartment.CheckInTime,
                CheckOutTime = dbApartment.CheckOutTime,
                NumberOfGuests = dbApartment.NumberOfGuests,
                NumberOfRooms = dbApartment.NumberOfRooms,
                PricePerNight = dbApartment.PricePerNight,
                Title = dbApartment.Title,
                Amenities = amenities,
                Comments = comments,
                AvailableDates = availableDates,
                ForRentalDates = forRentalDates,
                Images = images,
                Location = location,
                Rating = rating
            };
        }


        private LocationModel MapLocation(Domain.Entities.Location location)
        {
            return new LocationModel
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Address = MapAddress(location.Address)
            };
        }

        private static AddressModel MapAddress(Domain.Entities.Address address)
        {
            return new AddressModel
            {
                CityName = address.CityName,
                CountryName = address.CountryName,
                StreetName = address.StreetName,
                StreetNumber = address.StreetNumber,
                PostalCode = address.PostalCode
            };
        }

        private static ImageModel MapImage(Domain.Entities.Image i)
        {
            return new ImageModel { Id = i.Id, Uri = i.ImageUri };
        }

        private static CommentModel MapComment(Domain.Entities.Comment c)
        {
            return new CommentModel
            {
                Id = c.Id,
                Approved = c.Approved,
                GuestId = c.Guest.UserId,
                GuestUsername = c.Guest.User.Username,
                Rating = c.Rating,
                Text = c.Text
            };
        }

        private static AmenityModel MapAmenity(Domain.Entities.Amenity a)
        {
            return new AmenityModel { Id = a.Id, Name = a.Name };
        }
        #endregion Mapping
    }
}
