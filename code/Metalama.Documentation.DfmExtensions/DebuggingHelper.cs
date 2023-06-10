// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.Threading;

namespace Metalama.Documentation.DfmExtensions;

internal static class DebuggingHelper
{
    public static readonly Semaphore Semaphore = new( 1, 1 );
}