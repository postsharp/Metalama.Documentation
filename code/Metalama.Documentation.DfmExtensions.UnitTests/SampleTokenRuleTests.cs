
using Microsoft.DocAsCode.MarkdownLite;
using Xunit;

namespace Metalama.Documentation.DfmExtensions.UnitTests;

public class SampleTokenRuleTests
{
    [Theory]
    [InlineData( "not a match", false, null, null )]
    [InlineData( "[!metalama-sample some/path/to/somewhere.cs]", true, "some/path/to/somewhere.cs", "" )]
    [InlineData( "[!metalama-sample ~/some/path/to/somewhere.cs]", true, "~/some/path/to/somewhere.cs", "" )]
    [InlineData( "[!metalama-sample ~/some/path/to/somewhere.cs name=\"the name\"]", true, "~/some/path/to/somewhere.cs", "the name" )]
    [InlineData(
        "[!metalama-sample ~/some/path/to/somewhere.cs name=\"the name\" title=\"the title\"]",
        true,
        "~/some/path/to/somewhere.cs",
        "the name",
        "the title" )]
    public void Match( string text, bool isMatch, string path = "", string name = "", string title = "" )
    {
        var rule = new SampleTokenRule();
        var context = new MarkdownParsingContext( SourceInfo.Create( text, "file.md", 0, 1 ) );
        var token = (SampleToken?) rule.TryMatch( new TestParser(), context );

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
}