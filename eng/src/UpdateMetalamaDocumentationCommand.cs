﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Engineering.BuildTools.Search;
using PostSharp.Engineering.BuildTools.Search.Backends;
using PostSharp.Engineering.BuildTools.Search.Updaters;

namespace BuildMetalamaDocumentation;

internal class UpdateMetalamaDocumentationCommand : UpdateSearchCommandBase
{
    protected override CollectionUpdater CreateUpdater( SearchBackendBase backend ) => new DocumentationUpdater<MetalamaDocCrawler>( ["Metalama"], backend );
}