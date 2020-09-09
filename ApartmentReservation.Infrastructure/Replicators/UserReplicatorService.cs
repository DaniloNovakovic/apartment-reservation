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
    public class UserReplicatorService : BackgroundService
    {
        private readonly ILogger<UserReplicatorService> _logger;
        private readonly DbReplicationSettings _settings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserReplicatorService(
            IOptions<DbReplicationSettings> settings,
            ILogger<UserReplicatorService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _settings = settings.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"{nameof(UserReplicatorService)} is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug("Closing service..."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Syncing Users...");

                await SyncUsersAsync(stoppingToken);

                await Task.Delay(_settings.CheckUsersUpdateTimeMs, stoppingToken);
            }

            _logger.LogDebug("Closing background task...");
        }

        /// <summary>
        /// Synchronizes Users table from SQL (Command Model) to noSQL (Query Model)
        /// </summary>
        private async Task SyncUsersAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IApartmentReservationDbContext>();
                var queryDb = scope.ServiceProvider.GetRequiredService<IQueryDbContext>();

                var dbUsers = await db.Users
                    .Where(u => u.IsSyncNeeded)
                    .Take(_settings.MaxNumberOfEntitiesPerReplication)
                    .ToListAsync(stoppingToken);

                var tasksToAwait = new List<Task>();

                foreach (var dbUser in dbUsers)
                {
                    Task task = SyncUserAsync(queryDb, dbUser, stoppingToken);

                    tasksToAwait.Add(task);

                    dbUser.IsSyncNeeded = false;
                }

                tasksToAwait.Add(db.SaveChangesAsync(stoppingToken));

                await Task.WhenAll(tasksToAwait).ConfigureAwait(false);
            }
        }

        private Task SyncUserAsync(IQueryDbContext queryDb, Domain.Entities.User dbUser, CancellationToken stoppingToken)
        {
            var replacement = MapToQueryModel(dbUser);
            var filter = Builders<UserModel>.Filter.Eq(u => u.Id, replacement.Id);

            if (dbUser.IsDeleted)
            {
                return queryDb.Users.DeleteOneAsync(filter, stoppingToken);
            }

            var options = new ReplaceOptions() { IsUpsert = true };
            return queryDb.Users.ReplaceOneAsync(filter, replacement, options, stoppingToken);
        }

        private UserModel MapToQueryModel(Domain.Entities.User dbUser)
        {
            return new UserModel
            {
                Id = dbUser.Id,
                Banned = dbUser.IsBanned,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Gender = dbUser.Gender,
                Password = dbUser.Password,
                RoleName = dbUser.RoleName,
                Username = dbUser.Username
            };
        }
    }
}
