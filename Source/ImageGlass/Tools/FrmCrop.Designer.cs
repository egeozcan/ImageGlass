using ImageGlass.UI;

namespace ImageGlass
{
    partial class FrmCrop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCrop));
            TableTop = new TableLayoutPanel();
            LblLocation = new ModernLabel();
            LblSize = new ModernLabel();
            BtnSettings = new ModernButton();
            LblAspectRatio = new ModernLabel();
            NumX = new ModernNumericUpDown();
            NumY = new ModernNumericUpDown();
            NumWidth = new ModernNumericUpDown();
            NumHeight = new ModernNumericUpDown();
            CmbAspectRatio = new ModernComboBox();
            NumRatioFrom = new ModernNumericUpDown();
            NumRatioTo = new ModernNumericUpDown();
            flowLayoutPanel1 = new FlowLayoutPanel();
            BtnReset = new ModernButton();
            BtnQuickSelect = new ModernButton();
            TableBottom = new TableLayoutPanel();
            BtnSave = new ModernButton();
            BtnSaveAs = new ModernButton();
            BtnCopy = new ModernButton();
            BtnCrop = new ModernButton();
            TooltipMain = new ModernTooltip();
            TableTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumRatioFrom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumRatioTo).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            TableBottom.SuspendLayout();
            SuspendLayout();
            // 
            // TableTop
            // 
            TableTop.AutoSize = true;
            TableTop.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableTop.ColumnCount = 3;
            TableTop.ColumnStyles.Add(new ColumnStyle());
            TableTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableTop.Controls.Add(LblLocation, 0, 2);
            TableTop.Controls.Add(LblSize, 0, 3);
            TableTop.Controls.Add(BtnSettings, 2, 4);
            TableTop.Controls.Add(LblAspectRatio, 0, 0);
            TableTop.Controls.Add(NumX, 1, 2);
            TableTop.Controls.Add(NumY, 2, 2);
            TableTop.Controls.Add(NumWidth, 1, 3);
            TableTop.Controls.Add(NumHeight, 2, 3);
            TableTop.Controls.Add(CmbAspectRatio, 1, 0);
            TableTop.Controls.Add(NumRatioFrom, 1, 1);
            TableTop.Controls.Add(NumRatioTo, 2, 1);
            TableTop.Controls.Add(flowLayoutPanel1, 0, 4);
            TableTop.Dock = DockStyle.Top;
            TableTop.Location = new Point(0, 0);
            TableTop.Margin = new Padding(0);
            TableTop.Name = "TableTop";
            TableTop.Padding = new Padding(16, 13, 16, 13);
            TableTop.RowCount = 6;
            TableTop.RowStyles.Add(new RowStyle());
            TableTop.RowStyles.Add(new RowStyle());
            TableTop.RowStyles.Add(new RowStyle());
            TableTop.RowStyles.Add(new RowStyle());
            TableTop.RowStyles.Add(new RowStyle());
            TableTop.RowStyles.Add(new RowStyle());
            TableTop.Size = new Size(257, 170);
            TableTop.TabIndex = 0;
            // 
            // LblLocation
            // 
            LblLocation.AutoSize = true;
            LblLocation.BackColor = Color.Transparent;
            LblLocation.DarkMode = true;
            LblLocation.Location = new Point(16, 69);
            LblLocation.Margin = new Padding(0, 3, 8, 3);
            LblLocation.Name = "LblLocation";
            LblLocation.Size = new Size(64, 15);
            LblLocation.TabIndex = 0;
            LblLocation.Text = "[Location:]";
            // 
            // LblSize
            // 
            LblSize.AutoSize = true;
            LblSize.BackColor = Color.Transparent;
            LblSize.DarkMode = true;
            LblSize.Location = new Point(16, 95);
            LblSize.Margin = new Padding(0, 0, 8, 3);
            LblSize.Name = "LblSize";
            LblSize.Size = new Size(38, 15);
            LblSize.TabIndex = 1;
            LblSize.Text = "[Size:]";
            // 
            // BtnSettings
            // 
            BtnSettings.DarkMode = true;
            BtnSettings.Dock = DockStyle.Right;
            BtnSettings.Image = (Image)resources.GetObject("BtnSettings.Image");
            BtnSettings.ImagePadding = 0;
            BtnSettings.Location = new Point(194, 128);
            BtnSettings.Margin = new Padding(0, 7, 0, 3);
            BtnSettings.Name = "BtnSettings";
            BtnSettings.Padding = new Padding(4, 3, 4, 3);
            BtnSettings.Size = new Size(47, 26);
            BtnSettings.SvgIcon = IconName.Setting;
            BtnSettings.SystemIcon = null;
            BtnSettings.TabIndex = 8;
            BtnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnSettings.Click += BtnSettings_Click;
            // 
            // LblAspectRatio
            // 
            LblAspectRatio.AutoSize = true;
            LblAspectRatio.BackColor = Color.Transparent;
            LblAspectRatio.DarkMode = true;
            LblAspectRatio.Location = new Point(16, 14);
            LblAspectRatio.Margin = new Padding(0, 1, 2, 3);
            LblAspectRatio.Name = "LblAspectRatio";
            LblAspectRatio.Size = new Size(81, 15);
            LblAspectRatio.TabIndex = 4;
            LblAspectRatio.Text = "[Aspect ratio:]";
            // 
            // NumX
            // 
            NumX.DarkMode = true;
            NumX.Dock = DockStyle.Fill;
            NumX.Location = new Point(99, 69);
            NumX.Margin = new Padding(0, 3, 4, 3);
            NumX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumX.Name = "NumX";
            NumX.Size = new Size(67, 23);
            NumX.TabIndex = 3;
            NumX.ThousandsSeparator = true;
            // 
            // NumY
            // 
            NumY.DarkMode = true;
            NumY.Dock = DockStyle.Fill;
            NumY.Location = new Point(174, 69);
            NumY.Margin = new Padding(4, 3, 0, 3);
            NumY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumY.Name = "NumY";
            NumY.Size = new Size(67, 23);
            NumY.TabIndex = 4;
            NumY.ThousandsSeparator = true;
            // 
            // NumWidth
            // 
            NumWidth.DarkMode = true;
            NumWidth.Dock = DockStyle.Fill;
            NumWidth.Location = new Point(99, 95);
            NumWidth.Margin = new Padding(0, 0, 4, 3);
            NumWidth.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumWidth.Name = "NumWidth";
            NumWidth.Size = new Size(67, 23);
            NumWidth.TabIndex = 5;
            NumWidth.ThousandsSeparator = true;
            // 
            // NumHeight
            // 
            NumHeight.DarkMode = true;
            NumHeight.Dock = DockStyle.Fill;
            NumHeight.Location = new Point(174, 95);
            NumHeight.Margin = new Padding(4, 0, 0, 3);
            NumHeight.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumHeight.Name = "NumHeight";
            NumHeight.Size = new Size(67, 23);
            NumHeight.TabIndex = 6;
            NumHeight.ThousandsSeparator = true;
            // 
            // CmbAspectRatio
            // 
            TableTop.SetColumnSpan(CmbAspectRatio, 2);
            CmbAspectRatio.DarkMode = true;
            CmbAspectRatio.Dock = DockStyle.Fill;
            CmbAspectRatio.DrawMode = DrawMode.OwnerDrawVariable;
            CmbAspectRatio.FormattingEnabled = true;
            CmbAspectRatio.Items.AddRange(new object[] { "Free ratio", "Custom...", "Original", "1:1", "1:2", "3:2", "4:3", "16:9" });
            CmbAspectRatio.Location = new Point(99, 13);
            CmbAspectRatio.Margin = new Padding(0, 0, 0, 3);
            CmbAspectRatio.Name = "CmbAspectRatio";
            CmbAspectRatio.Size = new Size(142, 24);
            CmbAspectRatio.TabIndex = 0;
            // 
            // NumRatioFrom
            // 
            NumRatioFrom.DarkMode = true;
            NumRatioFrom.Dock = DockStyle.Top;
            NumRatioFrom.Location = new Point(99, 40);
            NumRatioFrom.Margin = new Padding(0, 0, 4, 3);
            NumRatioFrom.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumRatioFrom.Name = "NumRatioFrom";
            NumRatioFrom.Size = new Size(67, 23);
            NumRatioFrom.TabIndex = 1;
            // 
            // NumRatioTo
            // 
            NumRatioTo.DarkMode = true;
            NumRatioTo.Dock = DockStyle.Top;
            NumRatioTo.Location = new Point(174, 40);
            NumRatioTo.Margin = new Padding(4, 0, 0, 3);
            NumRatioTo.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumRatioTo.Name = "NumRatioTo";
            NumRatioTo.Size = new Size(67, 23);
            NumRatioTo.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableTop.SetColumnSpan(flowLayoutPanel1, 2);
            flowLayoutPanel1.Controls.Add(BtnReset);
            flowLayoutPanel1.Controls.Add(BtnQuickSelect);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(16, 121);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0, 7, 5, 3);
            flowLayoutPanel1.Size = new Size(154, 36);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // BtnReset
            // 
            BtnReset.DarkMode = true;
            BtnReset.Image = (Image)resources.GetObject("BtnReset.Image");
            BtnReset.ImagePadding = 0;
            BtnReset.Location = new Point(102, 7);
            BtnReset.Margin = new Padding(5, 0, 0, 0);
            BtnReset.Name = "BtnReset";
            BtnReset.Padding = new Padding(4, 3, 4, 3);
            BtnReset.Size = new Size(47, 26);
            BtnReset.SvgIcon = IconName.ResetSelection;
            BtnReset.SystemIcon = null;
            BtnReset.TabIndex = 1;
            BtnReset.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnReset.Click += BtnReset_Click;
            // 
            // BtnQuickSelect
            // 
            BtnQuickSelect.DarkMode = true;
            BtnQuickSelect.Image = (Image)resources.GetObject("BtnQuickSelect.Image");
            BtnQuickSelect.ImagePadding = 0;
            BtnQuickSelect.Location = new Point(50, 7);
            BtnQuickSelect.Margin = new Padding(5, 0, 0, 0);
            BtnQuickSelect.Name = "BtnQuickSelect";
            BtnQuickSelect.Padding = new Padding(4, 3, 4, 3);
            BtnQuickSelect.Size = new Size(47, 26);
            BtnQuickSelect.SvgIcon = IconName.Selection;
            BtnQuickSelect.SystemIcon = null;
            BtnQuickSelect.TabIndex = 0;
            BtnQuickSelect.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnQuickSelect.Visible = false;
            BtnQuickSelect.Click += BtnQuickSelect_Click;
            // 
            // TableBottom
            // 
            TableBottom.AutoSize = true;
            TableBottom.ColumnCount = 2;
            TableBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableBottom.Controls.Add(BtnSave, 0, 0);
            TableBottom.Controls.Add(BtnSaveAs, 0, 1);
            TableBottom.Controls.Add(BtnCopy, 1, 1);
            TableBottom.Controls.Add(BtnCrop, 1, 0);
            TableBottom.Dock = DockStyle.Bottom;
            TableBottom.Location = new Point(0, 168);
            TableBottom.Margin = new Padding(2);
            TableBottom.Name = "TableBottom";
            TableBottom.Padding = new Padding(16, 10, 16, 10);
            TableBottom.RowCount = 2;
            TableBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TableBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TableBottom.Size = new Size(257, 76);
            TableBottom.TabIndex = 1;
            // 
            // BtnSave
            // 
            BtnSave.ButtonStyle = ModernButtonStyle.CTA;
            BtnSave.DarkMode = true;
            BtnSave.Dock = DockStyle.Bottom;
            BtnSave.ImagePadding = 2;
            BtnSave.Location = new Point(16, 10);
            BtnSave.Margin = new Padding(0, 0, 2, 2);
            BtnSave.Name = "BtnSave";
            BtnSave.Padding = new Padding(4, 3, 4, 3);
            BtnSave.Size = new Size(110, 26);
            BtnSave.SystemIcon = null;
            BtnSave.TabIndex = 0;
            BtnSave.Text = "[Save]";
            BtnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnSaveAs
            // 
            BtnSaveAs.ButtonStyle = ModernButtonStyle.CTA;
            BtnSaveAs.DarkMode = true;
            BtnSaveAs.Dock = DockStyle.Bottom;
            BtnSaveAs.ImagePadding = 2;
            BtnSaveAs.Location = new Point(16, 40);
            BtnSaveAs.Margin = new Padding(0, 2, 2, 0);
            BtnSaveAs.Name = "BtnSaveAs";
            BtnSaveAs.Padding = new Padding(4, 3, 4, 3);
            BtnSaveAs.Size = new Size(110, 26);
            BtnSaveAs.SystemIcon = null;
            BtnSaveAs.TabIndex = 1;
            BtnSaveAs.Text = "[Save as...]";
            BtnSaveAs.Click += BtnSaveAs_Click;
            // 
            // BtnCopy
            // 
            BtnCopy.DarkMode = true;
            BtnCopy.Dock = DockStyle.Bottom;
            BtnCopy.Image = (Image)resources.GetObject("BtnCopy.Image");
            BtnCopy.ImagePadding = 2;
            BtnCopy.Location = new Point(130, 40);
            BtnCopy.Margin = new Padding(2, 2, 0, 0);
            BtnCopy.Name = "BtnCopy";
            BtnCopy.Padding = new Padding(4, 3, 4, 3);
            BtnCopy.Size = new Size(111, 26);
            BtnCopy.SvgIcon = IconName.Copy;
            BtnCopy.SystemIcon = null;
            BtnCopy.TabIndex = 3;
            BtnCopy.Text = "[Copy]";
            BtnCopy.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCopy.Click += BtnCopy_Click;
            // 
            // BtnCrop
            // 
            BtnCrop.DarkMode = true;
            BtnCrop.Dock = DockStyle.Bottom;
            BtnCrop.ImagePadding = 2;
            BtnCrop.Location = new Point(130, 10);
            BtnCrop.Margin = new Padding(2, 0, 0, 2);
            BtnCrop.Name = "BtnCrop";
            BtnCrop.Padding = new Padding(4, 3, 4, 3);
            BtnCrop.Size = new Size(111, 26);
            BtnCrop.SystemIcon = null;
            BtnCrop.TabIndex = 2;
            BtnCrop.Text = "[Crop only]";
            BtnCrop.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCrop.Click += BtnCrop_Click;
            // 
            // TooltipMain
            // 
            TooltipMain.AllPadding = 4;
            TooltipMain.DarkMode = false;
            TooltipMain.OwnerDraw = true;
            // 
            // FrmCrop
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(257, 244);
            Controls.Add(TableBottom);
            Controls.Add(TableTop);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(0, 0);
            Margin = new Padding(5, 4, 5, 4);
            Name = "FrmCrop";
            Opacity = 0.85D;
            Text = "[Crop tool]";
            TableTop.ResumeLayout(false);
            TableTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumX).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumY).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumRatioFrom).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumRatioTo).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            TableBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel TableTop;
        private ModernLabel LblLocation;
        private ModernLabel LblSize;
        private ModernLabel LblAspectRatio;
        private ModernNumericUpDown NumX;
        private ModernNumericUpDown NumY;
        private ModernNumericUpDown NumWidth;
        private ModernNumericUpDown NumHeight;
        private ModernComboBox CmbAspectRatio;
        private TableLayoutPanel TableBottom;
        private ModernButton BtnSave;
        private ModernButton BtnSaveAs;
        private ModernButton BtnCopy;
        private ModernButton BtnCrop;
        private ModernNumericUpDown NumRatioFrom;
        private ModernNumericUpDown NumRatioTo;
        private FlowLayoutPanel flowLayoutPanel1;
        private ModernButton BtnReset;
        private ModernButton BtnQuickSelect;
        private ModernButton BtnSettings;
        private ModernTooltip TooltipMain;
    }
}