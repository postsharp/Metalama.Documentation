using System;
using System.Collections.Generic;

namespace Caravela.Documentation.SampleCode.AspectFramework.GlobalImportWithSetter
{
    class TargetCode
    {


        private IFormatProvider _formatProvider;
        [Import]
        IFormatProvider FormatProvider
        {
            get
            {
                var service = FormatProvider_Source;
                if (service == null)
                {
                    service = (IFormatProvider)ServiceLocator.ServiceProvider.GetService(typeof(IFormatProvider));
                    this.FormatProvider_Source = service;
                }

                return service;
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        private IFormatProvider FormatProvider_Source
        {
            get
            {
                return this._formatProvider;
            }

            set
            {
                this._formatProvider = value;
            }
        }
    }

    class ServiceLocator : IServiceProvider
    {

        private static readonly ServiceLocator _instance = new();
        private readonly Dictionary<Type, object> _services = new();

        public static IServiceProvider ServiceProvider => _instance;


        object IServiceProvider.GetService(Type serviceType)
        {
            this._services.TryGetValue(serviceType, out var value);
            return value;
        }

        public static void AddService<T>(T service) => _instance._services[typeof(T)] = service;
    }

}
