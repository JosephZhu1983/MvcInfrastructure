namespace MvcInfrastructure.Core
{
    using System;
    using System.Diagnostics;

    public abstract class Disposable : IDisposable
    {
        private bool disposed;

        [DebuggerStepThrough]
        ~Disposable()
        {
            Dispose(false);
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [DebuggerStepThrough]
        protected virtual void DisposeCore()
        {
        }

        [DebuggerStepThrough]
        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                DisposeCore();
            }

            disposed = true;
        }
    }
}
