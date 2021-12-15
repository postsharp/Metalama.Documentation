using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.Testing
{
    class SimpleLogTests
    {
        [SimpleLog]
        void MyMethod()
        {
            Console.WriteLine("Hello, world");
        }
    }
}
