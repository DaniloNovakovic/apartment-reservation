using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentReservation.Infrastructure.Replicators
{
    public class UserReplicatorService : BackgroundService
    {
        private readonly ILogger<UserReplicatorService> _logger;
        private readonly DbReplicationSettings _settings;

        public UserReplicatorService(IOptions<DbReplicationSettings> settings,
                                         ILogger<UserReplicatorService> logger)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"{nameof(UserReplicatorService)} is starting.");

            const string backgroundTaskName = "UserReplicator";

            stoppingToken.Register(() =>
                _logger.LogDebug($" {backgroundTaskName} background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"{backgroundTaskName} task doing background work.");

                await SyncUsersAsync(stoppingToken);

                await Task.Delay(_settings.CheckUsersUpdateTimeMs, stoppingToken);
            }

            _logger.LogDebug($"{backgroundTaskName} background task is stopping.");
        }

        /// <summary>
        /// Synchronizes Users table from SQL (Command Model) to noSQL (Query Model)
        /// </summary>
        private async Task SyncUsersAsync(CancellationToken stoppingToken)
        {
            // TODO: Sync User table from SQL to NoSQL 

            await Task.Delay(_settings.CheckUsersUpdateTimeMs, stoppingToken);
        }
    }
}
