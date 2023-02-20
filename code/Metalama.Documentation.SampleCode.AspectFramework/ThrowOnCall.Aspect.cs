// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using System;
using System.Threading;

namespace Doc.ThrowOnCall
{
    internal class ThrowOnCallAttribute : OverrideMethodAspect
    {
		
        public override dynamic OverrideMethod() =>
            throw new Exception($"Method {meta.Target.Method.ToDisplayString()} is obsolete.");
        
    }
}