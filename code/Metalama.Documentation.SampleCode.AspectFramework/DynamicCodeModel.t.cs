using System;
using System.IO;

namespace Doc.DynamicCodeModel
{
    class Program
    {
        TextWriter _logger = Console.Out;

        [Log]
        void Foo()
        {
            _logger.WriteLine($"Executing Program.Foo().");
            return;
        }

        static void Main()
        {
            new Program().Foo();
        }
    }
}
