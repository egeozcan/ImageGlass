namespace ImageGlass.Tools;

partial class FrmResize
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
        TableTop = new TableLayoutPanel();
        LblSizeUnit = new UI.ModernLabel();
        ProgStatus = new ProgressBar();
        LblNewSizeValue = new UI.ModernLabel();
        LblCurrentSizeValue = new UI.ModernLabel();
        LblNewSize = new UI.ModernLabel();
        LblCurrentSize = new UI.ModernLabel();
        PanResizeBy = new FlowLayoutPanel();
        RadResizeByPixels = new RadioButton();
        RadResizeByPercentage = new RadioButton();
        LblSize = new UI.ModernLabel();
        NumHeight = new UI.ModernNumericUpDown();
        NumWidth = new UI.ModernNumericUpDown();
        ChkKeepRatio = new UI.ModernCheckBox();
        LblResample = new UI.ModernLabel();
        CmbResample = new UI.ModernComboBox();
        TableTop.SuspendLayout();
        PanResizeBy.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)NumHeight).BeginInit();
        ((System.ComponentModel.ISupportInitialize)NumWidth).BeginInit();
        SuspendLayout();
        // 
        // TableTop
        // 
        TableTop.AutoSize = true;
        TableTop.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        TableTop.ColumnCount = 4;
        TableTop.ColumnStyles.Add(new ColumnStyle());
        TableTop.ColumnStyles.Add(new ColumnStyle());
        TableTop.ColumnStyles.Add(new ColumnStyle());
        TableTop.ColumnStyles.Add(new ColumnStyle());
        TableTop.Controls.Add(LblSizeUnit, 3, 1);
        TableTop.Controls.Add(ProgStatus, 0, 7);
        TableTop.Controls.Add(LblNewSizeValue, 1, 6);
        TableTop.Controls.Add(LblCurrentSizeValue, 1, 5);
        TableTop.Controls.Add(LblNewSize, 0, 6);
        TableTop.Controls.Add(LblCurrentSize, 0, 5);
        TableTop.Controls.Add(PanResizeBy, 1, 0);
        TableTop.Controls.Add(LblSize, 0, 1);
        TableTop.Controls.Add(NumHeight, 2, 1);
        TableTop.Controls.Add(NumWidth, 1, 1);
        TableTop.Controls.Add(ChkKeepRatio, 1, 3);
        TableTop.Controls.Add(LblResample, 0, 4);
        TableTop.Controls.Add(CmbResample, 1, 4);
        TableTop.Dock = DockStyle.Top;
        TableTop.Location = new Point(0, 0);
        TableTop.Margin = new Padding(0);
        TableTop.Name = "TableTop";
        TableTop.Padding = new Padding(16, 13, 16, 20);
        TableTop.RowCount = 8;
        TableTop.RowStyles.Add(new RowStyle());
        TableTop.RowStyles.Add(new RowStyle());
        TableTop.RowStyles.Add(new RowStyle());
        TableTop.RowStyles.Add(new RowStyle());
        TableTop.RowStyles.Add(new RowStyle());
        TableTop.RowStyles.Add(new RowStyle());
        TableTop.RowStyles.Add(new RowStyle());
        TableTop.RowStyles.Add(new RowStyle());
        TableTop.Size = new Size(333, 224);
        TableTop.TabIndex = 3;
        // 
        // LblSizeUnit
        // 
        LblSizeUnit.AutoSize = true;
        LblSizeUnit.BackColor = Color.Transparent;
        LblSizeUnit.DarkMode = false;
        LblSizeUnit.Location = new Point(252, 44);
        LblSizeUnit.Margin = new Padding(3, 2, 8, 3);
        LblSizeUnit.Name = "LblSizeUnit";
        LblSizeUnit.Size = new Size(20, 15);
        LblSizeUnit.TabIndex = 22;
        LblSizeUnit.Text = "px";
        // 
        // ProgStatus
        // 
        ProgStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        TableTop.SetColumnSpan(ProgStatus, 4);
        ProgStatus.Location = new Point(16, 194);
        ProgStatus.Margin = new Padding(0, 20, 0, 0);
        ProgStatus.MarqueeAnimationSpeed = 10;
        ProgStatus.Name = "ProgStatus";
        ProgStatus.Size = new Size(301, 10);
        ProgStatus.Style = ProgressBarStyle.Marquee;
        ProgStatus.TabIndex = 4;
        ProgStatus.Value = 100;
        ProgStatus.Visible = false;
        // 
        // LblNewSizeValue
        // 
        LblNewSizeValue.AutoSize = true;
        LblNewSizeValue.BackColor = Color.Transparent;
        TableTop.SetColumnSpan(LblNewSizeValue, 2);
        LblNewSizeValue.DarkMode = false;
        LblNewSizeValue.Location = new Point(101, 155);
        LblNewSizeValue.Margin = new Padding(0, 0, 0, 4);
        LblNewSizeValue.Name = "LblNewSizeValue";
        LblNewSizeValue.Size = new Size(85, 15);
        LblNewSizeValue.TabIndex = 21;
        LblNewSizeValue.Text = "[800 x 1200 px]";
        // 
        // LblCurrentSizeValue
        // 
        LblCurrentSizeValue.AutoSize = true;
        LblCurrentSizeValue.BackColor = Color.Transparent;
        TableTop.SetColumnSpan(LblCurrentSizeValue, 2);
        LblCurrentSizeValue.DarkMode = false;
        LblCurrentSizeValue.Location = new Point(101, 136);
        LblCurrentSizeValue.Margin = new Padding(0, 10, 0, 4);
        LblCurrentSizeValue.Name = "LblCurrentSizeValue";
        LblCurrentSizeValue.Size = new Size(91, 15);
        LblCurrentSizeValue.TabIndex = 20;
        LblCurrentSizeValue.Text = "[1000 x 1500 px]";
        // 
        // LblNewSize
        // 
        LblNewSize.AutoSize = true;
        LblNewSize.BackColor = Color.Transparent;
        LblNewSize.DarkMode = false;
        LblNewSize.Location = new Point(16, 155);
        LblNewSize.Margin = new Padding(0, 0, 4, 0);
        LblNewSize.Name = "LblNewSize";
        LblNewSize.Size = new Size(65, 15);
        LblNewSize.TabIndex = 19;
        LblNewSize.Text = "[New Size:]";
        // 
        // LblCurrentSize
        // 
        LblCurrentSize.AutoSize = true;
        LblCurrentSize.BackColor = Color.Transparent;
        LblCurrentSize.DarkMode = false;
        LblCurrentSize.Location = new Point(16, 136);
        LblCurrentSize.Margin = new Padding(0, 10, 4, 0);
        LblCurrentSize.Name = "LblCurrentSize";
        LblCurrentSize.Size = new Size(81, 15);
        LblCurrentSize.TabIndex = 18;
        LblCurrentSize.Text = "[Current Size:]";
        // 
        // PanResizeBy
        // 
        PanResizeBy.AutoSize = true;
        PanResizeBy.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        TableTop.SetColumnSpan(PanResizeBy, 3);
        PanResizeBy.Controls.Add(RadResizeByPixels);
        PanResizeBy.Controls.Add(RadResizeByPercentage);
        PanResizeBy.Location = new Point(101, 13);
        PanResizeBy.Margin = new Padding(0, 0, 0, 10);
        PanResizeBy.Name = "PanResizeBy";
        PanResizeBy.Size = new Size(165, 19);
        PanResizeBy.TabIndex = 4;
        // 
        // RadResizeByPixels
        // 
        RadResizeByPixels.AutoSize = true;
        RadResizeByPixels.Checked = true;
        RadResizeByPixels.Location = new Point(0, 0);
        RadResizeByPixels.Margin = new Padding(0, 0, 10, 0);
        RadResizeByPixels.Name = "RadResizeByPixels";
        RadResizeByPixels.Size = new Size(63, 19);
        RadResizeByPixels.TabIndex = 4;
        RadResizeByPixels.TabStop = true;
        RadResizeByPixels.Text = "[Pixels]";
        RadResizeByPixels.UseVisualStyleBackColor = true;
        RadResizeByPixels.CheckedChanged += RadResizeByPixels_CheckedChanged;
        // 
        // RadResizeByPercentage
        // 
        RadResizeByPercentage.AutoSize = true;
        RadResizeByPercentage.Location = new Point(73, 0);
        RadResizeByPercentage.Margin = new Padding(0);
        RadResizeByPercentage.Name = "RadResizeByPercentage";
        RadResizeByPercentage.Size = new Size(92, 19);
        RadResizeByPercentage.TabIndex = 16;
        RadResizeByPercentage.Text = "[Percentage]";
        RadResizeByPercentage.UseVisualStyleBackColor = true;
        RadResizeByPercentage.CheckedChanged += RadResizeByPercentage_CheckedChanged;
        // 
        // LblSize
        // 
        LblSize.AutoSize = true;
        LblSize.BackColor = Color.Transparent;
        LblSize.DarkMode = false;
        LblSize.Location = new Point(16, 44);
        LblSize.Margin = new Padding(0, 2, 8, 3);
        LblSize.Name = "LblSize";
        LblSize.Size = new Size(38, 15);
        LblSize.TabIndex = 14;
        LblSize.Text = "[Size:]";
        // 
        // NumHeight
        // 
        NumHeight.DarkMode = false;
        NumHeight.Location = new Point(179, 42);
        NumHeight.Margin = new Padding(4, 0, 0, 4);
        NumHeight.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        NumHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        NumHeight.Name = "NumHeight";
        NumHeight.Size = new Size(70, 23);
        NumHeight.TabIndex = 10;
        NumHeight.ThousandsSeparator = true;
        NumHeight.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // NumWidth
        // 
        NumWidth.DarkMode = false;
        NumWidth.Location = new Point(101, 42);
        NumWidth.Margin = new Padding(0, 0, 4, 4);
        NumWidth.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        NumWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        NumWidth.Name = "NumWidth";
        NumWidth.Size = new Size(70, 23);
        NumWidth.TabIndex = 9;
        NumWidth.ThousandsSeparator = true;
        NumWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // ChkKeepRatio
        // 
        ChkKeepRatio.AutoSize = true;
        ChkKeepRatio.BackColor = Color.Transparent;
        ChkKeepRatio.Checked = true;
        ChkKeepRatio.CheckState = CheckState.Checked;
        TableTop.SetColumnSpan(ChkKeepRatio, 3);
        ChkKeepRatio.DarkMode = false;
        ChkKeepRatio.Location = new Point(101, 69);
        ChkKeepRatio.Margin = new Padding(0, 0, 0, 10);
        ChkKeepRatio.Name = "ChkKeepRatio";
        ChkKeepRatio.Size = new Size(156, 19);
        ChkKeepRatio.TabIndex = 11;
        ChkKeepRatio.Text = "[Keep ratio proportional]";
        ChkKeepRatio.UseVisualStyleBackColor = false;
        // 
        // LblResample
        // 
        LblResample.AutoSize = true;
        LblResample.BackColor = Color.Transparent;
        LblResample.DarkMode = false;
        LblResample.Location = new Point(16, 100);
        LblResample.Margin = new Padding(0, 2, 8, 3);
        LblResample.Name = "LblResample";
        LblResample.Size = new Size(69, 15);
        LblResample.TabIndex = 17;
        LblResample.Text = "[Resample:]";
        // 
        // CmbResample
        // 
        TableTop.SetColumnSpan(CmbResample, 2);
        CmbResample.DarkMode = false;
        CmbResample.Dock = DockStyle.Fill;
        CmbResample.DrawMode = DrawMode.OwnerDrawVariable;
        CmbResample.Location = new Point(101, 98);
        CmbResample.Margin = new Padding(0, 0, 0, 4);
        CmbResample.Name = "CmbResample";
        CmbResample.Size = new Size(148, 24);
        CmbResample.TabIndex = 12;
        // 
        // FrmResize
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(333, 347);
        ControlBox = false;
        Controls.Add(TableTop);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Name = "FrmResize";
        Text = "[Resize]";
        Controls.SetChildIndex(TableTop, 0);
        TableTop.ResumeLayout(false);
        TableTop.PerformLayout();
        PanResizeBy.ResumeLayout(false);
        PanResizeBy.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)NumHeight).EndInit();
        ((System.ComponentModel.ISupportInitialize)NumWidth).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TableLayoutPanel TableTop;
    private UI.ModernNumericUpDown NumWidth;
    private UI.ModernNumericUpDown NumHeight;
    private RadioButton RadResizeByPixels;
    private UI.ModernCheckBox ChkKeepRatio;
    private UI.ModernComboBox CmbResample;
    private UI.ModernLabel LblHeight;
    private UI.ModernLabel LblSize;
    private RadioButton RadResizeByPercentage;
    private UI.ModernLabel LblResample;
    private FlowLayoutPanel PanResizeBy;
    private UI.ModernLabel LblNewSize;
    private UI.ModernLabel LblCurrentSize;
    private UI.ModernLabel LblNewSizeValue;
    private UI.ModernLabel LblCurrentSizeValue;
    private ProgressBar ProgStatus;
    private UI.ModernLabel LblSizeUnit;
}