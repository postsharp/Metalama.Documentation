// This is public domain Metalama sample code.

using Metalama.Framework.CompileTimeContracts;
using System;

namespace Doc.StaticProxy;

public interface IPropertyStore
{
    object Get( string name );

    void Store( string name, object value );
}

public interface IInterceptor
{
    public T Invoke<T>( Func<T> next );

    public void Invoke( Action next );
}

[ProxyAspect( typeof(IPropertyStore) )]
public class PropertyStoreProxy { }