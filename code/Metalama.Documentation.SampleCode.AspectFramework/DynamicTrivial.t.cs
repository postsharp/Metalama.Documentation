using System;
using System.IO;

namespace Doc.DynamicTrivial
{
    internal class Program
    {
        private TextWriter _logger = Console.Out;

        [Log]
        private void Foo()
        {
            this._logger.WriteLine("Executing Program.Foo().");
            return;
        }

        private static void Main()
        {
            new Program().Foo();
        }
    }
}