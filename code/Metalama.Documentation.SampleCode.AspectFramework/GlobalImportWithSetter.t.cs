using System;
using System.Collections.Generic;

namespace Metalama.Documentation.SampleCode.AspectFramework.GlobalImportWithSetter
{
    internal class TargetCode
    {


        private IFormatProvider? _formatProvider1;


        private IFormatProvider? _formatProvider
        {
            get
            {
                var service = _formatProvider_Source;
                if (service == null)
                {
                    service = (IFormatProvider?)ServiceLocator.ServiceProvider.GetService(typeof(IFormatProvider));
                    this._formatProvider_Source = service;
                }

                return service;
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        private IFormatProvider? _formatProvider_Source
        {
            get
            {
                return this._formatProvider1;
            }

            set
            {
                this._formatProvider1 = value;
            }
        }
    }

    internal class ServiceLocator : IServiceProvider
    {

        private static readonly ServiceLocator _instance = new();
        private readonly Dictionary<Type, object> _services = new();

        public static IServiceProvider ServiceProvider => _instance;


        object? IServiceProvider.GetService(Type serviceType)
        {
            this._services.TryGetValue(serviceType, out var value);
            return value;
        }

        public static void AddService<T>(T service) where T : class => _instance._services[typeof(T)] = service;
    }

}
