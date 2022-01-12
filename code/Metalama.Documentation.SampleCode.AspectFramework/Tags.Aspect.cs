// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.Tags
{
    internal class TagsAspect : MethodAspect
    {
        public override void BuildAspect( IAspectBuilder<IMethod> builder )
        {
            builder.Advices.OverrideMethod(
                builder.Target,
                nameof(this.OverrideMethod),
                tags: new Framework.Aspects.Tags { ["ParameterCount"] = builder.Target.Parameters.Count } );
        }

        [Template]
        private dynamic? OverrideMethod()
        {
            Console.WriteLine( $"This method has {meta.Tags["ParameterCount"]} parameters." );

            return meta.Proceed();
        }
    }
}