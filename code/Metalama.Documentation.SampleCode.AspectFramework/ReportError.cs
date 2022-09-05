// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Doc.ReportError
{
    internal class Program
    {
        // Intentionally omitting the _logger field so an error is reported.

        [Log]
        private void Foo() { }

        private static void Main()
        {
            new Program().Foo();
        }
    }
}