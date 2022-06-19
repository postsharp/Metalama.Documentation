// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Microsoft.DocAsCode.Dfm;
using System.Collections.Generic;
using System.Composition;

namespace Metalama.Documentation.DfmExtensions
{
    [Export( typeof(IDfmCustomizedRendererPartProvider) )]
    public class MyRendererPartProvider : IDfmCustomizedRendererPartProvider
    {
        public IEnumerable<IDfmCustomizedRendererPart> CreateParts( IReadOnlyDictionary<string, object> parameters )
        {
            yield return new SampleRendererPart();
        }
    }
}