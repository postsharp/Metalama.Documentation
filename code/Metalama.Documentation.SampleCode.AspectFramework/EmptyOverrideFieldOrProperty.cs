// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

namespace Metalama.Documentation.SampleCode.AspectFramework
{
    internal class EmptyOverrideFieldOrPropertyExample
    {
        [EmptyOverrideFieldOrProperty]
        public int Field;

        [EmptyOverrideFieldOrProperty]
        public string? Property { get; set; }
    }
}