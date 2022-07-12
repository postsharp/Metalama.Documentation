using System;
using System.IO;
using System.Threading;

namespace Doc.Disposable
{
    [Disposable]
    internal class Foo : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource = new();

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._cancellationTokenSource?.Dispose();
        }
    }

    [Disposable]
    internal class Bar : Foo
    {
        private FileSystemWatcher _cancellationTokenSource = new();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this._cancellationTokenSource?.Dispose();
        }
    }
}