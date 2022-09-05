// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Doc
{
    internal class EmptyOverrideFieldOrPropertyExample
    {
        [EmptyOverrideFieldOrProperty]
        public int Field;

        [EmptyOverrideFieldOrProperty]
        public string? Property { get; set; }
    }
}