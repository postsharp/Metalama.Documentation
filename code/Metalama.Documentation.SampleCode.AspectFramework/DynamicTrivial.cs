// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.IO;

namespace Doc.DynamicTrivial
{
    internal class Program
    {
        private TextWriter _logger = Console.Out;

        [Log]
        private void Foo() { }

        private static void Main()
        {
            new Program().Foo();
        }
    }
}