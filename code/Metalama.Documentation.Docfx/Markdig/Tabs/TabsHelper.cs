namespace Metalama.Documentation.Docfx.Markdig.Tabs;

public static class TabsHelper
{
    public static string[] SplitTabs( string tabs ) => tabs.Split( ',' ).Select( x => x.Trim() ).Where( x => !string.IsNullOrEmpty( x ) ).ToArray();
}