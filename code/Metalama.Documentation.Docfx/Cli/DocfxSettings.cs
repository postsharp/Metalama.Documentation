using Spectre.Console.Cli;

namespace Metalama.Documentation.Docfx.Cli;

public class DocfxSettings : CommandSettings
{
    [CommandArgument( 0, "<config-path>" )]
    public string ConfigurationPath { get; set; } = null!;
}