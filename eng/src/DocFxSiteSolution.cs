using PostSharp.Engineering.BuildTools.Build;
using System.IO;
using System.IO.Compression;

namespace BuildMetalamaDocumentation;

internal class DocFxSiteSolution : DocFxSolutionBase
{
    private readonly string _archiveName;

    public DocFxSiteSolution( string solutionPath, string archiveName ) : base( solutionPath, "build" )
    {
        // Packing is done by the publish command.
        this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Pack;
        this._archiveName = archiveName;
    }

    public override bool Pack( BuildContext context, BuildSettings settings )
    {
        if ( !this.Build( context, settings ) )
        {
            return false;
        }

        var zipPath = Path.Combine( context.RepoDirectory, "artifacts", "publish", "private", this._archiveName );

        if ( File.Exists( zipPath ) )
        {
            File.Delete( zipPath );
        }

        ZipFile.CreateFromDirectory(
            Path.Combine( context.RepoDirectory, "artifacts", "site" ),
            zipPath );

        return true;
    }
}