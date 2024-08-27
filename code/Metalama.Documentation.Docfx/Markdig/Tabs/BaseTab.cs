// This is public domain Metalama sample code.

using Markdig.Renderers;

namespace Metalama.Documentation.Docfx.Markdig.Tabs;

internal abstract class BaseTab
{
    public string TabId { get; }

    public string FullPath { get; }

    public bool IsEmpty()
        => !File.Exists( this.FullPath ) || this.IsContentEmpty( File.ReadAllLines( this.FullPath ) );

    protected virtual bool IsContentEmpty( string[] lines ) => lines.All( string.IsNullOrWhiteSpace );

    protected BaseTab( string tabId, string fullPath )
    {
        this.TabId = tabId;
        this.FullPath = fullPath;
    }

    protected string GetProjectDirectory()
    {
        for ( var directory = Path.GetDirectoryName( this.FullPath );
              directory != null;
              directory = Path.GetDirectoryName( directory ) )
        {
            if ( Directory.GetFiles( directory, "*.csproj" ).Any() )
            {
                return directory;
            }
        }

        throw new InvalidOperationException( $"Cannot find the project directory for '{this.FullPath}'." );
    }

    public abstract string GetTabContent( bool fallbackToSource = true );

    protected abstract string TabHeader { get; }

    public void AppendTabHeader( HtmlRenderer renderer, string tabGroupId )
    {
        renderer.Write( $"<li><a href=\"#tabpanel_{tabGroupId}_{this.TabId}\">{this.TabHeader}</a></li>" );
    }

    public void AppendTabBody( HtmlRenderer renderer, string tabGroupId )
    {
        var content = this.GetTabContent();
        renderer.Write( $"<div id=\"tabpanel_{tabGroupId}_{this.TabId}\">{content}</div>" );
    }
}