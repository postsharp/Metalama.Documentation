// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.LogMethodAndProperty
{
    [AttributeUsage( AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property )]
    public class LogAttribute : Aspect, IAspect<IMethod>, IAspect<IFieldOrProperty>
    {
        public void BuildAspect( IAspectBuilder<IMethod> builder )
        {
            builder.Advices.OverrideMethod( builder.Target, nameof(this.OverrideMethod) );
        }

        public void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
        {
            builder.Advices.OverrideFieldOrProperty( builder.Target, nameof(this.OverrideProperty) );
        }

        [Template]
        private dynamic? OverrideMethod()
        {
            Console.WriteLine( "Entering " + meta.Target.Method.ToDisplayString() );

            try
            {
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine( " Leaving " + meta.Target.Method.ToDisplayString() );
            }
        }

        [Template]
        private dynamic? OverrideProperty
        {
            get => meta.Proceed();

            set
            {
                Console.WriteLine( "Assigning " + meta.Target.Method.ToDisplayString() );
                meta.Proceed();
            }
        }
    }
}