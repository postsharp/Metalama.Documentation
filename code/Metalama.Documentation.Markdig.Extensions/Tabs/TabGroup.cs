// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Markdig.Renderers;
using Metalama.Documentation.Markdig.Extensions.Sandbox;

namespace Metalama.Documentation.Markdig.Extensions.Tabs;

internal abstract class TabGroup
{
    private string TabGroupId { get; }

    public List<BaseTab> Tabs { get; } = new();

    public abstract string GetGitUrl();

    protected TabGroup( string tabGroupId )
    {
        if ( tabGroupId.Contains( ' ', StringComparison.Ordinal ) || tabGroupId.Contains( '.', StringComparison.Ordinal ) )
        {
            throw new ArgumentOutOfRangeException( nameof(tabGroupId), $"The id '{tabGroupId}' contains an invalid character." );
        }

        this.TabGroupId = tabGroupId;
    }

    public void Render( HtmlRenderer renderer, TabGroupBaseInline obj )
    {
        // Select tabs to render.
        var tabs = this.GetEnabledTabs( obj );

        if ( tabs.Count == 0 )
        {
            throw new InvalidOperationException( $"The tab group '{obj}' has no tab." );
        }

        // Define the wrapping div.
        var divId = $"code-{this.TabGroupId}";

        renderer.WriteLine( $"<div id=\"{divId}\" class=\"anchor\">" );

        if ( obj.AddLinks )
        {
            // Start the links.
            renderer.WriteLine( $@"<div class=""sample-links {(tabs.Count == 1 ? "" : "tabbed")}"">" );

            // Create the sandbox link.
            var sandboxPayload = this.GetSandboxPayload( tabs );

            if ( sandboxPayload != null )
            {
                renderer.WriteLine( $@"  <a class=""try"" onclick=""openSandbox('{sandboxPayload}');"" role=""button"">Open in sandbox</a>" );
                renderer.WriteLine( "<span class='separator'>|</span>" );
            }

            // Git.
            var gitUrl = this.GetGitUrl();

            renderer.WriteLine(
                @$"
    <a class=""github"" href=""{gitUrl}"" target=""github"">See on GitHub</a>" );

            renderer.WriteLine( "<span class='separator'>|</span>" );

            // Full screen.
            renderer.WriteLine(
                @$"
    <a class=""fullscreen"" onclick=""toggleFullScreen('{divId}');"" role=""button"" target=""github"">Full screen</a>" );

            // Close.
            renderer.WriteLine( "</div>" );
        }

        // Write the tabs.
        if ( tabs.Count == 1 )
        {
            // If there is a single file, we do not create a tab group.
            renderer.WriteLine( tabs[0].GetTabContent() );
        }
        else
        {
            renderer.WriteLine( @"<div class=""tabGroup""><ul>" );

            foreach ( var tab in tabs )
            {
                tab.AppendTabHeader( renderer, this.TabGroupId );
            }

            renderer.WriteLine( "</ul>" );

            foreach ( var tab in tabs )
            {
                tab.AppendTabBody( renderer, this.TabGroupId );
            }

            renderer.WriteLine( "</div>" );
        }

        // Close the wrapping div.
        renderer.WriteLine( "</div>" );
    }

    private List<BaseTab> GetEnabledTabs( TabGroupBaseInline obj )
    {
        bool IsTabEnabled( BaseTab tab ) => (obj.Tabs.Length == 0 || obj.Tabs.Contains( tab.TabId )) && !tab.IsEmpty();

        var tabs = this.Tabs.Where( IsTabEnabled ).ToList();

        return tabs;
    }

    public string? GetSandboxPayload( TabGroupBaseInline obj ) => this.GetSandboxPayload( this.GetEnabledTabs( obj ) );

    private string? GetSandboxPayload( List<BaseTab> tabs )
    {
        var sandboxFiles = new List<SandboxFile>();
        var canOpenInSandbox = true;

        foreach ( var tab in tabs )
        {
            if ( tab is CodeTab codeTab )
            {
                if ( codeTab.SandboxFileKind == SandboxFileKind.Incompatible )
                {
                    canOpenInSandbox = false;

                    break;
                }

                if ( codeTab.SandboxFileKind != SandboxFileKind.None )
                {
                    var fileName = codeTab.Name;

                    if ( !fileName.EndsWith( ".cs", StringComparison.Ordinal ) )
                    {
                        fileName += ".cs";
                    }

                    sandboxFiles.Add( new SandboxFile( fileName, codeTab.GetSandboxCode(), codeTab.SandboxFileKind ) );
                }
            }
            else if ( tab is CompareTab compareTab )
            {
                // Try currently requires that the code that is executed is in Program.cs.
                var fileName = "Program.cs";

                sandboxFiles.Add( new SandboxFile( fileName, compareTab.GetSandboxCode(), SandboxFileKind.TargetCode ) );
            }
        }

        if ( canOpenInSandbox )
        {
            return new SandboxPayload( sandboxFiles ).ToCompressedString();
        }
        else
        {
            return null;
        }
    }
}