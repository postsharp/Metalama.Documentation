// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

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