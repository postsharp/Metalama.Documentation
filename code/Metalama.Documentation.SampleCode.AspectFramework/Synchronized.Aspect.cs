// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.Synchronized
{
    internal class SynchronizedAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            foreach ( var method in builder.Target.Methods.Where( m => !m.IsStatic ) )
            {
                builder.Advices.OverrideMethod( method, nameof(this.OverrideMethod) );
            }
        }

        [Template]
        private dynamic? OverrideMethod()
        {
            lock ( meta.This )
            {
                return meta.Proceed();
            }
        }
    }
}