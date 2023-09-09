using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace Doc.LogCustomFramework
{
  // The class using the Log aspect. This class is instantiated by the host builder and dependencies are automatically passed.
  public class Worker : BackgroundService
  {
    [Log]
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      try
      {
        LoggerExtensions.LogWarning(this._logger, "Worker.ExecuteAsync(CancellationToken) started.");
        _logger.LogInformation("Hello, world.");
        return Task.CompletedTask;
      }
      finally
      {
        LoggerExtensions.LogWarning(this._logger, "Worker.ExecuteAsync(CancellationToken) completed.");
      }
    }
    private ILogger _logger;
    public Worker(ILogger<Worker> logger = default)
    {
      this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }
  }
}