using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.CustomFramework
{
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