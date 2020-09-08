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
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Infrastructure.Replicators
{
    public class ReservationReplicationService : BackgroundService
    {
        private readonly ILogger<ReservationReplicationService> _logger;
        private readonly DbReplicationSettings _settings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReservationReplicationService(
            IOptions<DbReplicationSettings> settings,
            ILogger<ReservationReplicationService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _settings = settings.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"{nameof(ReservationReplicationService)} is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug("Closing service..."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Syncing Reservations...");

                await SyncReservationsAsync(stoppingToken);

                await Task.Delay(_settings.CheckReservationsUpdateTimeMs, stoppingToken);
            }

            _logger.LogDebug("Closing background task...");
        }

        /// <summary>
        /// Synchronizes Reservations table from SQL (Command Model) to noSQL (Query Model)
        /// </summary>
        private async Task SyncReservationsAsync(CancellationToken stoppingToken)
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IApartmentReservationDbContext>();
                var queryDb = scope.ServiceProvider.GetRequiredService<IQueryDbContext>();

                var dbReservations = await db.Reservations
                    .Include(r => r.Guest).ThenInclude(g => g.User)
                    .Include(r => r.Apartment).ThenInclude(a => a.Host).ThenInclude(h => h.User)
                    .Where(r => r.IsSyncNeeded)
                    .Take(_settings.MaxNumberOfEntitiesPerReplication)
                    .ToListAsync(stoppingToken);

                var tasksToAwait = new List<Task>();

                foreach (var dbReservation in dbReservations)
                {
                    Task task = SyncReservationAsync(queryDb, dbReservation, stoppingToken);

                    tasksToAwait.Add(task);

                    dbReservation.IsSyncNeeded = false;
                }

                tasksToAwait.Add(db.SaveChangesAsync(stoppingToken));

                await Task.WhenAll(tasksToAwait).ConfigureAwait(false);
            }
        }

        private Task SyncReservationAsync(IQueryDbContext queryDb, Domain.Entities.Reservation dbReservation, CancellationToken stoppingToken)
        {
            var replacement = MapToQueryModel(dbReservation);
            var filter = Builders<ReservationModel>.Filter.Eq(u => u.Id, replacement.Id);

            if (dbReservation.IsDeleted)
            {
                return queryDb.Reservations.DeleteOneAsync(filter, stoppingToken);
            }

            var options = new ReplaceOptions() { IsUpsert = true };
            return queryDb.Reservations.ReplaceOneAsync(filter, replacement, options, stoppingToken);
        }

        private ReservationModel MapToQueryModel(Domain.Entities.Reservation dbReservation)
        {
            return new ReservationModel
            {
                Id = dbReservation.Id,
                ApartmentId = dbReservation.Apartment.Id,
                ApartmentTitle = dbReservation.Apartment.Title,
                GuestId = dbReservation.Guest.UserId,
                GuestUsername = dbReservation.Guest.User.Username,
                HostId = dbReservation.Apartment.Host.UserId,
                HostUsername = dbReservation.Apartment.Host.User.Username,
                NumberOfNightsRented = dbReservation.NumberOfNightsRented,
                ReservationStartDate = dbReservation.ReservationStartDate,
                ReservationState = dbReservation.ReservationState,
                TotalCost = dbReservation.TotalCost
            };
        }
    }
}
