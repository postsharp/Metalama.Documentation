// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;

namespace Metalama.Documentation.SampleCode.AspectFramework.Eligibility
{
    internal class LogAttribute : OverrideMethodAspect
    {
        public override void BuildEligibility( IEligibilityBuilder<IMethod> builder )
        {
            base.BuildEligibility( builder );

            // The aspect must not be offered to non-static methods because it uses a static field 'logger'.
            builder.MustBeNonStatic();
        }

        public override dynamic? OverrideMethod()
        {
            meta.This.logger.WriteLine( $"Executing {meta.Target.Method}" );

            return meta.Proceed();
        }
    }
}