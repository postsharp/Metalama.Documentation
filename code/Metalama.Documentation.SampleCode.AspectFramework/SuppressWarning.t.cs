using System;
using System.IO;

namespace Doc.SuppressWarning
{
    class Program
    {
#pragma warning disable CS0169
        TextWriter _logger = Console.Out;
#pragma warning restore CS0169

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
