// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using System;

namespace Doc.TypeFabric_
{
    // A trivial aspect to demonstrate the type fabric.
    public class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}