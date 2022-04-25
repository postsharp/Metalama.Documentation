using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Doc.RegisterInstance
{
    public class RegisterInstanceAttribute : TypeAspect
    {
        [Introduce]
        private IDisposable _instanceRegistryHandle;

        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            base.BuildAspect( builder );

            builder.Advices.AddInitializerBeforeInstanceConstructor( builder.Target, nameof( BeforeInstanceConstructor ) );
        }

        [Template]
        private void BeforeInstanceConstructor()
        {
            this._instanceRegistryHandle = InstanceRegistry.Register( meta.This );            
        }
    }

}
