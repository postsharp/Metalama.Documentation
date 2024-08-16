﻿using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using Metalama.Documentation.Markdig.Extensions.Helpers;
using Metalama.Documentation.Markdig.Extensions.Tabs;
using System.Diagnostics.CodeAnalysis;

namespace Metalama.Documentation.Markdig.Extensions.AspectTests;

public class AspectTestInlineParser : InlineParser
{
    private const string _startString = "[!metalama-test";

    public AspectTestInlineParser()
    {
        this.OpeningCharacters = ['['];
    }

    public override bool Match( InlineProcessor processor, ref StringSlice slice )
    {
        var saved = slice;

        if ( !ExtensionsHelper.MatchStart( ref slice, _startString, false ) )
        {
            return false;
        }

        slice.SkipWhitespaces();

        if ( !slice.ReadUntilCharOrWhitespace( ']', out var path ) )
        {
            return false;
        }

        var resolvedPath = PathHelper.ResolvePath( path );

        var test = new AspectTestInline { Src = resolvedPath };

        while ( true )
        {
            slice.SkipWhitespaces();
            var c = slice.CurrentChar;

            if ( c == ']' )
            {
                break;
            }

            if ( c == '\0' )
            {
                return false;
            }

            if ( !slice.MatchArgument( out var argument ) )
            {
                return false;
            }
            
            if ( string.IsNullOrEmpty( argument.Value.Value ) )
            {
                throw new InvalidOperationException( $"Argument '{argument.Value.Key}' is missing a value." );
            }

            switch ( argument.Value.Key )
            {
                case "name":
                    test.Name = argument.Value.Value;

                    break;

                case "title":
                    test.Title = argument.Value.Value;

                    break;

                case "tabs":
                    test.Tabs = TabsHelper.SplitTabs( argument.Value.Value );

                    break;

                default:
                    throw new InvalidOperationException( $"Unknown argument '{argument.Value.Key}'." );
            }
        }

        test.Span = new SourceSpan(
            processor.GetSourcePosition( saved.Start, out var line, out var column ),
            processor.GetSourcePosition( slice.Start - 1 ) );

        test.Line = line;
        test.Column = column;

        processor.Inline = test;

        return true;
    }
}