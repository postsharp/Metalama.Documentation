// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.CompileTimeIf
{
    internal class TargetCode
    {
        [CompileTimeIf]
        public void InstanceMethod()
        {
            Console.WriteLine( "InstanceMethod" );
        }

        [CompileTimeIf]
        public static void StaticMethod()
        {
            Console.WriteLine( "StaticMethod" );
        }
    }
}