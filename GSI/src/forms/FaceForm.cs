/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020-2021, REGATA Experiment at FLNP|JINR                  *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.Reflection;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Regata.Core.CNF;


namespace Regata.Desktop.WinForms.GSI
{
    // FIXME: for test I have to install dotnet.exe x86 see: https://github.com/xunit/xunit/issues/1123
    // TODO: add lib for install that will check dotnetcore runtime installation
    // TODO: find out how to run appx on clean system. test via sandbox and win7 vm
    // TODO: check if dotnet runtime is absent install it. the same for msix
    // TODO: remove old programs and clean pathes
    // TODO: freeze first two columns
    // TODO: add timestamping to packaging in actions
    // TODO: add sort by any columnt
    // TODO: can user add custom parameters to table by paramCode?

    public partial class FaceForm : Form
    {
        private readonly BindingList<SpectraInfo> _viewModels;
        private CancellationTokenSource _cts;
        private Settings _settings;

        public FaceForm()
        {
            InitializeComponents();
            _settings = new Settings();
            Text = $"Gamma Spectrum Info - {Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version}";

            if (Labels.CurrentLanguage == Languages.English)
                ToolStripMenuItemMenuLangEng.Checked = true;
            else
                ToolStripMenuItemMenuLangRus.Checked = true;

            _viewModels = new BindingList<SpectraInfo>();
            FaceFormDataGridViewMain.DataSource = _viewModels;

            ToolStripMenuItemMenuChoseSpectra.Click     += ToolStripMenuItemMenuChoseSpectra_ClickAsync;
            FaceFormButtonStart.Click                   += FaceFormButtonStart_Click;
            FaceFormButtonCancel.Click                  += cancelButton_Click;
            FaceFormButtonClear.Click                   += FaceFormButtonClear_Click;
            ToolStripMenuItemMenuLangEng.CheckedChanged += LangStripMenuItem_CheckedChanged;
            ToolStripMenuItemMenuLangRus.CheckedChanged += LangStripMenuItem_CheckedChanged;

            ToolStripMenuItemMenuChoseSpectra.Visible = false;
            Utilities.ChangeFormLanguage(this);
            InitializeMenuViewShowColumns();
        }

        private async void FaceFormButtonStart_Click(object sender, EventArgs e)
        {
            if (FaceFormOpenSpectraFileDialog.FileNames[0] == "FaceFormOpenSpectraFileDialog" || _isCleared || FaceFormToolStripProgressBar.Value == FaceFormToolStripProgressBar.Maximum)
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
                return;
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
            FaceFormToolStripStatusLabel.Text = $"{Utilities.GetValueOfSetting(FaceFormToolStripStatusLabel.Name)} {FaceFormToolStripProgressBar.Maximum}";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
            FaceFormButtonStart.Enabled = true;
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

        private void InsertToTheRightPlace(SpectraInfo currentfile)
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
                if (int.Parse(currentfile.File.Substring(0, 7)) <= int.Parse(v.File))
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

        private async Task<SpectraInfo> ProcessFile(string file, CancellationToken ct)
        {
            var spectra = await Task.Run(() => { return new Spectra(file); }, ct);
            return spectra.SpectrumData;
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
            FaceFormButtonCancel.PerformClick();
            FaceFormButtonStart.Enabled = true;
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
            InitializeMenuViewShowColumns();
            _settings.SaveSettings();
        }

        private void InitializeMenuViewShowColumns()
        {
            ToolStripMenuItemViewShowColumns.DropDownItems.Clear();
            foreach (DataGridViewColumn col in FaceFormDataGridViewMain.Columns)
            {
                var t = new ToolStripMenuItem { Name = col.Name, Text = col.HeaderText, CheckOnClick = true, Checked = true};

                if (_settings.NonDisplayedColumns.Contains(t.Name))
                {
                    t.Checked = false;
                    col.Visible = false;
                }
                t.CheckedChanged += ShowColumns_CheckedChanged;
                ToolStripMenuItemViewShowColumns.DropDownItems.AddRange(new ToolStripMenuItem[1] {t});

            }
        }

        private void ShowColumns_CheckedChanged(object sender, EventArgs e)
        {
            var t = sender as ToolStripMenuItem;

            if (!t.Checked)
                _settings.NonDisplayedColumns.Add(t.Name);
            else
            {
                if (_settings.NonDisplayedColumns.Contains(t.Name))
                    _settings.NonDisplayedColumns.Remove(t.Name);
            }
            _settings.SaveSettings();

            FaceFormDataGridViewMain.Columns[t.Name].Visible = t.Checked;
        }

    } //  public partial class FaceForm : Form
}     // namespace Regata.Desktop.WinForms.GSI
