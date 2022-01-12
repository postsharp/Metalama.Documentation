// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

namespace Metalama.Documentation.SampleCode.AspectFramework.LogMethodAndProperty
{
    internal class TargetCode
    {
        [Log]
        public int Method( int a, int b )
        {
            return a + b;
        }

        [Log]
        public int Property { get; set; }

        [Log]
        public string? Field { get; set; }
    }
}