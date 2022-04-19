// Warning CS8618 on `_instanceRegistryHandle`: `Non-nullable field '_instanceRegistryHandle' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.`
using System;

namespace Doc.RegisterInstance
{

    [RegisterInstance]
    internal class DemoClass
    {
        // TODO: the default constructor should not be necessary, but now it is (bug #30214).
        public DemoClass()
        {
            this._instanceRegistryHandle = InstanceRegistry.Register(this);
        }

        private IDisposable _instanceRegistryHandle;
    }

}
