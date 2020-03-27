using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GSI.Core;


namespace GSI.UI
{
    // TODO: parse selected spectra to list of object
    // TODO: bind list to data source of dgv
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
        private readonly List<ViewModel> _viewModels;
        private readonly StringBuilder _missedFiles;

        public FaceForm()
        {
            InitializeComponent();

            _viewModels = new List<ViewModel>();
            _missedFiles = new StringBuilder($"These files were skipped:{Environment.NewLine}");

            FaceFormButtonExportCSV.Click += FaceFormButtonExportCSV_Click;
            ToolStripMenuItemMenuChoseSpectra.Click += ToolStripMenuItemMenuChoseSpectra_Click;

            FaceFormDataGridViewMain.CellClick += FaceFormDataGridViewMain_CellClick;
            FaceFormDataGridViewMain.CellMouseClick += FaceFormDataGridViewMain_CellMouseClick; ;
            FaceFormDataGridViewMain.DataSource = _viewModels;

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

        private void ToolStripMenuItemMenuChoseSpectra_Click(object sender, EventArgs e)
        {
            if (FaceFormOpenSpectraFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            else
            {
                foreach (var file in FaceFormOpenSpectraFileDialog.FileNames)
                    _viewModels.Add((new Spectra(file)).viewModel);

                FillDataGridView();
            }
        }

        private void FillDataGridView()
        {
            FaceFormDataGridViewMain.DataSource = null;
            FaceFormDataGridViewMain.DataSource = _viewModels;
        }

        private void FaceFormButtonExportCSV_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
