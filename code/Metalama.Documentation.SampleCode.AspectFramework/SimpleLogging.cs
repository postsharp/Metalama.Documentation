// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.SimpleLogging
{
    internal class TargetCode
    {
        [SimpleLog]
        public void Method1()
        {
            Console.WriteLine( "Hello, world." );
        }
    }
}