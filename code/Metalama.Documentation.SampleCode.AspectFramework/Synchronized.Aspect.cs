// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.Synchronized
{
    internal class SynchronizedAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            foreach ( var method in builder.Target.Methods.Where( m => !m.IsStatic ) )
            {
                builder.Advice.Override( method, nameof(this.OverrideMethod) );
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