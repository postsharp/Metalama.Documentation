// Warning CS8618 on `_instanceRegistryHandle`: `Non-nullable field '_instanceRegistryHandle' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.`
using System;
namespace Doc.RegisterInstance;
[RegisterInstance]
internal class DemoClass
{
  public DemoClass() : base()
  {
    _instanceRegistryHandle = InstanceRegistry.Register(this);
  }
  public DemoClass(int i) : this()
  {
  }
  public DemoClass(string s)
  {
    _instanceRegistryHandle = InstanceRegistry.Register(this);
  }
  private IDisposable _instanceRegistryHandle;
}