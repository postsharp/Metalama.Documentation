// Warning CS8618 on `_instanceRegistryHandle`: `Non-nullable field '_instanceRegistryHandle' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.`
using System;

namespace Doc.RegisterInstance
{

    [RegisterInstance]
    internal class DemoClass
    {
        private IDisposable _instanceRegistryHandle;

        public DemoClass()
        {
            this._instanceRegistryHandle = InstanceRegistry.Register(this);
        }
    }

}
