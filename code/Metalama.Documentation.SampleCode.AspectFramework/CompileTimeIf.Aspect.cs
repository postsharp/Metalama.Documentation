// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.CompileTimeIf
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