// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.CompileTimeForEach
{
    internal class CompileTimeForEachAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            foreach ( var p in meta.Target.Parameters.Where( p => p.RefKind != RefKind.Out ) )
            {
                Console.WriteLine( $"{p.Name} = {p.Value}" );
            }

            return meta.Proceed();
        }
    }
}