// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Sandbox;
using BuildMetalamaDocumentation.Markdig.Tabs;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using System.IO;

namespace BuildMetalamaDocumentation.Markdig.SingleFiles;

public class HtmlSingeFileInlineRenderer : HtmlObjectRenderer<SingleFileInline>
{
    protected override void Write( HtmlRenderer renderer, SingleFileInline obj )
    {
        var name = Path.GetFileNameWithoutExtension( obj.Src );

        var tab = obj.ShowTransformed
            ? new TransformedSingleFileCodeTab(
                Path.GetFileNameWithoutExtension( obj.Src ),
                obj.Src,
                "" )
            : new CodeTab( name, obj.Src, SandboxFileKind.ExtraCode, obj.Marker, obj.Member );

        renderer.WriteLine( "<div class='single-file'>" );
        renderer.WriteLine( tab.GetTabContent( false ) );
        renderer.WriteLine( "</div>" );
    }
}