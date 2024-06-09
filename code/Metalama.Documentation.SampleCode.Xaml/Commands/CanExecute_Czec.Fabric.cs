// This is public domain Metalama sample code.

using System.Windows;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Xaml;
using Metalama.Patterns.Xaml.Configuration;

namespace Doc.Command.CanExecute_Czech;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureCommand(
            builder =>
            {
                builder.AddNamingConvention(
                    new CommandNamingConvention( "czech-1" )
                    {
                        CommandNamePattern = "^Vykonat(.*)$",
                        CanExecutePatterns = ["^MůzemeVykonat{CommandName}$"],
                        CommandPropertyName = "{CommandName}Příkaz"
                    } );
                
                builder.AddNamingConvention(
                    new CommandNamingConvention( "czech-2" )
                    {
                        CanExecutePatterns = ["^Můzeme{CommandName}$"],
                        CommandPropertyName = "{CommandName}Příkaz"
                    } );
            } );
    }
}