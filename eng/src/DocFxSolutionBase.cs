using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Utilities;
using System.Collections.Immutable;
using System.IO;
using System.Text.RegularExpressions;

namespace BuildMetalamaDocumentation;

internal abstract class DocFxSolutionBase : Solution
{
    private readonly string _command;

    public DocFxSolutionBase( string solutionPath, string command ) : base( solutionPath )
    {
        this._command = command;
    }

    public sealed override bool Build( BuildContext context, BuildSettings settings )
    {
        var options = new ToolInvocationOptions()
        {
            ErrorPatterns = ToolInvocationOptions.Default.ErrorPatterns.Add( new Regex( @"Markup failed" ) ),
            WarningPatterns = ToolInvocationOptions.Default.WarningPatterns,
            SilentPatterns =
                settings.Verbosity == Verbosity.Detailed
                    ? ImmutableArray<Regex>.Empty
                    : ImmutableArray.Create( new Regex( @": info :" ), new Regex( @"\]Info:" ) ),
            ReplacePatterns = ImmutableArray.Create(

                // Replace pattern when the path is present but not the line
                new ReplacePattern(
                    new Regex( @"(?<time>\[[^\]]*\])(?<severity>\w+):(?<component>\[[^\]]*\])\((~\/)?(?<path>[^\)#]+)\)(?<message>.*)$" ),
                    match => $"{match.Groups["path"]}: {match.Groups["severity"].Value.ToLowerInvariant()}: {match.Groups["message"]}" ),

                // Replace pattern when the path is present including the line.
                new ReplacePattern(
                    new Regex( @"(?<time>\[[^\]]*\])(?<severity>\w+):(?<component>\[[^\]]*\])\((~\/)?(?<path>[^\)#]+)#L(?<line>\d+)\)(?<message>.*)$" ),
                    match
                        => $"{match.Groups["path"]}({match.Groups["line"]}): {match.Groups["severity"].Value.ToLowerInvariant()}: {match.Groups["message"]}" ),

                // Replace pattern without the path
                new ReplacePattern(
                    new Regex( @"(?<time>\[[^\]]*\])(?<severity>\w+):(?<component>\[[^\]]*\])\((~\/)?(?<message>.*)$" ),
                    match => $"{this.SolutionPath}: {match.Groups["severity"].Value.ToLowerInvariant()}: {match.Groups["message"]}" ) )
        };

        var docfxPath = Path.Combine( context.RepoDirectory, "artifacts", "docfx", "docfx.dll" );

        return DotNetInvocationHelper.Run( context, $"{docfxPath} {this._command}", Path.Combine( context.RepoDirectory, this.SolutionPath ), options );
    }

    public override bool Pack( BuildContext context, BuildSettings settings ) => true;

    public override bool Test( BuildContext context, BuildSettings settings ) => true;

    public override bool Restore( BuildContext context, BuildSettings settings ) => true;
}