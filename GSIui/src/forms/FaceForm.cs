/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSI.Core;
using System.Threading;

namespace GSI.UI
{
    // TODO: add tests
    // TODO: add lib for install that will check dotnetcore runtime installation
    // TODO: can user add custom parameters to table by paramCode?
    // TODO: add github workflows for ci/cd (check installation and tests on independent platform and create github release)

    public partial class FaceForm : Form
    {
        private readonly BindingList<ViewModel> _viewModels;
        private CancellationTokenSource _cts;
        private string lang = ConfigurationManager.Language;

        public FaceForm()
        {
            InitializeComponent();

            if (lang == "eng")
                ToolStripMenuItemMenuLangEng.Checked = true;
            else
                ToolStripMenuItemMenuLangRus.Checked = true;

            _viewModels = new BindingList<ViewModel>();
            FaceFormDataGridViewMain.DataSource = _viewModels;

            ToolStripMenuItemMenuChoseSpectra.Click     += ToolStripMenuItemMenuChoseSpectra_ClickAsync;
            FaceFormButtonStart.Click                   += FaceFormButtonStart_Click;
            FaceFormButtonCancel.Click                  += cancelButton_Click;
            FaceFormButtonClear.Click                   += FaceFormButtonClear_Click;
            ToolStripMenuItemMenuLangEng.CheckedChanged += LangStripMenuItem_CheckedChanged;
            ToolStripMenuItemMenuLangRus.CheckedChanged += LangStripMenuItem_CheckedChanged;

            Utilities.ChangeFormLanguage(this, lang);
        }

        private async void FaceFormButtonStart_Click(object sender, EventArgs e)
        {
            if (FaceFormOpenSpectraFileDialog.FileNames[0] == "FaceFormOpenSpectraFileDialog" || _isCleared)
                ToolStripMenuItemMenuChoseSpectra_ClickAsync(sender, e);

            if (FaceFormOpenSpectraFileDialog.FileNames[0] == "FaceFormOpenSpectraFileDialog")
                return;

            FaceFormButtonStart.Enabled = false;
            FaceFormButtonStart.Text = ConfigurationManager.config[$"Continuation:{lang}"];
            var isCancelled = false;

            _isCleared = false;
            _cts = new CancellationTokenSource();
            try
            {
                await FillDataGridViewAsync(_cts.Token);
            }
            catch (OperationCanceledException oe)
            {
                FaceFormToolStripStatusLabel.Text = oe.Message;
                isCancelled = true;
            }
            catch (Exception ex)
            {
                FaceFormToolStripStatusLabel.Text = ex.Message;
            }
            FaceFormButtonStart.Enabled = true;

            if (!isCancelled)
            {
                FaceFormButtonStart.Text = ConfigurationManager.config[$"{FaceFormButtonStart.Name}:{lang}"];
                FaceFormToolStripStatusLabel.Text = ConfigurationManager.config[$"{FaceFormToolStripStatusLabel.Name}:Success:{lang}"];
            }
            else
            {
                FaceFormToolStripStatusLabel.Text = ConfigurationManager.config[$"{FaceFormToolStripStatusLabel.Name}:Canceled:{lang}"];
            }
            _cts = null;
        }

        private void ToolStripMenuItemMenuChoseSpectra_ClickAsync(object sender, EventArgs e)
        {
            if (FaceFormOpenSpectraFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            FaceFormToolStripProgressBar.Value = 0;
            FaceFormToolStripProgressBar.Maximum = FaceFormOpenSpectraFileDialog.FileNames.Length;
            FaceFormToolStripStatusLabel.Text = $"{ConfigurationManager.config[$"{FaceFormToolStripStatusLabel.Name}:Info:{lang}"]} {FaceFormToolStripProgressBar.Maximum.ToString()}";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
        }

        private async Task FillDataGridViewAsync(CancellationToken ct)
        {
            var ProcessFileTasks = FaceFormOpenSpectraFileDialog.FileNames.Select(file => ProcessFile(file, ct)).ToList();

            while (ProcessFileTasks.Any())
            {
                var completedTask = await Task.WhenAny(ProcessFileTasks);
                ProcessFileTasks.Remove(completedTask);
                InsertToTheRightPlace(await completedTask);
                FaceFormToolStripProgressBar.Value++;
            }
        }

        private void InsertToTheRightPlace(ViewModel currentfile)
        {
            FaceFormToolStripStatusLabel.Text = $"{currentfile.File} {ConfigurationManager.config[$"{FaceFormToolStripStatusLabel.Name}:Processing:{lang}"]}";
            if (!_viewModels.Any())
            {
                _viewModels.Add(currentfile);
                return;
            }

            if (_viewModels.Select(v => v.File).ToArray().Contains(currentfile.File)) return;

            foreach (var v in _viewModels)
            {
                if (int.Parse(currentfile.File) <= int.Parse(v.File))
                {
                    _viewModels.Insert(_viewModels.IndexOf(v), currentfile);
                    break;
                }

                if (v == _viewModels.Last())
                {
                    _viewModels.Add(currentfile);
                    break;
                }
            }
        }

        private async Task<ViewModel> ProcessFile(string file, CancellationToken ct)
        {
            var spectra = await Task.Run(() => { return new Spectra(file); }, ct);
            return spectra.viewModel;
        }

        private bool _isCleared;

        private void FaceFormButtonClear_Click(object sender, EventArgs e)
        {
            _viewModels.Clear();
            FaceFormToolStripStatusLabel.Text = "";
            _isCleared = true;
            FaceFormToolStripProgressBar.Value = 0;
        }
     
        private void LangStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            var langStrip = sender as ToolStripMenuItem;

            if (langStrip.Checked && langStrip.Name == ToolStripMenuItemMenuLangEng.Name)
            {
                lang = "eng";
                ToolStripMenuItemMenuLangRus.Checked = false;
            }

            if (langStrip.Checked && langStrip.Name == ToolStripMenuItemMenuLangRus.Name)
            {
                lang = "rus";
                ToolStripMenuItemMenuLangEng.Checked = false;
            }
            Utilities.ChangeFormLanguage(this, lang);
        }

    } //  public partial class FaceForm : Form
} // namespace GSI.UI
