// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.IO;
using System.Threading;

namespace Doc.Disposable
{
    [Disposable]
    internal class Foo
    {
        private CancellationTokenSource _cancellationTokenSource = new();
    }

    [Disposable]
    internal class Bar : Foo
    {
        private MemoryStream _stream = new();
    }
}