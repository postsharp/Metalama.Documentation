using System.Windows;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Xaml;
using Metalama.Patterns.Xaml.Configuration;

namespace Doc.Command.CanExecute;

public class MojeOkno : Window
{
    public int Počitadlo { get; private set; }

    [Command]
    public void Zvýšit()
    {
        this.Počitadlo++;
    }

    public bool MůzemeZvýšit => this.Počitadlo < 10;

    [Command]
    public void Snížit()
    {
        this.Počitadlo--;
    }

    public bool MůzemeSnížit => this.Počitadlo > 0;
}

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureCommand( builder => builder.AddNamingConvention( new CommandNamingConvention( "czech" ) { CanExecutePattern = "^Můzeme{CommandName}$" } ) );
    }
}