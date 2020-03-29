using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSI.Core;
using System.Threading;

namespace GSI.UI
{
    // TODO: parse selected spectra to list of object
    // TODO: bind list to data source of dgv
    // TODO: add deadtime
    // TODO: add duration
    // TODO: add duration type
    // TODO: add csvhelper
    // TODO: add labels.json for language settings
    // TODO: add tests
    // TODO: add lib for install that will check dotnetcore runtime installation
    // TODO: fix all sample titles for all spectras 
    // TODO: add benchmark and find out when(how many files should be) use parallel
    // TODO: add async and parallel processing (it should add files when read one ...)
    // TODO: add files to existing
    // TODO: clear table
    // FIXME: click to dgv crash the app | upd: the problem in current cell, it null and can't be initialised

    public partial class FaceForm : Form
    {
        private readonly BindingList<ViewModel> _viewModels;
        private readonly StringBuilder _missedFiles;
        private readonly List<string> _files;
        private CancellationTokenSource _cts;

        public FaceForm()
        {
            InitializeComponent();

            _viewModels = new BindingList<ViewModel>();
            _files = new List<string>();
            _missedFiles = new StringBuilder($"These files were skipped:{Environment.NewLine}");

            FaceFormButtonExportCSV.Click += FaceFormButtonExportCSV_Click;
            //ToolStripMenuItemMenuChoseSpectra.Click += ToolStripMenuItemMenuChoseSpectra_Click;
            ToolStripMenuItemMenuChoseSpectra.Click += ToolStripMenuItemMenuChoseSpectra_ClickAsync;

            FaceFormDataGridViewMain.CellClick += FaceFormDataGridViewMain_CellClick;
            FaceFormDataGridViewMain.CellMouseClick += FaceFormDataGridViewMain_CellMouseClick; 
            FaceFormDataGridViewMain.DataSource = _viewModels;
            //FaceFormDataGridViewMain.DataSource = null;

        } 

        private void FaceFormDataGridViewMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (FaceFormDataGridViewMain.CurrentCell == null || 
                FaceFormDataGridViewMain.CurrentCell.Value == null ||
                e.RowIndex == -1) return;
        }

        private void FaceFormDataGridViewMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FaceFormDataGridViewMain.CurrentCell == null ||
                FaceFormDataGridViewMain.CurrentCell.Value == null ||
                e.RowIndex == -1) return;
        }

        // TODO: how to fill dgv partially, i.e. after processing bunch of files or each files the result should be added to dgv
        // TODO: how to update dgv from side thread?

        //private void ToolStripMenuItemMenuChoseSpectra_Click(object sender, EventArgs e)
        //{
        //    if (FaceFormOpenSpectraFileDialog.ShowDialog() == DialogResult.Cancel)
        //        return;
        //    else
        //    {
        //        foreach (var file in FaceFormOpenSpectraFileDialog.FileNames)
        //            _viewModels.Add((new Spectra(file)).viewModel);
        //        //FillDataGridView();
        //    }
        //}

        private async void ToolStripMenuItemMenuChoseSpectra_ClickAsync(object sender, EventArgs e)
        {
            if (FaceFormOpenSpectraFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            else
            {
                _cts = new CancellationTokenSource();
                try
                {
                    _files.AddRange(FaceFormOpenSpectraFileDialog.FileNames);
                    await FillDataGridView(_cts.Token);
                    //resultsTextBox.Text += "\r\nDownloads complete.";
                }
                catch (OperationCanceledException)
                {
                    //resultsTextBox.Text += "\r\nDownloads canceled.\r\n";
                }
                catch (Exception)
                {
                    //resultsTextBox.Text += "\r\nDownloads failed.\r\n";
                }
                _cts = null;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }

        private async Task FillDataGridView(CancellationToken ct)
        {

            List<Task<ViewModel>> ProcessFileTasks = _files.Select(file => ProcessFile(file, ct)).ToList();

            while (ProcessFileTasks.Any())
            {
                Task<ViewModel> completedTask = await Task.WhenAny(ProcessFileTasks);

                ProcessFileTasks.Remove(completedTask);

                _viewModels.Add(await completedTask);
            }
        }

        private async Task<ViewModel> ProcessFile(string file, CancellationToken ct)
        {
            return await Task.FromResult<ViewModel>(new Spectra(file).viewModel);

        }

      
        private void FaceFormButtonExportCSV_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
