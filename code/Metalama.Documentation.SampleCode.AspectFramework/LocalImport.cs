// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;

namespace Doc.ImportService
{
    internal class Foo
    {
        // readonly IServiceProvider _serviceProvider;

        [ImportAspect]
        private IFormatProvider? FormatProvider { get; }

        public string Format( object? o )
        {
            return ((ICustomFormatter) this.FormatProvider!.GetFormat( typeof(ICustomFormatter) )!)
                .Format( null, o, this.FormatProvider );
        }
    }
}