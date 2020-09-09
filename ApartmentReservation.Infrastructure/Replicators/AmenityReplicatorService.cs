using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Domain.Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Infrastructure.Replicators
{
    public class AmenityReplicatorService : BackgroundService
    {
        private readonly ILogger<AmenityReplicatorService> _logger;
        private readonly DbReplicationSettings _settings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AmenityReplicatorService(
            IOptions<DbReplicationSettings> settings,
            ILogger<AmenityReplicatorService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _settings = settings.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"{nameof(AmenityReplicatorService)} is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug("Closing service..."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Syncing Amenities...");

                await SyncAmenitiesAsync(stoppingToken);

                await Task.Delay(_settings.CheckAmenitiesUpdateTimeMs, stoppingToken);
            }

            _logger.LogDebug("Closing background task...");
        }

        /// <summary>
        /// Synchronizes Amenities table from SQL (Command Model) to noSQL (Query Model)
        /// </summary>
        private async Task SyncAmenitiesAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IApartmentReservationDbContext>();
                var queryDb = scope.ServiceProvider.GetRequiredService<IQueryDbContext>();

                var dbAmenities = await db.Amenities
                    .Where(u => u.IsSyncNeeded)
                    .Take(_settings.MaxNumberOfEntitiesPerReplication)
                    .ToListAsync(stoppingToken);

                var tasksToAwait = new List<Task>();

                foreach (var dbAmenity in dbAmenities)
                {
                    Task task = SyncAmenityAsync(queryDb, dbAmenity, stoppingToken);

                    tasksToAwait.Add(task);

                    dbAmenity.IsSyncNeeded = false;
                }

                tasksToAwait.Add(db.SaveChangesAsync(stoppingToken));

                await Task.WhenAll(tasksToAwait).ConfigureAwait(false);
            }
        }

        private Task SyncAmenityAsync(IQueryDbContext queryDb, Domain.Entities.Amenity dbAmenity, CancellationToken stoppingToken)
        {
#pragma warning disable CS0251 // Indexing an array with a negative index (Justification: Element at negative index translates to $ - has special meaning to MongoDB.Driver)

            var filter = Builders<ApartmentModel>.Filter.ElemMatch(a => a.Amenities, amenity => amenity.Id == dbAmenity.Id);
            var update = Builders<ApartmentModel>.Update.Set(a => a.Amenities[-1].Name, dbAmenity.Name);

            if (dbAmenity.IsDeleted)
            {
                update = Builders<ApartmentModel>.Update.PullFilter(a => a.Amenities, amenity => amenity.Id == dbAmenity.Id);
            }

            return queryDb.Apartments.UpdateManyAsync(filter, update);

#pragma warning restore CS0251
        }
    }
}
