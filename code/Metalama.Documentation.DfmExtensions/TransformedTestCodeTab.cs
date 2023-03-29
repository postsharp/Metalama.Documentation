﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Metalama.Documentation.DfmExtensions;

internal class TransformedTestCodeTab : CodeTab
{
    public TransformedTestCodeTab( string fullPath ) : base( "transformed", fullPath, "Transformed", SandboxFileKind.None ) { }
}