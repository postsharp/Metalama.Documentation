// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

namespace Metalama.Documentation.SampleCode.AspectFramework.LogParameters
{
    internal class TargetCode
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