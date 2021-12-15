using System;

namespace Caravela.Documentation.SampleCode.AspectFramework.Testing
{
    class SimpleLogTests
    {
        [SimpleLog]
        void MyMethod()
        {
            Console.WriteLine($"Entering SimpleLogTests.MyMethod()");
            try
            {
                Console.WriteLine("Hello, world");
                return;
            }
            finally
            {
                Console.WriteLine($"Leaving SimpleLogTests.MyMethod()");
            }
        }
    }
}
