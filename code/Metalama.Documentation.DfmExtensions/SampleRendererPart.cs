// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using HtmlAgilityPack;
using Microsoft.DocAsCode.Common;
using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;
using Newtonsoft.Json;
using PKT.LZStringCSharp;
using System;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading;

namespace Metalama.Documentation.DfmExtensions;

public class SampleRendererPart : DfmCustomizedRendererPartBase<IMarkdownRenderer, DfmIncludeBlockToken,
    MarkdownBlockContext>
{
    private static int _nextId = 1;

    public override string Name => nameof(SampleRendererPart);

    static SampleRendererPart()
    {
//            Debugger.Launch();
    }

    public override bool Match( IMarkdownRenderer renderer, DfmIncludeBlockToken token, MarkdownBlockContext context )
    {
        return TryParseToken( token, out _ );
    }

    private static string HtmlEncode( string s )
    {
        var stringBuilder = new StringBuilder( s.Length );

        foreach ( var c in s )
        {
            switch ( c )
            {
                case '<':
                    stringBuilder.Append( "&lt;" );

                    break;

                case '>':
                    stringBuilder.Append( "&gt;" );

                    break;

                case '&':
                    stringBuilder.Append( "&amp;" );

                    break;

                default:
                    stringBuilder.Append( c );

                    break;
            }
        }

        return stringBuilder.ToString();
    }

    private static bool TryParseToken(
        DfmIncludeBlockToken token,
        out ( string FilePath, string Fragment, NameValueCollection Query, string Id ) parsed )
    {
        var targetFileName = UriUtility.GetPath( token.Src );

        if ( !targetFileName.EndsWith( ".cs" ) )
        {
            parsed = default;

            return false;
        }

        var parameters = new NameValueCollection();

        if ( UriUtility.HasQueryString( token.Src ) )
        {
            foreach ( var part in UriUtility.GetQueryString( token.Src ).Split( '&' ) )
            {
                var nameValuePair = part.Split( '=' );

                parameters.Add( nameValuePair[0], nameValuePair.Length > 1 ? nameValuePair[1] : null );
            }
        }

        var fragment = UriUtility.GetFragment( token.Src );
        var id = "code-" + Path.GetFileNameWithoutExtension( targetFileName ).ToLowerInvariant();

        if ( !string.IsNullOrEmpty( fragment ) )
        {
            id += "-" + fragment;
        }

        parsed = (targetFileName, fragment, parameters, id);

        return true;
    }

    private static string FindParentDirectory( string directory, Predicate<string> predicate )
    {
        if ( directory == null )
        {
            return null;
        }

        if ( predicate( directory ) )
        {
            return directory;
        }
        else
        {
            var parentDirectory = Path.GetDirectoryName( directory );

            return FindParentDirectory( parentDirectory, predicate );
        }
    }

    public override StringBuffer Render(
        IMarkdownRenderer renderer,
        DfmIncludeBlockToken token,
        MarkdownBlockContext context )
    {
        TryParseToken( token, out var source );

        string CreateCodeBlock( string content ) => $"<div id={source.Id} class=\"anchor\">{content}</div>";

        var referencingFile =
            Path.GetFullPath( Path.Combine( (string) context.Variables["BaseFolder"], token.SourceInfo.File ) );

        var shortFileNameWithoutExtension = Path.GetFileNameWithoutExtension( source.FilePath );
        var targetPath = Path.GetFullPath( Path.Combine( Path.GetDirectoryName( referencingFile ), source.FilePath ) );
        var programOutputPath = Path.ChangeExtension( targetPath, ".t.txt" );

        // Find the directories.
        var projectDir = FindParentDirectory(
            Path.GetDirectoryName( targetPath ),
            directory => Directory.GetFiles( directory, "*.csproj" ).Length > 0 );

        var gitDirectory = FindParentDirectory(
            Path.GetDirectoryName( targetPath ),
            directory => Directory.Exists( Path.Combine( directory, ".git" ) ) );

        var targetPathRelativeToProjectDir = GetRelativePath( projectDir, targetPath );
        var sourceDirectoryRelativeToGitDir = GetRelativePath( gitDirectory, Path.GetDirectoryName( targetPath ) );

        var aspectHtmlPath = Path.GetFullPath(
            Path.Combine(
                projectDir,
                "obj",
                "html",
                "net6.0",
                Path.ChangeExtension( targetPathRelativeToProjectDir, ".Aspect.cs.html" ) ) );

        var additionalHtmlPath = Path.GetFullPath(
            Path.Combine(
                projectDir,
                "obj",
                "html",
                "net6.0",
                Path.ChangeExtension( targetPathRelativeToProjectDir, ".Additional.cs.html" ) ) );

        var targetHtmlPath = Path.GetFullPath(
            Path.Combine(
                projectDir,
                "obj",
                "html",
                "net6.0",
                Path.ChangeExtension( targetPathRelativeToProjectDir, ".cs.html" ) ) );

        var transformedHtmlPath = Path.GetFullPath(
            Path.Combine(
                projectDir,
                "obj",
                "html",
                "net6.0",
                Path.ChangeExtension( targetPathRelativeToProjectDir, ".out.cs.html" ) ) );

        var currentFile = ((ImmutableStack<string>) context.Variables["FilePathStack"]).Peek();
        const string conceptualPrefix = "../conceptual/";

        if ( currentFile.StartsWith( conceptualPrefix ) )
        {
            currentFile = currentFile.Substring( conceptualPrefix.Length );
        }
        else
        {
            // This is a namespace topic and it does not have a prefix.
        }

        var currentFileId = currentFile.Substring(
                0,
                currentFile.Length - ".md".Length )
            .ToLowerInvariant();

        var permalink = "https://doc.metalama.net/" + currentFileId + "#" + source.Id;

        const string gitBranch = "master";
        const string gitHubProjectPath = "https://github.com/postsharp/Metalama.Documentation/blob/" + gitBranch;
        const string tryBaseUrl = "https://try.metalama.net/#";

        if ( File.Exists( transformedHtmlPath ) )
        {
            // Create the tab group with the aspect, target, and transformed code.

            var targetHtml = File.ReadAllText( targetHtmlPath );
            var transformedHtml = File.ReadAllText( transformedHtmlPath );

            string Html2Text( string html )
            {
                var doc = new HtmlDocument();
                doc.LoadHtml( html );

                return HtmlEntity.DeEntitize( doc.DocumentNode.SelectSingleNode( "//pre" ).InnerText );
            }

            var targetCs = Html2Text( targetHtml );

            var snippetId = Interlocked.Increment( ref _nextId ).ToString();

            var tabHeaders = new StringBuilder();
            var tabBodies = new StringBuilder();

            void AppendTab( string tabId, string header, string content )
            {
                tabHeaders.Append( $"<li><a href=\"#tabpanel_{snippetId}_{tabId}\">{header}</a></li>" );
                tabBodies.Append( $"<div id=\"tabpanel_{snippetId}_{tabId}\">{content}</div>" );
            }

            string aspectCs;
            string gitUrlExtension;

            if ( File.Exists( aspectHtmlPath ) )
            {
                var aspectHtml = File.ReadAllText( aspectHtmlPath );
                AppendTab( "aspect", "Aspect Code", aspectHtml );
                aspectCs = Html2Text( aspectHtml );
                gitUrlExtension = ".Aspect.cs";
            }
            else
            {
                aspectCs = "";
                gitUrlExtension = ".cs";
            }

            AppendTab( "target", "Target Code", targetHtml );

            if ( File.Exists( additionalHtmlPath ) )
            {
                var programHtml = File.ReadAllText( additionalHtmlPath );
                AppendTab( "additional", "Additional Code", programHtml );

                // TODO: we should add this to the TryMetalama link, but TryMetalama does not support 3 buffers. 
            }

            AppendTab( "transformed", "Transformed Code", transformedHtml );

            if ( File.Exists( programOutputPath ) )
            {
                AppendTab( "output", "Program Output", "<pre class=\"program-output\">" + File.ReadAllText( programOutputPath ) + "</pre>" );
            }

            // Create the links
            var gitUrl = gitHubProjectPath + "/" + sourceDirectoryRelativeToGitDir + "/" +
                         shortFileNameWithoutExtension + gitUrlExtension;

            var tryPayloadJson = JsonConvert.SerializeObject( new { a = aspectCs, p = targetCs } );
            var tryPayloadHash = LZString.CompressToEncodedURIComponent( tryPayloadJson );
            var tryUrl = tryBaseUrl + tryPayloadHash;

            var totalContent =
                @$"
<div class=""sample-links tabbed"">
    <a class=""try"" href=""{tryUrl}"" target=""try"">Try Online</a> |
    <a class=""github"" href=""{gitUrl}"" target=""github"">See on GitHub</a> | 
    <a class=""permalink"" href=""{permalink}"" target=""doc"">Permalink</a>
</div>
<div class=""tabGroup"">
    <ul>
        {tabHeaders}
    </ul>
    {tabBodies}
</div>";

            return CreateCodeBlock( totalContent );
        }
        else
        {
            var gitUrl = gitHubProjectPath + "/" + sourceDirectoryRelativeToGitDir + "/" +
                         shortFileNameWithoutExtension + ".cs";

            var links = $@"
<div class=""sample-links"">
    <a class=""github"" href=""{gitUrl}"" target=""github"">See on GitHub</a>  |
    <a class=""permalink"" href=""{permalink}"" target=""doc"">Permalink</a>
</div>";

            if ( File.Exists( targetHtmlPath ) )
            {
                // Write the syntax-highlighted HTML instead.

                var html = File.ReadAllText( targetHtmlPath );

                return CreateCodeBlock( links + html );
            }
            else
            {
                return
                    CreateCodeBlock(
                        links +
                        $@"
<pre>
    <code class=""lang-csharp"" name=""{token.Name}"">{File.ReadAllText( targetPath )}</code>
</pre>" );
            }
        }
    }

    private static string GetRelativePath( string projectDir, string targetPath )
    {
        return new Uri( Path.Combine( projectDir, "_" ) ).MakeRelativeUri( new Uri( targetPath ) ).ToString();
    }
}