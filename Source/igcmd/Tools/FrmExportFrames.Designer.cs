namespace igcmd.Tools
{
    partial class FrmExportFrames
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
            ProgressBar = new ImageGlass.UI.ModernProgressBar();
            TableTop = new TableLayoutPanel();
            LblStatus = new ImageGlass.UI.ModernLabel();
            TableTop.SuspendLayout();
            SuspendLayout();
            // 
            // ProgressBar
            // 
            ProgressBar.AccessibleRole = AccessibleRole.ProgressBar;
            ProgressBar.BackColor = Color.Transparent;
            ProgressBar.DarkMode = false;
            ProgressBar.Dock = DockStyle.Top;
            ProgressBar.Location = new Point(16, 45);
            ProgressBar.Margin = new Padding(0, 10, 0, 0);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(343, 10);
            ProgressBar.TabIndex = 3;
            ProgressBar.UseMarqueeStyle = false;
            // 
            // TableTop
            // 
            TableTop.AutoSize = true;
            TableTop.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableTop.ColumnCount = 1;
            TableTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableTop.Controls.Add(ProgressBar, 0, 1);
            TableTop.Controls.Add(LblStatus, 0, 0);
            TableTop.Dock = DockStyle.Top;
            TableTop.Location = new Point(0, 0);
            TableTop.Margin = new Padding(0);
            TableTop.Name = "TableTop";
            TableTop.Padding = new Padding(16, 13, 16, 27);
            TableTop.RowCount = 2;
            TableTop.RowStyles.Add(new RowStyle());
            TableTop.RowStyles.Add(new RowStyle());
            TableTop.Size = new Size(375, 82);
            TableTop.TabIndex = 5;
            // 
            // LblStatus
            // 
            LblStatus.AutoSize = true;
            LblStatus.BackColor = Color.Transparent;
            LblStatus.DarkMode = false;
            LblStatus.Location = new Point(16, 13);
            LblStatus.Margin = new Padding(0, 0, 0, 7);
            LblStatus.Name = "LblStatus";
            LblStatus.Size = new Size(98, 15);
            LblStatus.TabIndex = 4;
            LblStatus.Text = "[frame file name]";
            // 
            // FrmExportFrames
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(375, 255);
            ControlBox = false;
            Controls.Add(TableTop);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(0, 0, 0, 0);
            Name = "FrmExportFrames";
            ShowAcceptButton = false;
            ShowIcon = true;
            ShowInTaskbar = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
            Controls.SetChildIndex(TableTop, 0);
            TableTop.ResumeLayout(false);
            TableTop.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ImageGlass.UI.ModernProgressBar ProgressBar;
        private TableLayoutPanel TableTop;
        private ImageGlass.UI.ModernLabel LblStatus;
    }
}