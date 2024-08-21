namespace BuildMetalamaDocumentation;

internal class DocFxMetadataSolution : DocFxSolutionBase
{
    public DocFxMetadataSolution( string solutionPath ) : base( solutionPath, "metadata" )
    {
        this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Build;
    }
}
