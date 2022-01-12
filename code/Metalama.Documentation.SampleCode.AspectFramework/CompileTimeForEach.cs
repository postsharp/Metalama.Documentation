// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.CompileTimeForEach
{
    internal class TargetCode
    {
        [CompileTimeForEach]
        private void Method( int a, string b )
        {
            Console.WriteLine( $"Hello, world! a={a}, b='{b}'." );
        }
    }
}