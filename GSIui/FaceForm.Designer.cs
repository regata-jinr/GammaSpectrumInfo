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
            this.FaceFormDataGridViewMain = new System.Windows.Forms.DataGridView();
            this.ToolStripMenuItemMenuChoseSpectra = new System.Windows.Forms.ToolStripMenuItem();
            this.FaceFormButtonStart = new System.Windows.Forms.Button();
            this.FaceFormButtonCancel = new System.Windows.Forms.Button();
            this.FaceFormOpenSpectraFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FaceFormMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FaceFormDataGridViewMain)).BeginInit();
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
            this.ToolStripMenuItemMenu.Size = new System.Drawing.Size(53, 20);
            this.ToolStripMenuItemMenu.Text = "Меню";
            // 
            // ToolStripMenuItemMenuLang
            // 
            this.ToolStripMenuItemMenuLang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemMenuLangRus,
            this.ToolStripMenuItemMenuLangEng});
            this.ToolStripMenuItemMenuLang.Name = "ToolStripMenuItemMenuLang";
            this.ToolStripMenuItemMenuLang.Size = new System.Drawing.Size(215, 22);
            this.ToolStripMenuItemMenuLang.Text = "Язык";
            // 
            // ToolStripMenuItemMenuLangRus
            // 
            this.ToolStripMenuItemMenuLangRus.Name = "ToolStripMenuItemMenuLangRus";
            this.ToolStripMenuItemMenuLangRus.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemMenuLangRus.Text = "Русский";
            // 
            // ToolStripMenuItemMenuLangEng
            // 
            this.ToolStripMenuItemMenuLangEng.Name = "ToolStripMenuItemMenuLangEng";
            this.ToolStripMenuItemMenuLangEng.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemMenuLangEng.Text = "Английский";
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
            this.FaceFormDataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FaceFormDataGridViewMain.Location = new System.Drawing.Point(18, 85);
            this.FaceFormDataGridViewMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FaceFormDataGridViewMain.Name = "FaceFormDataGridViewMain";
            this.FaceFormDataGridViewMain.RowHeadersVisible = false;
            this.FaceFormDataGridViewMain.Size = new System.Drawing.Size(1164, 589);
            this.FaceFormDataGridViewMain.TabIndex = 1;
            this.FaceFormDataGridViewMain.ReadOnly = true;
            this.FaceFormDataGridViewMain.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            //this.FaceFormDataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ToolStripMenuItemMenuChoseSpectra
            // 
            this.ToolStripMenuItemMenuChoseSpectra.Name = "ToolStripMenuItemMenuChoseSpectra";
            this.ToolStripMenuItemMenuChoseSpectra.Size = new System.Drawing.Size(215, 22);
            this.ToolStripMenuItemMenuChoseSpectra.Text = "Выбрать файлы спектров";
           // 
            // FaceFormButtonStart
            // 
            this.FaceFormButtonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FaceFormButtonStart.Location = new System.Drawing.Point(664, 42);
            this.FaceFormButtonStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FaceFormButtonStart.Name = "FaceFormButtonStart";
            this.FaceFormButtonStart.Size = new System.Drawing.Size(218, 35);
            this.FaceFormButtonStart.TabIndex = 2;
            this.FaceFormButtonStart.Text = "Пуск";
            this.FaceFormButtonStart.UseVisualStyleBackColor = true;
            // 
            // FaceFormButtonCancel
            // 
            this.FaceFormButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FaceFormButtonCancel.Location = new System.Drawing.Point(333, 42);
            this.FaceFormButtonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FaceFormButtonCancel.Name = "FaceFormButtonCancel";
            this.FaceFormButtonCancel.Size = new System.Drawing.Size(218, 35);
            this.FaceFormButtonCancel.TabIndex = 2;
            this.FaceFormButtonCancel.Text = "Отмена";
            this.FaceFormButtonStart.UseVisualStyleBackColor = true;
            // 
            // FaceFormOpenSpectraFileDialog
            // 
            this.FaceFormOpenSpectraFileDialog.FileName = "FaceFormOpenSpectraFileDialog";
            FaceFormOpenSpectraFileDialog.Filter = "Spectras (*.cnf)|*.cnf|All files|*.*";
            FaceFormOpenSpectraFileDialog.RestoreDirectory = true;
            FaceFormOpenSpectraFileDialog.DefaultExt = "cnf";
            FaceFormOpenSpectraFileDialog.CheckFileExists = true;
            FaceFormOpenSpectraFileDialog.CheckPathExists = true;
            FaceFormOpenSpectraFileDialog.ValidateNames = true;
            FaceFormOpenSpectraFileDialog.Multiselect = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.FaceFormButtonStart);
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
        private System.Windows.Forms.OpenFileDialog FaceFormOpenSpectraFileDialog;
    }
}

