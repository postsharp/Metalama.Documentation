using Docfx.Dotnet;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace Metalama.Documentation.Docfx.Cli;

[UsedImplicitly]
public class MetadataCommand : AsyncCommand<DocfxSettings>
{
    public override async Task<int> ExecuteAsync( CommandContext context, DocfxSettings settings )
    {
        await DotnetApiCatalog.GenerateManagedReferenceYamlFiles( settings.ConfigurationPath );
        
        return 0;
    }
}