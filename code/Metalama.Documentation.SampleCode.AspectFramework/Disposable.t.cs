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

        private void Dispose_Source(bool disposing)
        {
        }
    }

    [Disposable]
    internal class Bar : Foo, IDisposable
    {
        private FileSystemWatcher _cancellationTokenSource = new();

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this._cancellationTokenSource?.Dispose();
        }
    }
}