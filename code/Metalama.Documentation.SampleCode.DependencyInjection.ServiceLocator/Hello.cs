using System;
using System.Threading.Tasks;

namespace Doc.DependencyInjection
{
    public class Worker 
    {
        [Log]
        public Task ExecuteAsync( )
        {
            Console.WriteLine( "Hello, world." );
            return Task.CompletedTask;
        }
    }

}