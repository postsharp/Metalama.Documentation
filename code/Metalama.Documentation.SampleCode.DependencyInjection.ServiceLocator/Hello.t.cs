using System;
using System.Threading.Tasks;
using Metalama.Framework.DependencyInjection.ServiceLocator;

namespace Doc.DependencyInjection
{
    public class Worker
    {
        [Log]
        public Task ExecuteAsync()
        {
            try
            {
                this._messageWriter.Write($"Worker.ExecuteAsync() started.");
                Console.WriteLine("Hello, world.");
                return Task.CompletedTask;
            }
            finally
            {
                this._messageWriter.Write($"Worker.ExecuteAsync() completed.");
            }
        }

        private IMessageWriter _messageWriter;

        public Worker()
        {
            this._messageWriter = (IMessageWriter)ServiceProviderProvider.ServiceProvider().GetService(typeof(IMessageWriter)) ?? throw new InvalidOperationException($"The service 'IMessageWriter' could not be obtained from the service locator.");
        }
    }

}