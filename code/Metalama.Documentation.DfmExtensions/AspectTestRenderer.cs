// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Metalama.Documentation.DfmExtensions;

internal class AspectTestRenderer : BaseRenderer<AspectTestToken>
{
    private static readonly ImmutableHashSet<string> _compileTimeNamespaces = ImmutableHashSet.Create<string>(
        StringComparer.Ordinal,
        "using Metalama.Framework.Aspects",
        "using Metalama.Framework.Code",
        "using Metalama.Framework.Diagnostics",
        "using Metalama.Framework.Engine",
        "using Metalama.Framework.Fabrics",
        "using Metalama.Framework.Project",
        "using Metalama.Framework.Services",
        "using Metalama.Framework.Validation",
        "using Metalama.Framework.Options",
        "using Metalama.Framework.Metrics",
        "using Metalama.Framework.Eligibility",
        "using Metalama.Framework.Advising",
        "using Metalama.Framework.Serialization",
        "using Metalama.Framework.CodeFixes" );

    public override string Name => nameof(AspectTestRenderer);

    protected override StringBuffer RenderCore(
        AspectTestToken token,
        MarkdownBlockContext context )
    {
        if ( !File.Exists( token.Src ) )
        {
            throw new FileNotFoundException( $"The file '{token.Src}' does not exist." );
        }

        var id = Path.GetFileNameWithoutExtension( token.Src ).ToLowerInvariant().Replace( '.', '_' );
        var directory = Path.GetDirectoryName( token.Src )!;

        var tabGroup = new AspectTestTabGroup( id );

        void AddCodeTab( string tabId, string suffix, SandboxFileKind sandboxFileKind, DiffSide diffSide )
        {
            var tabPath = suffix == "" ? token.Src : Path.ChangeExtension( token.Src, suffix + ".cs" );

            switch ( diffSide )
            {
                case DiffSide.Both:
                    tabGroup.Tabs.Add( new CompareTab( tabId, "Target Code", tabPath ) );

                    break;

                case DiffSide.Source:
                    tabGroup.Tabs.Add( new CodeTab( tabId, tabPath, suffix, sandboxFileKind ) );

                    break;

                case DiffSide.Transformed:
                    tabGroup.Tabs.Add( new TransformedSingleFileCodeTab( tabPath ) );

                    break;
            }
        }

        void AddOtherTab( string extension, Func<string, BaseTab> createTab )
        {
            var tabPath = Path.ChangeExtension( token.Src, extension );

            if ( File.Exists( tabPath ) )
            {
                tabGroup.Tabs.Add( createTab( tabPath ) );
            }
        }

        foreach ( var file in Directory.GetFiles( directory, $"{id}.*.cs" )
                     .OrderBy( x => x ) )
        {
            if ( file.EndsWith( ".t.cs" ) )
            {
                continue;
            }

            var text = File.ReadAllText( file );
            var isCompileTime = _compileTimeNamespaces.Any( ns => text.Contains( ns ) );

            var fileName = Path.GetFileNameWithoutExtension( file );
            var fileNameParts = fileName.Split( '.' );
            var fileKind = fileNameParts[^1];
            var fileSuffix = string.Join( ".", fileNameParts.Skip( 1 ) );

            var (sandboxFileKind,diffSide) = (fileKind.ToLowerInvariant(), isCompileTime)
                switch
                {
                    ("i", _) => (SandboxFileKind.None, DiffSide.Transformed), // Introduced code
                    ("dependency", _) => (SandboxFileKind.Incompatible, DiffSide.Both),
                    (_, true) => (SandboxFileKind.AspectCode, DiffSide.Source),
                    (_, false) => (SandboxFileKind.ExtraCode, DiffSide.Source)
                };

            AddCodeTab( fileKind.ToLowerInvariant(), fileSuffix, sandboxFileKind, diffSide );
        }

        AddCodeTab( "target", "", SandboxFileKind.TargetCode, DiffSide.Both );

        AddOtherTab( ".t.txt", p => new ProgramOutputTab( p ) );

        var stringBuilder = new StringBuilder();
        tabGroup.Render( stringBuilder, token );

        return stringBuilder.ToString();
    }
}