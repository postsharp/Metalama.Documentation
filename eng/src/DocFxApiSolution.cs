namespace BuildMetalamaDocumentation;

internal class DocFxApiSolution : DocFxSolutionBase
{
    public DocFxApiSolution( string solutionPath ) : base( solutionPath, "metadata" )
    {
        this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Build;
    }
}
