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
        yield return new SampleRenderer();
    }
}