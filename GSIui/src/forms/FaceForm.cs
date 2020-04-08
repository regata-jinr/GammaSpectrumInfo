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
    // TODO: what files I can put into github?
    // TODO: add lib for install that will check dotnetcore runtime installation
    // TODO: check if dotnet runtime is absent install it. the same for msix.
    // TODO: unify start program pathes for all regata software: Regata -> GammaSpectrumInfo
    // TODO: prepare icons:
    // TODO:    make it more readable in small size
    // TODO:    perhaps solution is for small size use only feather, but for medium combine with logo
    // TODO:    add icon to application not only package

    // TODO: remove old programs and clean pathes
    // TODO: test it on win7 vm
    // TODO: prepare README
    // TODO: add github workflows for ci/cd (check installation and tests on independent platform and create github release)
    // FIXME: for test I have to install dotnet.exe x86 see: https://github.com/xunit/xunit/issues/1123
    // TODO: add sort by any columnt
    // TODO: can user add custom parameters to table by paramCode?

    public partial class FaceForm : Form
    {
        private readonly BindingList<ViewModel> _viewModels;
        private CancellationTokenSource _cts;
        private Settings _settings;

        public FaceForm()
        {
            InitializeComponent();

            _settings = new Settings();

            if (Labels.CurrentLanguage == Languages.English)
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

            Utilities.ChangeFormLanguage(this);
        }

        private async void FaceFormButtonStart_Click(object sender, EventArgs e)
        {
            if (FaceFormOpenSpectraFileDialog.FileNames[0] == "FaceFormOpenSpectraFileDialog" || _isCleared)
                ToolStripMenuItemMenuChoseSpectra_ClickAsync(sender, e);

            if (FaceFormOpenSpectraFileDialog.FileNames[0] == "FaceFormOpenSpectraFileDialog")
                return;

            FaceFormButtonStart.Enabled = false;
            Labels.CurrentStatus = Status.Processing;
            FaceFormButtonStart.Text = Utilities.GetValueOfSetting(FaceFormButtonStart.Name);
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
                Labels.CurrentStatus = Status.Canceled;
            }
            catch (Exception ex)
            {
                FaceFormToolStripStatusLabel.Text = ex.Message;
            }
            FaceFormButtonStart.Enabled = true;

            if (!isCancelled)
            {
                Labels.CurrentStatus = Status.Success;
                FaceFormButtonStart.Text = Utilities.GetValueOfSetting(FaceFormButtonStart.Name);
                FaceFormToolStripStatusLabel.Text = Utilities.GetValueOfSetting(FaceFormToolStripStatusLabel.Name);
            }
            else
            {
                FaceFormToolStripStatusLabel.Text = Utilities.GetValueOfSetting(FaceFormToolStripStatusLabel.Name);
            }
            _cts = null;
        }

        private void ToolStripMenuItemMenuChoseSpectra_ClickAsync(object sender, EventArgs e)
        {
            if (FaceFormOpenSpectraFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            Labels.CurrentStatus = Status.Info;
            FaceFormToolStripProgressBar.Value = 0;
            FaceFormToolStripProgressBar.Maximum = FaceFormOpenSpectraFileDialog.FileNames.Length;
            FaceFormToolStripStatusLabel.Text = $"{Utilities.GetValueOfSetting(FaceFormToolStripStatusLabel.Name)} {FaceFormToolStripProgressBar.Maximum.ToString()}";
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
            Labels.CurrentStatus = Status.Processing;
            FaceFormToolStripStatusLabel.Text = $"{currentfile.File} {Utilities.GetValueOfSetting(FaceFormToolStripStatusLabel.Name)}";
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
            Labels.CurrentStatus = Status.Info;
            FaceFormButtonStart.Text = Utilities.GetValueOfSetting(FaceFormButtonStart.Name);
        }
     
        private void LangStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            var langStrip = sender as ToolStripMenuItem;

            if (langStrip.Checked && langStrip.Name == ToolStripMenuItemMenuLangEng.Name)
            {
                _settings.CurrentLanguage = Languages.English;
                ToolStripMenuItemMenuLangRus.Checked = false;
            }

            if (langStrip.Checked && langStrip.Name == ToolStripMenuItemMenuLangRus.Name)
            {
                _settings.CurrentLanguage = Languages.Russian;
                ToolStripMenuItemMenuLangEng.Checked = false;
            }
            Utilities.ChangeFormLanguage(this);
        }

    } //  public partial class FaceForm : Form
} // namespace GSI.UI
