namespace BuildMetalamaDocumentation;

internal class DocFxApiSolution : DocFxSolutionBase
{
    public DocFxApiSolution( string solutionPath ) : base( solutionPath, "api" )
    {
        this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Build;
    }
}
