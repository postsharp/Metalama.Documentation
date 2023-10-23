// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Engineering.BuildTools.Search;
using PostSharp.Engineering.BuildTools.Search.Backends;
using PostSharp.Engineering.BuildTools.Search.Crawlers;
using PostSharp.Engineering.BuildTools.Search.Updaters;

namespace BuildMetalamaDocumentation;

public class UpdateMetalamaDocumentationCommand : UpdateSearchCommandBase
{
    protected override CollectionUpdater CreateUpdater( SearchBackendBase backend ) =>
        new DocumentationUpdater<MetalamaDocCrawler>( new[] { "Metalama" }, backend );
}