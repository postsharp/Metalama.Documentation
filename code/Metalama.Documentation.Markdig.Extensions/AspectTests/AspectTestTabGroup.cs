// This is public domain Metalama sample code.

using Metalama.Documentation.Markdig.Extensions.Helpers;
using Metalama.Documentation.Markdig.Extensions.Tabs;

namespace Metalama.Documentation.Markdig.Extensions.AspectTests;

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