using System;
using System.IO;

namespace Doc.DynamicTrivial
{
    class Program
    {
        TextWriter _logger = Console.Out;

        [Log]
        void Foo()
        {
            this._logger.WriteLine($"Executing Program.Foo().");
            return;
        }

        static void Main()
        {
            new Program().Foo();
        }
    }
}
