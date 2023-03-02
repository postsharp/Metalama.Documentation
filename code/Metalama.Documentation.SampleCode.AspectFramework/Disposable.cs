
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