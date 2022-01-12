// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.ImportService
{
    internal class TargetCode
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