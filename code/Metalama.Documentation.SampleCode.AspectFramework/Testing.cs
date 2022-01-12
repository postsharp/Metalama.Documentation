using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.Testing
{
    internal class SimpleLogTests
    {
        [SimpleLog]
        private void MyMethod()
        {
            Console.WriteLine("Hello, world");
        }
    }
}
