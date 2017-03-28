using System;

namespace wallchat.DAL.App
{
    public class Disposable : IDisposable
    {
        private bool isDisposed;

        public void Dispose ()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
        }

        ~Disposable ()
        {
            Dispose (false);
        }

        private void Dispose ( bool disposing )
        {
            if ( !isDisposed && disposing )
                DisposeCore ( );

            isDisposed = true;
        }

        protected virtual void DisposeCore ()
        {
        }
    }
}