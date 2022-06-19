// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using System;

namespace Doc.InheritedMethodLevel
{
    [Inherited]
    internal class InheritedAspectAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            Console.WriteLine( "Hacked!" );

            return meta.Proceed();
        }
    }
}