namespace ImageGlass
{
    partial class FrmColorPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmColorPicker));
            TableLayout = new TableLayoutPanel();
            BtnCopyCIELab = new UI.ModernButton();
            TxtCIELAB = new UI.ModernTextBox();
            LblCIELAB = new UI.ModernLabel();
            BtnSettings = new UI.ModernButton();
            BtnCopyHsv = new UI.ModernButton();
            BtnCopyHsl = new UI.ModernButton();
            BtnCopyCmyk = new UI.ModernButton();
            BtnCopyHex = new UI.ModernButton();
            BtnCopyRgb = new UI.ModernButton();
            TxtHsv = new UI.ModernTextBox();
            TxtHsl = new UI.ModernTextBox();
            TxtCmyk = new UI.ModernTextBox();
            TxtHex = new UI.ModernTextBox();
            TxtRgb = new UI.ModernTextBox();
            LblHsv = new UI.ModernLabel();
            LblLocation = new UI.ModernLabel();
            PanColor = new Panel();
            LblCursorLocation = new UI.ModernLabel();
            LblRgb = new UI.ModernLabel();
            LblHex = new UI.ModernLabel();
            LblCmyk = new UI.ModernLabel();
            LblHsl = new UI.ModernLabel();
            TxtLocation = new UI.ModernTextBox();
            BtnCopyLocation = new UI.ModernButton();
            TooltipMain = new UI.ModernTooltip();
            TableLayout.SuspendLayout();
            PanColor.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayout
            // 
            TableLayout.AutoSize = true;
            TableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayout.ColumnCount = 3;
            TableLayout.ColumnStyles.Add(new ColumnStyle());
            TableLayout.ColumnStyles.Add(new ColumnStyle());
            TableLayout.ColumnStyles.Add(new ColumnStyle());
            TableLayout.Controls.Add(BtnCopyCIELab, 2, 7);
            TableLayout.Controls.Add(TxtCIELAB, 1, 7);
            TableLayout.Controls.Add(LblCIELAB, 0, 7);
            TableLayout.Controls.Add(BtnSettings, 2, 0);
            TableLayout.Controls.Add(BtnCopyHsv, 2, 6);
            TableLayout.Controls.Add(BtnCopyHsl, 2, 5);
            TableLayout.Controls.Add(BtnCopyCmyk, 2, 4);
            TableLayout.Controls.Add(BtnCopyHex, 2, 3);
            TableLayout.Controls.Add(BtnCopyRgb, 2, 2);
            TableLayout.Controls.Add(TxtHsv, 1, 6);
            TableLayout.Controls.Add(TxtHsl, 1, 5);
            TableLayout.Controls.Add(TxtCmyk, 1, 4);
            TableLayout.Controls.Add(TxtHex, 1, 3);
            TableLayout.Controls.Add(TxtRgb, 1, 2);
            TableLayout.Controls.Add(LblHsv, 0, 6);
            TableLayout.Controls.Add(LblLocation, 0, 1);
            TableLayout.Controls.Add(PanColor, 0, 0);
            TableLayout.Controls.Add(LblRgb, 0, 2);
            TableLayout.Controls.Add(LblHex, 0, 3);
            TableLayout.Controls.Add(LblCmyk, 0, 4);
            TableLayout.Controls.Add(LblHsl, 0, 5);
            TableLayout.Controls.Add(TxtLocation, 1, 1);
            TableLayout.Controls.Add(BtnCopyLocation, 2, 1);
            TableLayout.Dock = DockStyle.Top;
            TableLayout.Location = new Point(0, 0);
            TableLayout.Margin = new Padding(0);
            TableLayout.Name = "TableLayout";
            TableLayout.Padding = new Padding(16, 13, 16, 13);
            TableLayout.RowCount = 8;
            TableLayout.RowStyles.Add(new RowStyle());
            TableLayout.RowStyles.Add(new RowStyle());
            TableLayout.RowStyles.Add(new RowStyle());
            TableLayout.RowStyles.Add(new RowStyle());
            TableLayout.RowStyles.Add(new RowStyle());
            TableLayout.RowStyles.Add(new RowStyle());
            TableLayout.RowStyles.Add(new RowStyle());
            TableLayout.RowStyles.Add(new RowStyle());
            TableLayout.Size = new Size(257, 253);
            TableLayout.TabIndex = 0;
            // 
            // BtnCopyCIELab
            // 
            BtnCopyCIELab.Anchor = AnchorStyles.Right;
            BtnCopyCIELab.DarkMode = true;
            BtnCopyCIELab.Image = (Image)resources.GetObject("BtnCopyCIELab.Image");
            BtnCopyCIELab.ImagePadding = 0;
            BtnCopyCIELab.Location = new Point(240, 215);
            BtnCopyCIELab.Margin = new Padding(4, 0, 0, 0);
            BtnCopyCIELab.Name = "BtnCopyCIELab";
            BtnCopyCIELab.Padding = new Padding(3, 3, 3, 3);
            BtnCopyCIELab.Size = new Size(31, 20);
            BtnCopyCIELab.SvgIcon = UI.IconName.Copy;
            BtnCopyCIELab.SystemIcon = null;
            BtnCopyCIELab.TabIndex = 15;
            BtnCopyCIELab.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCopyCIELab.Click += BtnCopyCIELab_Click;
            // 
            // TxtCIELAB
            // 
            TxtCIELAB.BackColor = Color.FromArgb(69, 73, 74);
            TxtCIELAB.BorderStyle = BorderStyle.FixedSingle;
            TxtCIELAB.DarkMode = true;
            TxtCIELAB.Dock = DockStyle.Fill;
            TxtCIELAB.ForeColor = Color.FromArgb(210, 210, 210);
            TxtCIELAB.Location = new Point(74, 214);
            TxtCIELAB.Margin = new Padding(0, 3, 0, 3);
            TxtCIELAB.Name = "TxtCIELAB";
            TxtCIELAB.ReadOnly = true;
            TxtCIELAB.Size = new Size(162, 23);
            TxtCIELAB.TabIndex = 14;
            // 
            // LblCIELAB
            // 
            LblCIELAB.Anchor = AnchorStyles.Left;
            LblCIELAB.AutoSize = true;
            LblCIELAB.BackColor = Color.Transparent;
            LblCIELAB.DarkMode = true;
            LblCIELAB.Location = new Point(17, 218);
            LblCIELAB.Margin = new Padding(1, 0, 1, 0);
            LblCIELAB.Name = "LblCIELAB";
            LblCIELAB.Size = new Size(56, 15);
            LblCIELAB.TabIndex = 13;
            LblCIELAB.Text = "[CIELAB:]";
            // 
            // BtnSettings
            // 
            BtnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnSettings.DarkMode = true;
            BtnSettings.Image = (Image)resources.GetObject("BtnSettings.Image");
            BtnSettings.ImagePadding = 0;
            BtnSettings.Location = new Point(240, 13);
            BtnSettings.Margin = new Padding(4, 0, 0, 3);
            BtnSettings.Name = "BtnSettings";
            BtnSettings.Padding = new Padding(3, 3, 3, 3);
            BtnSettings.Size = new Size(31, 20);
            BtnSettings.SvgIcon = UI.IconName.Setting;
            BtnSettings.SystemIcon = null;
            BtnSettings.TabIndex = 12;
            BtnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnSettings.Click += BtnSettings_Click;
            // 
            // BtnCopyHsv
            // 
            BtnCopyHsv.Anchor = AnchorStyles.Right;
            BtnCopyHsv.DarkMode = true;
            BtnCopyHsv.Image = (Image)resources.GetObject("BtnCopyHsv.Image");
            BtnCopyHsv.ImagePadding = 0;
            BtnCopyHsv.Location = new Point(240, 186);
            BtnCopyHsv.Margin = new Padding(4, 0, 0, 0);
            BtnCopyHsv.Name = "BtnCopyHsv";
            BtnCopyHsv.Padding = new Padding(3, 3, 3, 3);
            BtnCopyHsv.Size = new Size(31, 20);
            BtnCopyHsv.SvgIcon = UI.IconName.Copy;
            BtnCopyHsv.SystemIcon = null;
            BtnCopyHsv.TabIndex = 11;
            BtnCopyHsv.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCopyHsv.Click += BtnCopyHsv_Click;
            // 
            // BtnCopyHsl
            // 
            BtnCopyHsl.Anchor = AnchorStyles.Right;
            BtnCopyHsl.DarkMode = true;
            BtnCopyHsl.Image = (Image)resources.GetObject("BtnCopyHsl.Image");
            BtnCopyHsl.ImagePadding = 0;
            BtnCopyHsl.Location = new Point(240, 157);
            BtnCopyHsl.Margin = new Padding(4, 0, 0, 0);
            BtnCopyHsl.Name = "BtnCopyHsl";
            BtnCopyHsl.Padding = new Padding(3, 3, 3, 3);
            BtnCopyHsl.Size = new Size(31, 20);
            BtnCopyHsl.SvgIcon = UI.IconName.Copy;
            BtnCopyHsl.SystemIcon = null;
            BtnCopyHsl.TabIndex = 9;
            BtnCopyHsl.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCopyHsl.Click += BtnCopyHsl_Click;
            // 
            // BtnCopyCmyk
            // 
            BtnCopyCmyk.Anchor = AnchorStyles.Right;
            BtnCopyCmyk.DarkMode = true;
            BtnCopyCmyk.Image = (Image)resources.GetObject("BtnCopyCmyk.Image");
            BtnCopyCmyk.ImagePadding = 0;
            BtnCopyCmyk.Location = new Point(240, 128);
            BtnCopyCmyk.Margin = new Padding(4, 0, 0, 0);
            BtnCopyCmyk.Name = "BtnCopyCmyk";
            BtnCopyCmyk.Padding = new Padding(3, 3, 3, 3);
            BtnCopyCmyk.Size = new Size(31, 20);
            BtnCopyCmyk.SvgIcon = UI.IconName.Copy;
            BtnCopyCmyk.SystemIcon = null;
            BtnCopyCmyk.TabIndex = 7;
            BtnCopyCmyk.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCopyCmyk.Click += BtnCopyCmyk_Click;
            // 
            // BtnCopyHex
            // 
            BtnCopyHex.Anchor = AnchorStyles.Right;
            BtnCopyHex.DarkMode = true;
            BtnCopyHex.Image = (Image)resources.GetObject("BtnCopyHex.Image");
            BtnCopyHex.ImagePadding = 0;
            BtnCopyHex.Location = new Point(240, 99);
            BtnCopyHex.Margin = new Padding(4, 0, 0, 0);
            BtnCopyHex.Name = "BtnCopyHex";
            BtnCopyHex.Padding = new Padding(3, 3, 3, 3);
            BtnCopyHex.Size = new Size(31, 20);
            BtnCopyHex.SvgIcon = UI.IconName.Copy;
            BtnCopyHex.SystemIcon = null;
            BtnCopyHex.TabIndex = 5;
            BtnCopyHex.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCopyHex.Click += BtnCopyHex_Click;
            // 
            // BtnCopyRgb
            // 
            BtnCopyRgb.Anchor = AnchorStyles.Right;
            BtnCopyRgb.DarkMode = true;
            BtnCopyRgb.Image = (Image)resources.GetObject("BtnCopyRgb.Image");
            BtnCopyRgb.ImagePadding = 0;
            BtnCopyRgb.Location = new Point(240, 70);
            BtnCopyRgb.Margin = new Padding(4, 0, 0, 0);
            BtnCopyRgb.Name = "BtnCopyRgb";
            BtnCopyRgb.Padding = new Padding(3, 3, 3, 3);
            BtnCopyRgb.Size = new Size(31, 20);
            BtnCopyRgb.SvgIcon = UI.IconName.Copy;
            BtnCopyRgb.SystemIcon = null;
            BtnCopyRgb.TabIndex = 3;
            BtnCopyRgb.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCopyRgb.Click += BtnCopyRgb_Click;
            // 
            // TxtHsv
            // 
            TxtHsv.BackColor = Color.FromArgb(69, 73, 74);
            TxtHsv.BorderStyle = BorderStyle.FixedSingle;
            TxtHsv.DarkMode = true;
            TxtHsv.Dock = DockStyle.Fill;
            TxtHsv.ForeColor = Color.FromArgb(210, 210, 210);
            TxtHsv.Location = new Point(74, 185);
            TxtHsv.Margin = new Padding(0, 3, 0, 3);
            TxtHsv.Name = "TxtHsv";
            TxtHsv.ReadOnly = true;
            TxtHsv.Size = new Size(162, 23);
            TxtHsv.TabIndex = 10;
            // 
            // TxtHsl
            // 
            TxtHsl.BackColor = Color.FromArgb(69, 73, 74);
            TxtHsl.BorderStyle = BorderStyle.FixedSingle;
            TxtHsl.DarkMode = true;
            TxtHsl.Dock = DockStyle.Fill;
            TxtHsl.ForeColor = Color.FromArgb(210, 210, 210);
            TxtHsl.Location = new Point(74, 156);
            TxtHsl.Margin = new Padding(0, 3, 0, 3);
            TxtHsl.Name = "TxtHsl";
            TxtHsl.ReadOnly = true;
            TxtHsl.Size = new Size(162, 23);
            TxtHsl.TabIndex = 8;
            // 
            // TxtCmyk
            // 
            TxtCmyk.BackColor = Color.FromArgb(69, 73, 74);
            TxtCmyk.BorderStyle = BorderStyle.FixedSingle;
            TxtCmyk.DarkMode = true;
            TxtCmyk.Dock = DockStyle.Fill;
            TxtCmyk.ForeColor = Color.FromArgb(210, 210, 210);
            TxtCmyk.Location = new Point(74, 127);
            TxtCmyk.Margin = new Padding(0, 3, 0, 3);
            TxtCmyk.Name = "TxtCmyk";
            TxtCmyk.ReadOnly = true;
            TxtCmyk.Size = new Size(162, 23);
            TxtCmyk.TabIndex = 6;
            // 
            // TxtHex
            // 
            TxtHex.BackColor = Color.FromArgb(69, 73, 74);
            TxtHex.BorderStyle = BorderStyle.FixedSingle;
            TxtHex.DarkMode = true;
            TxtHex.Dock = DockStyle.Fill;
            TxtHex.ForeColor = Color.FromArgb(210, 210, 210);
            TxtHex.Location = new Point(74, 98);
            TxtHex.Margin = new Padding(0, 3, 0, 3);
            TxtHex.Name = "TxtHex";
            TxtHex.ReadOnly = true;
            TxtHex.Size = new Size(162, 23);
            TxtHex.TabIndex = 4;
            // 
            // TxtRgb
            // 
            TxtRgb.BackColor = Color.FromArgb(69, 73, 74);
            TxtRgb.BorderStyle = BorderStyle.FixedSingle;
            TxtRgb.DarkMode = true;
            TxtRgb.Dock = DockStyle.Fill;
            TxtRgb.ForeColor = Color.FromArgb(210, 210, 210);
            TxtRgb.Location = new Point(74, 69);
            TxtRgb.Margin = new Padding(0, 3, 0, 3);
            TxtRgb.Name = "TxtRgb";
            TxtRgb.ReadOnly = true;
            TxtRgb.Size = new Size(162, 23);
            TxtRgb.TabIndex = 2;
            // 
            // LblHsv
            // 
            LblHsv.Anchor = AnchorStyles.Left;
            LblHsv.AutoSize = true;
            LblHsv.BackColor = Color.Transparent;
            LblHsv.DarkMode = true;
            LblHsv.Location = new Point(17, 189);
            LblHsv.Margin = new Padding(1, 0, 1, 0);
            LblHsv.Name = "LblHsv";
            LblHsv.Size = new Size(47, 15);
            LblHsv.TabIndex = 6;
            LblHsv.Text = "[HSVA:]";
            // 
            // LblLocation
            // 
            LblLocation.Anchor = AnchorStyles.Left;
            LblLocation.AutoSize = true;
            LblLocation.BackColor = Color.Transparent;
            LblLocation.DarkMode = true;
            LblLocation.Location = new Point(16, 44);
            LblLocation.Margin = new Padding(0);
            LblLocation.Name = "LblLocation";
            LblLocation.Size = new Size(38, 15);
            LblLocation.TabIndex = 1;
            LblLocation.Text = "[X, Y:]";
            // 
            // PanColor
            // 
            PanColor.BackColor = Color.Transparent;
            PanColor.BorderStyle = BorderStyle.FixedSingle;
            TableLayout.SetColumnSpan(PanColor, 2);
            PanColor.Controls.Add(LblCursorLocation);
            PanColor.Dock = DockStyle.Fill;
            PanColor.Location = new Point(16, 13);
            PanColor.Margin = new Padding(0, 0, 0, 3);
            PanColor.Name = "PanColor";
            PanColor.Size = new Size(220, 21);
            PanColor.TabIndex = 0;
            // 
            // LblCursorLocation
            // 
            LblCursorLocation.BackColor = Color.Transparent;
            LblCursorLocation.DarkMode = false;
            LblCursorLocation.Dock = DockStyle.Fill;
            LblCursorLocation.ForeColor = Color.White;
            LblCursorLocation.Location = new Point(0, 0);
            LblCursorLocation.Margin = new Padding(1, 0, 1, 0);
            LblCursorLocation.Name = "LblCursorLocation";
            LblCursorLocation.Size = new Size(218, 19);
            LblCursorLocation.TabIndex = 0;
            LblCursorLocation.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LblRgb
            // 
            LblRgb.Anchor = AnchorStyles.Left;
            LblRgb.AutoSize = true;
            LblRgb.BackColor = Color.Transparent;
            LblRgb.DarkMode = true;
            LblRgb.Location = new Point(16, 73);
            LblRgb.Margin = new Padding(0);
            LblRgb.Name = "LblRgb";
            LblRgb.Size = new Size(48, 15);
            LblRgb.TabIndex = 2;
            LblRgb.Text = "[RGBA:]";
            // 
            // LblHex
            // 
            LblHex.Anchor = AnchorStyles.Left;
            LblHex.AutoSize = true;
            LblHex.BackColor = Color.Transparent;
            LblHex.DarkMode = true;
            LblHex.Location = new Point(17, 102);
            LblHex.Margin = new Padding(1, 0, 1, 0);
            LblHex.Name = "LblHex";
            LblHex.Size = new Size(48, 15);
            LblHex.TabIndex = 3;
            LblHex.Text = "[HEXA:]";
            // 
            // LblCmyk
            // 
            LblCmyk.Anchor = AnchorStyles.Left;
            LblCmyk.AutoSize = true;
            LblCmyk.BackColor = Color.Transparent;
            LblCmyk.DarkMode = true;
            LblCmyk.Location = new Point(16, 131);
            LblCmyk.Margin = new Padding(0);
            LblCmyk.Name = "LblCmyk";
            LblCmyk.Size = new Size(51, 15);
            LblCmyk.TabIndex = 4;
            LblCmyk.Text = "[CMYK:]";
            // 
            // LblHsl
            // 
            LblHsl.Anchor = AnchorStyles.Left;
            LblHsl.AutoSize = true;
            LblHsl.BackColor = Color.Transparent;
            LblHsl.DarkMode = true;
            LblHsl.Location = new Point(16, 160);
            LblHsl.Margin = new Padding(0);
            LblHsl.Name = "LblHsl";
            LblHsl.Size = new Size(47, 15);
            LblHsl.TabIndex = 5;
            LblHsl.Text = "[HSLA:]";
            // 
            // TxtLocation
            // 
            TxtLocation.BackColor = Color.FromArgb(69, 73, 74);
            TxtLocation.BorderStyle = BorderStyle.FixedSingle;
            TxtLocation.DarkMode = true;
            TxtLocation.Dock = DockStyle.Fill;
            TxtLocation.ForeColor = Color.FromArgb(210, 210, 210);
            TxtLocation.Location = new Point(74, 40);
            TxtLocation.Margin = new Padding(0, 3, 0, 3);
            TxtLocation.Name = "TxtLocation";
            TxtLocation.ReadOnly = true;
            TxtLocation.Size = new Size(162, 23);
            TxtLocation.TabIndex = 0;
            // 
            // BtnCopyLocation
            // 
            BtnCopyLocation.Anchor = AnchorStyles.Right;
            BtnCopyLocation.DarkMode = true;
            BtnCopyLocation.Image = (Image)resources.GetObject("BtnCopyLocation.Image");
            BtnCopyLocation.ImagePadding = 0;
            BtnCopyLocation.Location = new Point(240, 41);
            BtnCopyLocation.Margin = new Padding(4, 0, 0, 0);
            BtnCopyLocation.Name = "BtnCopyLocation";
            BtnCopyLocation.Padding = new Padding(3, 3, 3, 3);
            BtnCopyLocation.Size = new Size(31, 20);
            BtnCopyLocation.SvgIcon = UI.IconName.Copy;
            BtnCopyLocation.SystemIcon = null;
            BtnCopyLocation.TabIndex = 1;
            BtnCopyLocation.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnCopyLocation.Click += BtnCopyLocation_Click;
            // 
            // TooltipMain
            // 
            TooltipMain.AllPadding = 4;
            TooltipMain.DarkMode = false;
            TooltipMain.OwnerDraw = true;
            // 
            // FrmColorPicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(280, 244);
            Controls.Add(TableLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Location = new Point(0, 0);
            Margin = new Padding(1, 1, 1, 1);
            Name = "FrmColorPicker";
            Text = "[Color picker]";
            TableLayout.ResumeLayout(false);
            TableLayout.PerformLayout();
            PanColor.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private TableLayoutPanel TableLayout;
        private Panel PanColor;
        private UI.ModernLabel LblHsv;
        private UI.ModernLabel LblLocation;
        private UI.ModernLabel LblRgb;
        private UI.ModernLabel LblHex;
        private UI.ModernLabel LblCmyk;
        private UI.ModernLabel LblHsl;
        private UI.ModernTextBox TxtLocation;
        private UI.ModernTextBox TxtHsv;
        private UI.ModernTextBox TxtHsl;
        private UI.ModernTextBox TxtCmyk;
        private UI.ModernTextBox TxtHex;
        private UI.ModernTextBox TxtRgb;
        private UI.ModernButton BtnCopyLocation;
        private UI.ModernButton BtnCopyHsv;
        private UI.ModernButton BtnCopyHsl;
        private UI.ModernButton BtnCopyCmyk;
        private UI.ModernButton BtnCopyHex;
        private UI.ModernButton BtnCopyRgb;
        private UI.ModernLabel LblCursorLocation;
        private UI.ModernButton BtnSettings;
        private UI.ModernButton BtnCopyCIELab;
        private UI.ModernTextBox TxtCIELAB;
        private UI.ModernLabel LblCIELAB;
        private UI.ModernTooltip TooltipMain;
    }
}