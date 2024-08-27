// This is public domain Metalama sample code.

using BuildMetalamaDocumentation.Markdig.Helpers;
using BuildMetalamaDocumentation.Markdig.Tabs;
using System.Linq;

namespace BuildMetalamaDocumentation.Markdig.AspectTests;

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