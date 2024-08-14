using Docfx;
using JetBrains.Annotations;
using Markdig;
using Markdig.Parsers;
using Markdig.Parsers.Inlines;
using Spectre.Console.Cli;

namespace Metalama.Documentation.Docfx.Cli;

[UsedImplicitly]
public class BuildCommand : AsyncCommand<DocfxSettings>
{
    public override async Task<int> ExecuteAsync( CommandContext context, DocfxSettings settings )
    {
        var options = new BuildOptions
        {
            ConfigureMarkdig = pipeline =>
            {
                // Disable HtmlBlockParser to let inline elemets in HTML blocks be parsed.
                // E.g. Xref links. (<xref:...>)
                var htmlBlockParser = pipeline.BlockParsers.Find<HtmlBlockParser>();

                if ( htmlBlockParser != null )
                {
                    pipeline.BlockParsers.Remove( htmlBlockParser );
                }

                // Enable HTML parsing in AutolinkInlineParser to prevent escaping of HTML tags.
                // (This has nothing to do with autolink parsing, but the AutoplinkInlineParser provides this feature.)
                var autolinkInlineParser = pipeline.InlineParsers.Find<AutolinkInlineParser>()!;
                autolinkInlineParser.EnableHtmlParsing = true;

                return pipeline;
            }
        };

        await Docset.Build( settings.ConfigurationPath, options );

        return 0;
    }
}