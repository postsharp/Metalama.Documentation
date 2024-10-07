// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using BuildMetalamaDocumentation.Markdig.Sandbox;
using BuildMetalamaDocumentation.Markdig.Tabs;

namespace BuildMetalamaDocumentation.UnitTests;

public class SnippetTests
{
    [Fact]
    public void TestIndentation()
    {
        var htmlFullPath = Path.Combine( Path.GetDirectoryName( this.GetType().Assembly.Location! )!, "GenerateBuilderAttribute.cs.html" );
        var tab = new CodeTab( "Test", htmlFullPath, SandboxFileKind.ExtraCode, marker: "IntroduceBuilder", htmlPath: htmlFullPath );

        var snippet = tab.GetTabContent();


    }
}