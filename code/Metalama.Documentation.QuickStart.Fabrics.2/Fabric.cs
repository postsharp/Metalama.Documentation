using Metalama.Documentation.QuickStart;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Code;

internal partial class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender project )
    {
        this.AddLogging( project );
        this.AddNotifyPropertyChanged( project );
    }
    
    private void AddNotifyPropertyChanged( IProjectAmender project )
    {
        // Add to namespace.
        project.With( p => p.GlobalNamespace.GetDescendant( "My.Namsepsace" ).DescendantsAndSelf().SelectMany( ns=> ns.Types ) ).AddAspect(...);

        // Add to any derived type.
        project.With( p => p.GetDerivedTypes( typeof( BaseClass ) ) ).AddAspectIfEligible<LogAttribute>();
    }
}

class BaseClass
{

}
