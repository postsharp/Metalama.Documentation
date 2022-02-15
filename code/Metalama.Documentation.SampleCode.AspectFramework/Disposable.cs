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
        private FileSystemWatcher _cancellationTokenSource = new();
    }
}