using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.LogDefaultFramework
{
    // The class using the Log aspect. This class is instantiated by the host builder and dependencies are automatically passed.
    public class Worker : BackgroundService
    {
        [Log]
        protected override Task ExecuteAsync( CancellationToken stoppingToken )
        {
            Console.WriteLine( "Hello, world." );
            return Task.CompletedTask;
        }
    }

}