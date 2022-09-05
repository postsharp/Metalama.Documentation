// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.RegisterInstance
{
    public class RegisterInstanceAttribute : TypeAspect
    {
        [Introduce]
        private IDisposable _instanceRegistryHandle;

        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            base.BuildAspect( builder );

            builder.Advice.AddInitializer( builder.Target, nameof(this.BeforeInstanceConstructor), InitializerKind.BeforeInstanceConstructor );
        }

        [Template]
        private void BeforeInstanceConstructor()
        {
            this._instanceRegistryHandle = InstanceRegistry.Register( meta.This );
        }
    }
}