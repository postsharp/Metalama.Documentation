// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;
using System.Collections.Generic;

namespace Metalama.Documentation.SampleCode.AspectFramework.GlobalImportWithSetter
{
    internal class TargetCode
    {
        [Import]
        private IFormatProvider? _formatProvider;
    }

    internal class ServiceLocator : IServiceProvider
    {
        private static readonly ServiceLocator _instance = new();
        private readonly Dictionary<Type, object> _services = new();

        public static IServiceProvider ServiceProvider => _instance;

        object? IServiceProvider.GetService( Type serviceType )
        {
            this._services.TryGetValue( serviceType, out var value );

            return value;
        }

        public static void AddService<T>( T service ) where T : class => _instance._services[typeof(T)] = service;
    }
}