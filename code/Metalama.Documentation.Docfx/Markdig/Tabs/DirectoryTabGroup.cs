// This is public domain Metalama sample code.

using Metalama.Documentation.Docfx.Markdig.Helpers;

namespace Metalama.Documentation.Docfx.Markdig.Tabs;

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