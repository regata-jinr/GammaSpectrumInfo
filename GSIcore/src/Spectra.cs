using System;
using System.IO;
using CanberraDataAccessLib;

namespace GSI.Core
{
    public class Spectra : IDisposable
    {
        private IDataAccess _spectra;

        public readonly string FileName;

        public string ErrorMessage;

        public readonly bool ReadSuccess;

        public Spectra(string pathToCnf)
        {
            try
            {
                FileName = pathToCnf;
                _spectra = new DataAccess();
                _spectra.Open(pathToCnf, OpenMode.dReadWrite);

                Sample = new SampleInfo(_spectra);

                if (string.IsNullOrEmpty(Sample.ErrorMessage))
                    ReadSuccess = true;
                else
                {
                    ReadSuccess = false;
                    ErrorMessage += Sample.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                ReadSuccess = false;
            }
            finally
            {
                Dispose(true);
            }

        }

        // FIXME: it smells...
        public override string ToString()
        {
            if (ReadSuccess)
                return String.Format($"{Path.GetFileNameWithoutExtension(FileName),7}{Sample.Id,-15}|{Sample.Title,-5}|{Sample.CollectorName,-15}|{Sample.Type,-5}|{Sample.BeginDate,-20}|{Sample.EndDate,-20}|{Sample.Quantity,-7}|{Sample.Uncertainty,-3}|{Sample.Units,-5}|{Sample.Geometry,-4}|{Sample.Description}|{ReadSuccess,5}");

            return String.Format($"{Path.GetFileNameWithoutExtension(FileName),7}{Sample.Id,-15}|{Sample.Title,-5}|{Sample.CollectorName,-15}|{Sample.Type,-5}|{Sample.BeginDate,-20}|{Sample.EndDate,-20}|{Sample.Quantity,-7}|{Sample.Uncertainty,-3}|{Sample.Units,-5}|{Sample.Geometry,-4}|{Sample.Description}|{ReadSuccess,5}|{ErrorMessage}");
        }

        public ViewModel viewModel
        {
            get
            {
                return new ViewModel
                {
                    File          = Path.GetFileNameWithoutExtension(this.FileName),
                    Id            = this.Sample.Id,
                    Title         = this.Sample.Title,
                    CollectorName = this.Sample.CollectorName,
                    Type          = this.Sample.Type,
                    Quantity      = this.Sample.Quantity,
                    Uncertainty   = this.Sample.Uncertainty,
                    Units         = this.Sample.Units,
                    Geometry      = this.Sample.Geometry,
                    BuildUpType   = this.Sample.BuildUpType,
                    BeginDate     = this.Sample.BeginDate,
                    EndDate       = this.Sample.EndDate,
                    Description   = this.Sample.Description,
                    ReadSuccess   = this.ReadSuccess,
                    ErrorMessage  = this.ErrorMessage,
                };
            }
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
                if (_spectra.IsOpen)
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

