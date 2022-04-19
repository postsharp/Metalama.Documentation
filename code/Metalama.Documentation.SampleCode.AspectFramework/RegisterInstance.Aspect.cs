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

    public static class InstanceRegistry
    {
        private static int _nextId;
        private static readonly ConcurrentDictionary<int, WeakReference<object>> _instances = new();

        public static IDisposable Register( object instance )
        {
            var id = Interlocked.Increment( ref _nextId );
            _instances.TryAdd( id, new WeakReference<object>(instance) );

            return new Handle( id );
        }
        
        private static void Unregister( int id )
        {
            _instances.TryRemove( id, out _ );
        }

        public static IEnumerable<object> GetInstances()
        {
            foreach ( var weakReference in _instances.Values )
            {
                if ( weakReference.TryGetTarget( out var instance ) )
                {
                    yield return instance;
                }
            }
        }


        
        private class Handle : IDisposable
        {
            private int _id;

            public Handle( int id )
            {
                this._id = id;
            }

            public void Dispose()
            {
                GC.SuppressFinalize( this );
                Unregister( _id );
            }

            ~Handle()
            {
                Unregister( _id );
            }
        }

    }

 


}
