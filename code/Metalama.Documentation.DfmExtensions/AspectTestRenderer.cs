// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System;
using System.Collections.Generic;
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

    private enum TabOrder
    {
        Aspect,
        Target,
        Introduced,
        Dependency,
        Auxiliary,
        ProgramOutput
    }

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

        List<(BaseTab Tab, TabOrder Order)> tabs = [];

        void AddCodeTab(
            string tabId,
            string suffix,
            SandboxFileKind sandboxFileKind,
            DiffSide diffSide,
            string tabHeader,
            TabOrder order )
        {
            var tabPath = suffix == "" ? token.Src : Path.ChangeExtension( token.Src, suffix + ".cs" );

            switch ( diffSide )
            {
                case DiffSide.Both:
                    tabs.Add( (new CompareTab( tabId, tabHeader, tabPath ), order) );

                    break;

                case DiffSide.Source:
                    tabs.Add( (new CodeTab( tabId, tabPath, sandboxFileKind ), order) );

                    break;

                case DiffSide.Transformed:
                    tabs.Add( (new TransformedSingleFileCodeTab( tabId, tabPath, tabHeader ), order) );

                    break;
            }
        }

        void AddOtherTab( string extension, Func<string, BaseTab> createTab, TabOrder order )
        {
            var tabPath = Path.ChangeExtension( token.Src, extension );

            if ( File.Exists( tabPath ) )
            {
                tabs.Add( (createTab( tabPath ), order) );
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

            var (sandboxFileKind, diffSide, tabHeader, order) = (fileKind.ToLowerInvariant(), isCompileTime)
                switch
                {
                    ("i", _) => (SandboxFileKind.None, DiffSide.Transformed,
                                 fileNameParts[1] + " (Introduced)", TabOrder.Introduced), // Introduced code
                    ("dependency", _) => (SandboxFileKind.Incompatible, DiffSide.Both, "Referenced Project",
                                          TabOrder.Dependency),
                    (_, true) => (SandboxFileKind.AspectCode, DiffSide.Source, "Aspect Code", TabOrder.Aspect),
                    (_, false) => (SandboxFileKind.ExtraCode, DiffSide.Source, "Extra Code", TabOrder.ProgramOutput)
                };

            AddCodeTab( fileNameParts[1], fileSuffix, sandboxFileKind, diffSide, tabHeader, order );
        }

        AddCodeTab( "target", "", SandboxFileKind.TargetCode, DiffSide.Both, "Target Code", TabOrder.Target );

        AddOtherTab( ".t.txt", p => new ProgramOutputTab( p ), TabOrder.ProgramOutput );

        var stringBuilder = new StringBuilder();

        var tabGroup = new AspectTestTabGroup( id );
        tabGroup.Tabs.AddRange( tabs.OrderBy( t => (int) t.Order ).ThenBy( t => t.Tab.TabId ).Select( t => t.Tab ) );
        tabGroup.Render( stringBuilder, token );

        return stringBuilder.ToString();
    }
}