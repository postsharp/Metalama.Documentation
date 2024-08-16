﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.Markdig.Extensions.Helpers;

namespace Metalama.Documentation.Markdig.Extensions.Tabs;

internal class DirectoryTabGroup : TabGroup
{
    private readonly string _directory;

    public DirectoryTabGroup( string tabGroupId, string directory ) : base( tabGroupId )
    {
        this._directory = directory;
    }

    public override string GetGitUrl()
    {
        var gitUrl = GitHelper.GetOnlineUrl( this._directory );

        return gitUrl;
    }
}