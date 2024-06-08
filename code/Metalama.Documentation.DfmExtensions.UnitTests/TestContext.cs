// This is public domain Metalama sample code.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions.UnitTests;

internal class TestParser : IMarkdownParser
{
    public IMarkdownContext SwitchContext( IMarkdownContext context )
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<IMarkdownToken> Tokenize( SourceInfo sourceInfo )
    {
        throw new NotImplementedException();
    }

    public IMarkdownContext Context { get; } = new TestMarkdownContext();

    public Dictionary<string, LinkObj> Links => throw new NotImplementedException();

    public Options Options => throw new NotImplementedException();
}

internal class TestMarkdownContext : IMarkdownContext
{
    public IMarkdownContext CreateContext( ImmutableDictionary<string, object> variables ) => throw new NotImplementedException();

    public ImmutableList<IMarkdownRule> Rules => throw new NotImplementedException();

    public ImmutableDictionary<string, object> Variables { get; } = ImmutableDictionary<string, object>.Empty.Add( "BaseFolder", "c:\\src\\Foo" );
}