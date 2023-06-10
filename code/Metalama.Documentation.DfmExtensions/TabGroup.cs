// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metalama.Documentation.DfmExtensions;

internal abstract class TabGroup
{
    private string TabGroupId { get; }

    public List<BaseTab> Tabs { get; } = new();

    public abstract string GetGitUrl();

    protected TabGroup( string tabGroupId )
    {
        this.TabGroupId = tabGroupId;
    }

    public void Render( StringBuilder stringBuilder, TabGroupBaseToken token )
    {
        // Select tabs to render.
        var tabs = this.GetEnabledTabs( token );

        if ( tabs.Count == 0 )
        {
            throw new InvalidOperationException( $"The tab group '{token}' has no tab." );
        }

        // Define the wrapping div.
        var divId = $"code-{this.TabGroupId}";
        stringBuilder.AppendLine( $"<div id={divId} class=\"anchor\">" );

        if ( token.AddLinks )
        {
            // Start the links.
            stringBuilder.AppendLine( $@"<div class=""sample-links {(tabs.Count == 1 ? "" : "tabbed")}"">" );

            // Create the sandbox link.
            var sandboxPayload = this.GetSandboxPayload( tabs );

            if ( sandboxPayload != null )
            {
                stringBuilder.AppendLine( $@"  <a class=""try"" onclick=""openSandbox('{sandboxPayload}');"" role=""button"">Open in sandbox</a> |" );
            }

            // Finish the links.
            var gitUrl = this.GetGitUrl();

            stringBuilder.AppendLine(
                @$"
    <a class=""github"" href=""{gitUrl}"" target=""github"">See on GitHub</a>
</div>" );

            stringBuilder.AppendLine( "</div>" );
        }

        // Write the tabs.
        if ( tabs.Count == 1 )
        {
            // If there is a single file, we do not create a tab group.
            stringBuilder.AppendLine( tabs[0].GetTabContent() );
        }
        else
        {
            stringBuilder.AppendLine( @"<div class=""tabGroup""><ul>" );

            foreach ( var tab in tabs )
            {
                tab.AppendTabHeader( stringBuilder, this.TabGroupId );
            }

            stringBuilder.AppendLine( "</ul>" );

            foreach ( var tab in tabs )
            {
                tab.AppendTabBody( stringBuilder, this.TabGroupId );
            }

            stringBuilder.AppendLine( "</div>" );
        }

        // Close the wrapping div.
        stringBuilder.AppendLine( "</div>" );
    }

    private List<BaseTab> GetEnabledTabs( TabGroupBaseToken token )
    {
        bool IsTabEnabled( BaseTab tab ) => (token.Tabs.Length == 0 || token.Tabs.Contains( tab.TabId )) && !tab.IsEmpty();

        var tabs = this.Tabs.Where( IsTabEnabled ).ToList();

        return tabs;
    }

    public string? GetSandboxPayload( TabGroupBaseToken token ) => this.GetSandboxPayload( this.GetEnabledTabs( token ) );

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

                    if ( !fileName.EndsWith( ".cs" ) )
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