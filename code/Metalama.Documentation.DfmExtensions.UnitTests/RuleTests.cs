// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
using System.Runtime.InteropServices;
using Xunit;

namespace Metalama.Documentation.DfmExtensions.UnitTests;

public class RuleTests
{
    [Theory]
    [InlineData( "not a match", false, null, null )]
    [InlineData( "[!metalama-test some/path/to/somewhere.cs]", true, "some/path/to/somewhere.cs", "" )]
    [InlineData( "[!metalama-test ~/some/path/to/somewhere.cs]", true, "~/some/path/to/somewhere.cs", "" )]
    [InlineData( "[!metalama-test ~/some/path/to/somewhere.cs name=\"the name\"]", true, "~/some/path/to/somewhere.cs", "the name" )]
    [InlineData(
        "[!metalama-test ~/some/path/to/somewhere.cs name=\"the name\" title=\"the title\"]",
        true,
        "~/some/path/to/somewhere.cs",
        "the name",
        "the title" )]
    public void AspectTest( string text, bool isMatch, string path = "", string name = "", string title = "" )
    {
        var rule = new AspectTestTokenRule();
        var context = new MarkdownParsingContext( SourceInfo.Create( text, "file.md", 0, 1 ) );
        var token = (AspectTestToken?) rule.TryMatch( new TestParser(), context );

        if ( token != null )
        {
            Assert.True( isMatch );
            Assert.Equal( path, token.Src );
            Assert.Equal( name, token.Name );
            Assert.Equal( title, token.Title );
        }
        else
        {
            Assert.False( isMatch );
        }
    }

    [Theory]
    [InlineData( "not a match", false )]
    [InlineData( "[!metalama-file some/path.cs]", true, "some/path.cs", false )]
    [InlineData( "[!metalama-file some/path.cs transformed ]", true, "some/path.cs", true )]
    public void SingleFile( string text, bool isMatch, string path = "", bool showTransformed = false )
    {
        var rule = new SingleFileTokenRule();

        var context = new MarkdownParsingContext( SourceInfo.Create( text, "file.md", 0, 1 ) );
        var token = (SingleFileToken?) rule.TryMatch( new TestParser(), context );

        if ( token != null )
        {
            Assert.True( isMatch );
            Assert.Equal( path, token.Src );
            Assert.Equal( showTransformed, token.ShowTransformed );
        }
        else
        {
            Assert.False( isMatch );
        }
    }
}