// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Doc.PrintFieldValues
{
    internal class Foo
    {
        private readonly int _a;

        public string? B { get; set; }

        private static readonly int _c;

        [PrintFieldValues]
        public void Bar() { }
    }
}