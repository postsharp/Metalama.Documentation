using Markdig.Renderers;
using Markdig.Renderers.Html;
using Metalama.Documentation.Markdig.Extensions.Sandbox;
using Metalama.Documentation.Markdig.Extensions.Tabs;
using System.Collections.Immutable;

namespace Metalama.Documentation.Markdig.Extensions.AspectTests;

public class HtmlAspectTestInlineRenderer : HtmlObjectRenderer<AspectTestInline>
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

    protected override void Write( HtmlRenderer renderer, AspectTestInline obj )
    {
        if ( !File.Exists( obj.Src ) )
        {
            throw new FileNotFoundException( $"The file '{obj.Src}' does not exist." );
        }

        var id = Path.GetFileNameWithoutExtension( obj.Src ).ToLowerInvariant().Replace( '.', '_' );
        var directory = Path.GetDirectoryName( obj.Src )!;

        var tabGroup = new AspectTestTabGroup( id );

        void AddCodeTab( string tabId, string suffix, SandboxFileKind kind )
        {
            var tabPath = suffix == "" ? obj.Src : Path.ChangeExtension( obj.Src, suffix + ".cs" );

            if ( File.Exists( tabPath ) )
            {
                if ( kind == SandboxFileKind.TargetCode )
                {
                    tabGroup.Tabs.Add( new CompareTab( tabId, "Target Code", tabPath ) );
                }
                else
                {
                    tabGroup.Tabs.Add( new CodeTab( tabId, tabPath, suffix, kind ) );
                }
            }
        }

        void AddOtherTab( string extension, Func<string, BaseTab> createTab )
        {
            var tabPath = Path.ChangeExtension( obj.Src, extension );

            if ( File.Exists( tabPath ) )
            {
                tabGroup.Tabs.Add( createTab( tabPath ) );
            }
        }

        foreach ( var file in Directory.GetFiles( directory, $"{id}.*.cs" )
                     .OrderBy( x => x ) )
        {
            if ( file.EndsWith( ".t.cs", StringComparison.Ordinal ) )
            {
                continue;
            }

            var text = File.ReadAllText( file );
            var isCompileTime = _compileTimeNamespaces.Any( ns => text.Contains( ns, StringComparison.Ordinal ) );

            var fileName = Path.GetFileNameWithoutExtension( file );
            var fileKind = fileName.Substring( fileName.LastIndexOf( '.' ) + 1 );

            var sandboxFileKind = (fileKind.ToLowerInvariant(), isCompileTime)
                switch
                {
                    ("dependency", _) => SandboxFileKind.Incompatible,
                    (_, true) => SandboxFileKind.AspectCode,
                    (_, false) => SandboxFileKind.ExtraCode,
                };

            AddCodeTab( fileKind.ToLowerInvariant(), fileKind, sandboxFileKind );
        }

        AddCodeTab( "target", "", SandboxFileKind.TargetCode );

        AddOtherTab( ".t.txt", p => new ProgramOutputTab( p ) );

        tabGroup.Render( renderer, obj );
    }
}