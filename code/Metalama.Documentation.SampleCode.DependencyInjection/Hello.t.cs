using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.DependencyInjection
{
    public class Worker : BackgroundService
    {
        [Log]
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                this._messageWriter.Write($"Worker.ExecuteAsync(CancellationToken) started.");
                Console.WriteLine("Hello, world.");
                return Task.CompletedTask;
            }
            finally
            {
                this._messageWriter.Write($"Worker.ExecuteAsync(CancellationToken) completed.");
            }
        }

        private IMessageWriter _messageWriter;

        public Worker(IMessageWriter? messageWriter = default)
        {
            this._messageWriter = messageWriter ?? throw new System.ArgumentNullException(nameof(messageWriter));
        }
    }

}