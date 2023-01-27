using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.DocAsCode.MarkdownLite;
using Microsoft.DocAsCode.MarkdownLite.Matchers;

namespace Metalama.Documentation.DfmExtensions.UnitTests;

internal class TestParser : IMarkdownParser
{
    public TestParser() { }

    public IMarkdownContext SwitchContext( IMarkdownContext context )
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<IMarkdownToken> Tokenize( SourceInfo sourceInfo )
    {
        throw new NotImplementedException();
    }

    public IMarkdownContext Context => null;

    public Dictionary<string, LinkObj> Links => throw new NotImplementedException();

    public Options Options => throw new NotImplementedException();
}