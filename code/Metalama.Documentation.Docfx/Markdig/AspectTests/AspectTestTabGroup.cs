// This is public domain Metalama sample code.

using Metalama.Documentation.Docfx.Markdig.Helpers;
using Metalama.Documentation.Docfx.Markdig.Tabs;

namespace Metalama.Documentation.Docfx.Markdig.AspectTests;

internal class AspectTestTabGroup : TabGroup
{
    public override string GetGitUrl()
    {
        var tab = this.Tabs.OrderBy( t => t.FullPath.Length ).First();
        var gitUrl = GitHelper.GetOnlineUrl( tab.FullPath );

        return gitUrl;
    }

    public AspectTestTabGroup( string tabGroupId ) : base( tabGroupId ) { }
}