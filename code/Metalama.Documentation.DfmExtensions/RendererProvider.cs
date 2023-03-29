// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.Dfm;
using System.Collections.Generic;
using System.Composition;
using JetBrains.Annotations;

namespace Metalama.Documentation.DfmExtensions;

[Export( typeof(IDfmCustomizedRendererPartProvider) )]
[UsedImplicitly]
public class RendererProvider : IDfmCustomizedRendererPartProvider
{
    public IEnumerable<IDfmCustomizedRendererPart> CreateParts( IReadOnlyDictionary<string, object> parameters )
    {
        yield return new AspectTestRenderer();
        yield return new SingleFileRenderer();
        yield return new ProjectButtonsRenderer();
        yield return new CompareFileRenderer();
    }
}