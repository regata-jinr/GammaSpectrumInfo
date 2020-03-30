using System;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSI.Core;
using System.Threading;

namespace GSI.UI
{
    // TODO: add deadtime
    // TODO: add duration
    // TODO: add duration type
    // TODO: add csvhelper
    // TODO: add labels.json for language settings
    // TODO: add tests
    // TODO: add lib for install that will check dotnetcore runtime installation
    // TODO: add benchmark and find out when(how many files should be) use parallel
    // TODO: add cancel button
    // TODO: add clear button
    // TODO: add result text box for status or status strip?
    // TODO: add text box for displaying the number of non processed files?
    // TODO: ignore duplicates
    // TODO: sort by sample title
    // BUG: double click start will double result table

    public partial class FaceForm : Form
    {
        private readonly BindingList<ViewModel> _viewModels;
        private CancellationTokenSource _cts;

        public FaceForm()
        {
            InitializeComponent();

            _viewModels = new BindingList<ViewModel>();

            ToolStripMenuItemMenuChoseSpectra.Click += ToolStripMenuItemMenuChoseSpectra_ClickAsync;
            FaceFormButtonStart.Click += FaceFormButtonStart_Click;
            FaceFormButtonCancel.Click += cancelButton_Click;

            FaceFormDataGridViewMain.DataSource = _viewModels;
        }

        private async void FaceFormButtonStart_Click(object sender, EventArgs e)
        {
            // HACK: FaceFormOpenSpectraFileDialog.FileNames couldn't be empty
            if (FaceFormOpenSpectraFileDialog.FileNames[0] == "FaceFormOpenSpectraFileDialog") ToolStripMenuItemMenuChoseSpectra_ClickAsync(sender, e);

            _cts = new CancellationTokenSource();
            try
            {
                await FillDataGridViewAsync(_cts.Token);
                //resultsTextBox.Text += "\r\nDownloads complete.";
            }
            catch (OperationCanceledException oe)
            {
                Debug.WriteLine(oe.Message);          
                //resultsTextBox.Text += "\r\nDownloads canceled.\r\n";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);          
                //resultsTextBox.Text += "\r\nDownloads failed.\r\n";
            }
            _cts = null;
        }

        private void ToolStripMenuItemMenuChoseSpectra_ClickAsync(object sender, EventArgs e)
        {
            if (FaceFormOpenSpectraFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            //FaceFormButtonStart_Click(sender, e);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
        }

        private async Task FillDataGridViewAsync(CancellationToken ct)
        {
            var ProcessFileTasks = FaceFormOpenSpectraFileDialog.FileNames.Select(file => ProcessFile(file, ct)).ToList();

            //var sampleIdMask = new Regex(@"[a-z]-\d{2}");
            while (ProcessFileTasks.Any())
            {
                var completedTask = await Task.WhenAny(ProcessFileTasks);
                ProcessFileTasks.Remove(completedTask);
                _viewModels.Add(await completedTask);
            }
        }

        private async Task<ViewModel> ProcessFile(string file, CancellationToken ct)
        {
            var spectra = await Task.Run(() => { return new Spectra(file); }, ct);
            return spectra.viewModel;
        }
    } //  public partial class FaceForm : Form
} // namespace GSI.UI
