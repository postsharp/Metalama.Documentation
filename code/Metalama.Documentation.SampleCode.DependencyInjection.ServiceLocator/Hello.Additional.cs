using Metalama.Framework.DependencyInjection.ServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Doc.DependencyInjection
{
    public static class Program
    {
        private static Task Main()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IMessageWriter>(new MessageWriter());
            var serviceProvider = serviceCollection.BuildServiceProvider();

            
            ServiceProviderProvider.ServiceProvider = () => serviceProvider;

            return new Worker().ExecuteAsync();
        }

    }

    public interface IMessageWriter
    {
        void Write( string message );
    }

    public class MessageWriter : IMessageWriter
    {
        public void Write( string message )
        {
            Console.WriteLine( message );
        }
    }
}