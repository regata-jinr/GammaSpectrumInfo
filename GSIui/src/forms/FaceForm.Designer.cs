namespace GSI.UI
{
    partial class FaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FaceFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMenuLang = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMenuLangRus = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMenuLangEng = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMenuChoseSpectra = new System.Windows.Forms.ToolStripMenuItem();
            this.FaceFormDataGridViewMain = new System.Windows.Forms.DataGridView();
            this.FaceFormButtonStart = new System.Windows.Forms.Button();
            this.FaceFormButtonCancel = new System.Windows.Forms.Button();
            this.FaceFormButtonClear = new System.Windows.Forms.Button();
            this.FaceFormOpenSpectraFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.FaceFormToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FaceFormToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.FaceFormMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FaceFormDataGridViewMain)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FaceFormMenuStrip
            // 
            this.FaceFormMenuStrip.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FaceFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemMenu});
            this.FaceFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.FaceFormMenuStrip.Name = "FaceFormMenuStrip";
            this.FaceFormMenuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.FaceFormMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.FaceFormMenuStrip.Size = new System.Drawing.Size(1200, 30);
            this.FaceFormMenuStrip.TabIndex = 0;
            this.FaceFormMenuStrip.Text = "FaceFormMenuStrip";
            // 
            // ToolStripMenuItemMenu
            // 
            this.ToolStripMenuItemMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemMenuLang,
            this.ToolStripMenuItemMenuChoseSpectra});
            this.ToolStripMenuItemMenu.Name = "ToolStripMenuItemMenu";
            this.ToolStripMenuItemMenu.Size = new System.Drawing.Size(63, 24);
            this.ToolStripMenuItemMenu.Text = "Меню";
            // 
            // ToolStripMenuItemLang
            // 
            this.ToolStripMenuItemMenuLang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemMenuLangRus,
            this.ToolStripMenuItemMenuLangEng});
            this.ToolStripMenuItemMenuLang.Name = "ToolStripMenuItemMenuLang";
            this.ToolStripMenuItemMenuLang.Size = new System.Drawing.Size(255, 24);
            this.ToolStripMenuItemMenuLang.Text = "Язык";
            // 
            // ToolStripMenuItemMenuLangRus
            // 
            this.ToolStripMenuItemMenuLangRus.Name = "ToolStripMenuItemMenuLangRus";
            this.ToolStripMenuItemMenuLangRus.Size = new System.Drawing.Size(161, 24);
            this.ToolStripMenuItemMenuLangRus.Text = "Русский";
            this.ToolStripMenuItemMenuLangRus.CheckOnClick = true;
            // 
            // ToolStripMenuItemMenuLangEng
            // 
            this.ToolStripMenuItemMenuLangEng.Name = "ToolStripMenuItemMenuLangEng";
            this.ToolStripMenuItemMenuLangEng.Size = new System.Drawing.Size(161, 24);
            this.ToolStripMenuItemMenuLangEng.Text = "Английский";
            this.ToolStripMenuItemMenuLangEng.CheckOnClick = true;
            // 
            // ToolStripMenuItemMenuChoseSpectra
            // 
            this.ToolStripMenuItemMenuChoseSpectra.Name = "ToolStripMenuItemMenuChoseSpectra";
            this.ToolStripMenuItemMenuChoseSpectra.Size = new System.Drawing.Size(255, 24);
            this.ToolStripMenuItemMenuChoseSpectra.Text = "Выбрать файлы спектров";
            // 
            // FaceFormDataGridViewMain
            // 
            this.FaceFormDataGridViewMain.AllowUserToAddRows = false;
            this.FaceFormDataGridViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FaceFormDataGridViewMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.FaceFormDataGridViewMain.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.FaceFormDataGridViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FaceFormDataGridViewMain.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.FaceFormDataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FaceFormDataGridViewMain.Location = new System.Drawing.Point(18, 85);
            this.FaceFormDataGridViewMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FaceFormDataGridViewMain.Name = "FaceFormDataGridViewMain";
            this.FaceFormDataGridViewMain.ReadOnly = true;
            this.FaceFormDataGridViewMain.RowHeadersVisible = false;
            this.FaceFormDataGridViewMain.Size = new System.Drawing.Size(1164, 573);
            this.FaceFormDataGridViewMain.TabIndex = 1;
            // 
            // FaceFormButtonStart
            // 
            this.FaceFormButtonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FaceFormButtonStart.Location = new System.Drawing.Point(1062, 40);
            this.FaceFormButtonStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FaceFormButtonStart.Name = "FaceFormButtonStart";
            this.FaceFormButtonStart.Size = new System.Drawing.Size(120, 35);
            this.FaceFormButtonStart.TabIndex = 2;
            this.FaceFormButtonStart.Text = "Пуск";
            this.FaceFormButtonStart.UseVisualStyleBackColor = true;
            // 
            // FaceFormButtonCancel
            // 
            this.FaceFormButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FaceFormButtonCancel.Location = new System.Drawing.Point(934, 40);
            this.FaceFormButtonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FaceFormButtonCancel.Name = "FaceFormButtonCancel";
            this.FaceFormButtonCancel.Size = new System.Drawing.Size(120, 35);
            this.FaceFormButtonCancel.TabIndex = 2;
            this.FaceFormButtonCancel.Text = "Отмена";
            // 
            // FaceFormButtonClear
            // 
            this.FaceFormButtonClear.Location = new System.Drawing.Point(18, 40);
            this.FaceFormButtonClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FaceFormButtonClear.Name = "FaceFormButtonClear";
            this.FaceFormButtonClear.Size = new System.Drawing.Size(120, 35);
            this.FaceFormButtonClear.TabIndex = 2;
            this.FaceFormButtonClear.Text = "Очистить";
            this.FaceFormButtonClear.UseVisualStyleBackColor = true;
            // 
            // FaceFormOpenSpectraFileDialog
            // 
            this.FaceFormOpenSpectraFileDialog.DefaultExt = "cnf";
            this.FaceFormOpenSpectraFileDialog.FileName = "FaceFormOpenSpectraFileDialog";
            this.FaceFormOpenSpectraFileDialog.Filter = "Spectras (*.cnf)|*.cnf|All files|*.*";
            this.FaceFormOpenSpectraFileDialog.Multiselect = true;
            this.FaceFormOpenSpectraFileDialog.RestoreDirectory = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowMerge = false;
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FaceFormToolStripProgressBar,
            this.FaceFormToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 663);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1200, 29);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FaceFormToolStripStatusLabel
            // 
            this.FaceFormToolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FaceFormToolStripStatusLabel.Name = "FaceFormToolStripStatusLabel";
            this.FaceFormToolStripStatusLabel.Size = new System.Drawing.Size(151, 32);
            this.FaceFormToolStripStatusLabel.Text = "";
            this.FaceFormToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FaceFormToolStripProgressBar
            // 
            this.FaceFormToolStripProgressBar.ForeColor = System.Drawing.Color.ForestGreen;
            this.FaceFormToolStripProgressBar.Name = "FaceFormToolStripProgressBar";
            this.FaceFormToolStripProgressBar.Size = new System.Drawing.Size(200, 31);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.FaceFormButtonStart);
            this.Controls.Add(this.FaceFormButtonClear);
            this.Controls.Add(this.FaceFormButtonCancel);
            this.Controls.Add(this.FaceFormDataGridViewMain);
            this.Controls.Add(this.FaceFormMenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.FaceFormMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FaceForm";
            this.Text = "GammaSpectrumInfo";
            this.FaceFormMenuStrip.ResumeLayout(false);
            this.FaceFormMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FaceFormDataGridViewMain)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip FaceFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMenuLang;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMenuLangRus;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMenuLangEng;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMenuChoseSpectra;
        private System.Windows.Forms.DataGridView FaceFormDataGridViewMain;
        private System.Windows.Forms.Button FaceFormButtonStart;
        private System.Windows.Forms.Button FaceFormButtonCancel;
        private System.Windows.Forms.Button FaceFormButtonClear;
        private System.Windows.Forms.OpenFileDialog FaceFormOpenSpectraFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel FaceFormToolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar FaceFormToolStripProgressBar;
    }
}

