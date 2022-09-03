// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

namespace Doc.LogMethodAndProperty
{
    internal class Foo
    {
        [Log]
        public int Method( int a, int b )
        {
            return a + b;
        }

        [Log]
        public int Property { get; set; }

        [Log]
        public string? Field;
    }
}