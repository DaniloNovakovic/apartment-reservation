using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        private Task SyncApartmentsAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
