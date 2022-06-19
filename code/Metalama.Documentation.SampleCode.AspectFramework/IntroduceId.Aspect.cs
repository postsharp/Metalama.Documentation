// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using System;

namespace Doc.IntroduceId
{
    internal class IntroduceIdAttribute : TypeAspect
    {
        [Introduce]
        public Guid Id { get; } = Guid.NewGuid();
    }
}