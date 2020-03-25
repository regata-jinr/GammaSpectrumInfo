using System;
using CanberraDataAccessLib;

namespace GSI.Core
{
    public class Spectra : IDisposable
    {
        private IDataAccess _spectra;

        public Spectra(string pathToCnf)
        {
            _spectra = new DataAccess();
            _spectra.Open(pathToCnf);

            Sample = new SampleInfo(_spectra);

            _spectra.Close();
            _spectra.Close();

        }

        public readonly SampleInfo Sample;

        private bool _isDisposed = false;

        private void Dispose(bool isDisposing)
        {

            if (!_isDisposed)
            {
                if (isDisposing)
                {
                }
                _spectra.Close();
            }
            _isDisposed = true;
        }

        ~Spectra()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    } //  public class Spectra : IDisposable
} // namespace GSI.Core

