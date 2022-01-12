// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.ConsumingProperty
{
    public class Log : OverrideMethodAspect
    {
        public string? Category { get; set; }

        public override dynamic? OverrideMethod()
        {
            if ( !meta.Target.Project.TryGetProperty( "DefaultLogCategory", out var defaultCategory ) )
            {
                defaultCategory = "Default";
            }

            Console.WriteLine( $"{this.Category ?? defaultCategory}: Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}