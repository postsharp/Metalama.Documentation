// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.BuildInfo
{
    internal partial class BuildInfo
    {
        private class Fabric : TypeFabric
        {
            [Introduce]
            public string? TargetFramework { get; } = meta.Target.Project.TargetFramework;

            [Introduce]
            public string? Configuration { get; } = meta.Target.Project.Configuration;
        }
    }
}