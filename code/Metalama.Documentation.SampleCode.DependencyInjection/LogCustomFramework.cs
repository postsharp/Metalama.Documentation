using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

#if METALAMA
using Microsoft.Extensions.Logging;
#endif

namespace Doc.LogCustomFramework
{
    // The class using the Log aspect. This class is instantiated by the host builder and dependencies are automatically passed.
    public class Worker : BackgroundService
    {
        [Log]
        protected override Task ExecuteAsync( CancellationToken stoppingToken )
        {
#if METALAMA
            _logger.LogInformation( "Hello, world." );
#endif
            return Task.CompletedTask;
        }
    }

}