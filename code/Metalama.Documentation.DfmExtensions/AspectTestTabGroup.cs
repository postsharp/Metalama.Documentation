// This is public domain Metalama sample code.

using System.Linq;

namespace Metalama.Documentation.DfmExtensions;

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