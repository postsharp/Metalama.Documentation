// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Doc.LogParameters
{
    internal class Foo
    {
        [Log]
        private void VoidMethod( int a, out int b )
        {
            b = a;
        }

        [Log]
        private int IntMethod( int a )
        {
            return a;
        }
    }
}