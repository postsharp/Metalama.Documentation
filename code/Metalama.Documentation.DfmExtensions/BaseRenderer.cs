// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;
using System.Diagnostics;

namespace Metalama.Documentation.DfmExtensions;

internal abstract class BaseRenderer<T> : DfmCustomizedRendererPartBase<IMarkdownRenderer, T, MarkdownBlockContext>
    where T : IMarkdownToken
{
    public override bool Match( IMarkdownRenderer renderer, T token, MarkdownBlockContext context ) => true;

    public sealed override StringBuffer Render( IMarkdownRenderer renderer, T token, MarkdownBlockContext context )
    {
        if ( Debugger.IsAttached )
        {
            DebuggingHelper.Semaphore.WaitOne();
        }

        try
        {
            return this.RenderCore( token, context );
        }
        finally
        {
            if ( Debugger.IsAttached )
            {
                DebuggingHelper.Semaphore.Release();
            }
        }
    }

    protected abstract StringBuffer RenderCore( T token, MarkdownBlockContext context );

    public override string Name => this.GetType().Name;
}