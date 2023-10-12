// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Code;
using Metalama.Patterns.Caching.Aspects.Configuration;
using Microsoft.Extensions.Logging;

namespace Doc.ParameterFilter
{
    public class LoggerParameterClassifier : ICacheParameterClassifier
    {
        public CacheParameterClassification GetClassification( IParameter parameter )
            => parameter.Type.Is( typeof(ILogger) )
                ? CacheParameterClassification.ExcludeFromCacheKey
                : CacheParameterClassification.Default;
    }
}