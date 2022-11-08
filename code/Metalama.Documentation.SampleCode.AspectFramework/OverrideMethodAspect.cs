// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.OverrideMethodAspect_
{
    // <aspect>
    [AttributeUsage( AttributeTargets.Method )]
    public abstract class OverrideMethodAspect : Attribute, IAspect<IMethod>
    {
        public virtual void BuildAspect( IAspectBuilder<IMethod> builder )
        {
            builder.Advice.Override( builder.Target, nameof(this.OverrideMethod) );
        }

        public virtual void BuildEligibility( IEligibilityBuilder<IMethod> builder )
        {
            builder.ExceptForInheritance().MustNotBeAbstract();
        }

        [Template]
        public abstract dynamic? OverrideMethod();
    }

    // </aspect>
}