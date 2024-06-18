// Warning CS8618 on `ProxyAspect`: `Non-nullable field '_interceptor' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.`
using Metalama.Framework.CompileTimeContracts;
using System;
namespace Doc.StaticProxy;
public interface IPropertyStore
{
  object Get(string name);
  void Store(string name, object value);
}
public interface IInterceptor
{
  public T Invoke<T>(Func<T> next);
  public void Invoke(Action next);
}
[ProxyAspect(typeof(IPropertyStore))]
public class PropertyStoreProxy : IPropertyStore
{
  private IPropertyStore _intercepted;
  private IInterceptor _interceptor;
  public PropertyStoreProxy(IInterceptor interceptor, IPropertyStore intercepted)
  {
    _interceptor = interceptor;
    _intercepted = intercepted;
  }
  object IPropertyStore.Get(string name)
  {
    return _interceptor.Invoke(() => _intercepted.Get(name));
  }
  void IPropertyStore.Store(string name, object value)
  {
    _interceptor.Invoke(() => _intercepted.Store(name, value));
  }
}