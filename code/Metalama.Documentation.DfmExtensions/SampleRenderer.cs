
using HtmlAgilityPack;
using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;
using Newtonsoft.Json;
using PKT.LZStringCSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Metalama.Documentation.DfmExtensions;

internal class SampleRenderer : DfmCustomizedRendererPartBase<IMarkdownRenderer, SampleToken, MarkdownBlockContext>
{
    private static int _nextId = 1;

    public override string Name => nameof(SampleRenderer);

    public override bool Match( IMarkdownRenderer renderer, SampleToken token, MarkdownBlockContext context )
    {
        return true;
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

    private static string FindParentDirectory( string directory, Predicate<string> predicate )
    {
        if ( predicate( directory ) )
        {
            return directory;
        }
        else
        {
            var parentDirectory = Path.GetDirectoryName( directory );

            if ( parentDirectory == null )
            {
                throw new InvalidOperationException( $"Cannot get the directory of '{directory}'." );
            }

            return FindParentDirectory( parentDirectory, predicate );
        }
    }

    public override StringBuffer Render(
        IMarkdownRenderer renderer,
        SampleToken token,
        MarkdownBlockContext context )
    {
        var baseDirectory = (string) context.Variables["BaseFolder"];
        var path = token.Src.Replace( "~", baseDirectory );
        var id = "code-" + Path.GetFileNameWithoutExtension( path ).ToLowerInvariant();

        string CreateCodeBlock( string content )
        {
            return $"<div id={id} class=\"anchor\">{content}</div>";
        }

        var referencingFile =
            Path.GetFullPath( Path.Combine( baseDirectory, token.SourceInfo.File ) );

        var shortFileNameWithoutExtension = Path.GetFileNameWithoutExtension( path );
        var targetPath = Path.GetFullPath( Path.Combine( Path.GetDirectoryName( referencingFile )!, path ) );
        var programOutputPath = Path.ChangeExtension( targetPath, ".t.txt" );

        // Find the directories.
        var targetDirectory = Path.GetDirectoryName( targetPath );

        if ( targetDirectory == null )
        {
            throw new InvalidOperationException( $"Cannot get the directory of '{targetPath}'." );
        }

        var projectDir = FindParentDirectory(
            targetDirectory,
            directory => Directory.GetFiles( directory, "*.csproj" ).Length > 0 );

        var gitDirectory = FindParentDirectory(
            targetDirectory,
            directory => Directory.Exists( Path.Combine( directory, ".git" ) ) );

        var targetPathRelativeToProjectDir = GetRelativePath( projectDir, targetPath );
        var sourceDirectoryRelativeToGitDir = GetRelativePath( gitDirectory, targetDirectory );

        var aspectHtmlPath = Path.GetFullPath(
            Path.Combine(
                projectDir,
                "obj",
                "html",
                "net6.0",
                Path.ChangeExtension( targetPathRelativeToProjectDir, ".Aspect.cs.html" ) ) );

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

        if ( !File.Exists( transformedHtmlPath ) )
        {
            // Try an alternate extension.
            transformedHtmlPath = Path.GetFullPath(
                Path.Combine(
                    projectDir,
                    "obj",
                    "html",
                    "net6.0",
                    Path.ChangeExtension( targetPathRelativeToProjectDir, ".cs.out.html" ) ) );
        }

        var currentFile = ((ImmutableStack<string>) context.Variables["FilePathStack"]).Peek();
        const string conceptualPrefix = "content/";

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

        var permalink = "https://doc.metalama.net/" + currentFileId + "#" + id;

        const string gitBranch = "master";
        const string gitHubProjectPath = "https://github.com/postsharp/Metalama.Documentation/blob/" + gitBranch;
        const string tryBaseUrl = "https://try.metalama.net/#";

        if ( File.Exists( transformedHtmlPath ) )
        {
            // Create the tab group with the aspect, target, and transformed code.

            bool IsTabEnabled( string id ) => token.Tabs.Length == 0 || token.Tabs.Contains( id );

            var targetHtml = File.ReadAllText( targetHtmlPath );
            var transformedHtml = File.ReadAllText( transformedHtmlPath );

            string Html2Text( string html )
            {
                var doc = new HtmlDocument();
                doc.LoadHtml( html );

                return HtmlEntity.DeEntitize( doc.DocumentNode.SelectSingleNode( "//pre" ).InnerText );
            }

            var targetCs = Html2Text( targetHtml );

            var tryFiles = new List<TryPayloadFile> { new( "Program.cs", targetCs, TryFileKind.TargetCode ) };

            var snippetId = Interlocked.Increment( ref _nextId ).ToString();

            var tabHeaders = new StringBuilder();
            var tabBodies = new StringBuilder();
            string? lastContent = null;
            var tabCount = 0;

            void AppendTab( string tabId, string header, string content )
            {
                // Ignore the tab if it is not enabled.
                if ( !IsTabEnabled( tabId ) )
                {
                    return;
                }

                // Ignore any empty tab.
                if ( string.IsNullOrWhiteSpace( content ) )
                {
                    return;
                }

                // Ignore any tab that has only comments.
                var code = Html2Text( content ).Split( new[] { '\r', '\n' } );

                if ( code.All(
                        line =>
                        {
                            var trimmed = line.TrimStart();

                            return string.IsNullOrWhiteSpace( trimmed ) || trimmed.StartsWith( "//" );
                        } ) )
                {
                    return;
                }

                // Append the tab if it is not ignored.
                tabHeaders.Append( $"<li><a href=\"#tabpanel_{snippetId}_{tabId}\">{header}</a></li>" );
                tabBodies.Append( $"<div id=\"tabpanel_{snippetId}_{tabId}\">{content}</div>" );
                lastContent = content;
                tabCount++;
            }

            void AppendAdditionalTab( string suffix, string? header = null )
            {
                var additionalHtmlPath = Path.GetFullPath(
                    Path.Combine(
                        projectDir,
                        "obj",
                        "html",
                        "net6.0",
                        Path.ChangeExtension( targetPathRelativeToProjectDir, $".{suffix}.cs.html" ) ) );

                if ( File.Exists( additionalHtmlPath ) )
                {
                    var additionalHtml = File.ReadAllText( additionalHtmlPath );
                    AppendTab( suffix.ToLower(), header ?? $"{suffix} Code", additionalHtml );
                    var additionalCs = Html2Text( additionalHtml );

                    tryFiles.Add( new TryPayloadFile( $"{suffix}.cs", additionalCs, TryFileKind.ExtraCode ) );
                }
            }

            string gitUrlExtension;
            var canTryOnline = true;

            if ( File.Exists( aspectHtmlPath ) )
            {
                var aspectHtml = File.ReadAllText( aspectHtmlPath );
                AppendTab( "aspect", "Aspect Code", aspectHtml );
                var aspectCs = Html2Text( aspectHtml );
                gitUrlExtension = ".Aspect.cs";

                tryFiles.Add( new TryPayloadFile( "Aspect.cs", aspectCs, TryFileKind.AspectCode ) );
            }
            else
            {
                gitUrlExtension = ".cs";
            }

            AppendAdditionalTab( "Fabric" );

            AppendTab( "target", "Target Code", targetHtml );

            AppendAdditionalTab( "Additional" );
            
            AppendTab( "transformed", "Transformed Code", transformedHtml );

            if ( File.Exists( programOutputPath ) )
            {
                AppendTab(
                    "output",
                    "Program Output",
                    "<pre class=\"program-output\">" + File.ReadAllText( programOutputPath ) + "</pre>" );
            }
            
            // We cannot try online if there is a dependency.
            if ( File.Exists( Path.ChangeExtension( targetPath, $".Dependency.cs" ) ) ) 
            {
                canTryOnline = false;
            }

            // Create the links
            var links = new StringBuilder();
            links.AppendLine( @"<div class=""sample-links tabbed"">" );

            var gitUrl = gitHubProjectPath + "/" + sourceDirectoryRelativeToGitDir + "/" +
                         shortFileNameWithoutExtension + gitUrlExtension;

            if ( canTryOnline )
            {
                var tryPayloadJson = JsonConvert.SerializeObject( new TryPayload( tryFiles ) );
                var tryPayloadHash = LZString.CompressToEncodedURIComponent( tryPayloadJson );
                var tryUrl = tryBaseUrl + tryPayloadHash;

                links.AppendLine( $@"  <a class=""try"" href=""{tryUrl}"" target=""try"">Try Online</a> |" );
            }

            links.AppendLine(
                @$"
    <a class=""github"" href=""{gitUrl}"" target=""github"">See on GitHub</a> | 
    <a class=""permalink"" href=""{permalink}"" target=""doc"">Permalink</a>
</div>" );

            var tabs = tabCount > 1
                ? @$"
<div class=""tabGroup"">
    <ul>
    {tabHeaders}
    </ul>
    {tabBodies}
</div>"
                : lastContent;

            return CreateCodeBlock( links + tabs );
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