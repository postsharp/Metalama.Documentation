using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.CustomFramework
{
    public class Worker : BackgroundService
    {
        [Log]
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                LoggerExtensions.LogWarning(this._logger, $"Worker.ExecuteAsync(CancellationToken) started.");
                Console.WriteLine("Hello, world.");
                return Task.CompletedTask;
            }
            finally
            {
                LoggerExtensions.LogWarning(this._logger, $"Worker.ExecuteAsync(CancellationToken) completed.");
            }
        }

        private ILogger _logger;

        public Worker(ILogger<Worker> logger = default)
        {
            this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }
    }

}