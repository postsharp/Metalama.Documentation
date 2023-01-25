// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Doc.CompileTimeIf
{
    internal class CompileTimeIfAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            if ( meta.Target.Method.IsStatic )
            {
                Console.WriteLine( $"Invoking {meta.Target.Method.ToDisplayString()}" );
            }
            else
            {
                Console.WriteLine( $"Invoking {meta.Target.Method.ToDisplayString()} on instance {meta.This.ToString()}." );
            }

            return meta.Proceed();
        }
    }
}