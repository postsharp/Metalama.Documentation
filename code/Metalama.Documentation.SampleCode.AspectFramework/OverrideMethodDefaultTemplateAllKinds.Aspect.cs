// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.OverrideMethodDefaultTemplateAllKinds
{
    public class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"{meta.Target.Method.Name}: start" );
            var result = meta.Proceed();
            Console.WriteLine( $"{meta.Target.Method.Name}: returning {result}." );

            return result;
        }
    }
}