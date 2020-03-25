using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    // FIXME: click to dgv crash the app

    public partial class FaceForm : Form
    {
        private readonly List<ViewModel> _viewModels;
        private readonly StringBuilder _missedFiles;

        public FaceForm()
        {
            InitializeComponent();

            var spectra = new Spectra(@"D:\Spectra\1207256.cnf");

            _viewModels = new List<ViewModel>();
            _missedFiles = new StringBuilder($"These files were skipped:{Environment.NewLine}");

            FaceFormButtonExportCSV.Click += FaceFormButtonExportCSV_Click;
            ToolStripMenuItemMenuChoseSpectra.Click += ToolStripMenuItemMenuChoseSpectra_Click;

            FaceFormDataGridViewMain.DataSource = _viewModels;

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
