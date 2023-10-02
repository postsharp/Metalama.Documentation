// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

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

        var id = Path.GetFileNameWithoutExtension( token.Src ).ToLowerInvariant();
        var directory = Path.GetDirectoryName( token.Src )!;

        var tabGroup = new AspectTestTabGroup( id );

        void AddCodeTab( string tabId, string suffix, SandboxFileKind kind )
        {
            var tabPath = suffix == "" ? token.Src : Path.ChangeExtension( token.Src, suffix + ".cs" );

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
            var fileKind = fileName.Substring( fileName.LastIndexOf( '.' ) + 1 );

            var sandboxFileKind = (fileKind.ToLowerInvariant(), isCompileTime )
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

        var stringBuilder = new StringBuilder();
        tabGroup.Render( stringBuilder, token );

        return stringBuilder.ToString();
    }
}