// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Doc.Testing
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