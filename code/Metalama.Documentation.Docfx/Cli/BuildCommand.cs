using Docfx;
using JetBrains.Annotations;
using Markdig.Parsers;
using Markdig.Parsers.Inlines;
using Metalama.Documentation.Docfx.Markdig.AspectTests;
using Metalama.Documentation.Docfx.Markdig.Comments;
using Metalama.Documentation.Docfx.Markdig.CompareFile;
using Metalama.Documentation.Docfx.Markdig.MultipleFiles;
using Metalama.Documentation.Docfx.Markdig.ProjectButtons;
using Metalama.Documentation.Docfx.Markdig.SingleFiles;
using Metalama.Documentation.Docfx.Markdig.Vimeo;
using Spectre.Console.Cli;

namespace Metalama.Documentation.Docfx.Cli;

[UsedImplicitly]
public class BuildCommand : AsyncCommand<DocfxSettings>
{
    public override async Task<int> ExecuteAsync( CommandContext context, DocfxSettings settings )
    {
        var options = new BuildOptions
        {
            ConfigureMarkdig = markdig =>
            {
                // Disable HtmlBlockParser to let inline elemets in HTML blocks be parsed.
                // E.g. Xref links. (<xref:...>)
                var htmlBlockParser = markdig.BlockParsers.Find<HtmlBlockParser>();

                if ( htmlBlockParser != null )
                {
                    markdig.BlockParsers.Remove( htmlBlockParser );
                }

                // Enable HTML parsing in AutolinkInlineParser to prevent escaping of HTML tags.
                // (This has nothing to do with autolink parsing, but the AutoplinkInlineParser provides this feature.)
                var autolinkInlineParser = markdig.InlineParsers.Find<AutolinkInlineParser>()!;
                autolinkInlineParser.EnableHtmlParsing = true;
                
                markdig.Extensions.AddIfNotAlready<AspectTestInlineExtension>();
                markdig.Extensions.AddIfNotAlready<SingleFileInlineExtension>();
                markdig.Extensions.AddIfNotAlready<CompareFileInlineExtension>();
                markdig.Extensions.AddIfNotAlready<ProjectButtonsInlineExtension>();
                markdig.Extensions.AddIfNotAlready<MultipleFilesInlineExtension>();
                markdig.Extensions.AddIfNotAlready<VimeoInlineExtension>();
                markdig.Extensions.AddIfNotAlready<CommentBlockExtension>();

                return markdig;
            }
        };

        await Docset.Build( settings.ConfigurationPath, options );

        return 0;
    }
}