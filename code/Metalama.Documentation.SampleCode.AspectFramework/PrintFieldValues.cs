// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

namespace Metalama.Documentation.SampleCode.AspectFramework.PrintFieldValues
{
    internal class TargetCode
    {
        private readonly int _a;

        public string? B { get; set; }

        private static readonly int _c;

        [PrintFieldValues]
        public void Method() { }
    }
}