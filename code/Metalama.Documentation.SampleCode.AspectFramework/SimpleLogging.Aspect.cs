// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.SimpleLogging
{
    public class SimpleLogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"Entering {meta.Target.Method.ToDisplayString()}" );

            try
            {
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine( $"Leaving {meta.Target.Method.ToDisplayString()}" );
            }
        }
    }
}