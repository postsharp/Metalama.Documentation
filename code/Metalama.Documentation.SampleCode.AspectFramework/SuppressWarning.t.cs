using System;
using System.IO;

namespace Doc.SuppressWarning
{
    internal class Program
    {

#pragma warning disable CS0169
        private TextWriter _logger = Console.Out;

#pragma warning restore CS0169

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