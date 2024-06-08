// This is public domain Metalama sample code.

using System.Threading;

namespace Metalama.Documentation.DfmExtensions;

internal static class DebuggingHelper
{
    public static readonly Semaphore Semaphore = new( 1, 1 );
}