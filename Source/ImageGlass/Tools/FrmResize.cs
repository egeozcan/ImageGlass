/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2010 - 2024 DUONG DIEU PHAP
Project homepage: https://imageglass.org

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using ImageGlass.Base;
using ImageGlass.Base.PhotoBox;
using ImageGlass.Settings;
using WicNet;

namespace ImageGlass.Tools;

public partial class FrmResize : DialogForm
{
    private readonly IProgress<ImageResizedEventArgs> _uiReporter;
    private CancellationTokenSource? _tokenSrc;
    private Size _inputSize;
    private Size _outputSize;

    private float MaxWidthValue => RadResizeByPixels.Checked
        ? Const.MAX_IMAGE_DIMENSION
        : 100f * Const.MAX_IMAGE_DIMENSION / _inputSize.Width;

    private float MaxHeightValue => RadResizeByPixels.Checked
        ? Const.MAX_IMAGE_DIMENSION
        : 100f * Const.MAX_IMAGE_DIMENSION / _inputSize.Height;


    /// <summary>
    /// Gets the output image after resized.
    /// </summary>
    public WicBitmapSource? Result { get; private set; }


    public FrmResize()
    {
        InitializeComponent();

        _uiReporter = new Progress<ImageResizedEventArgs>(ReportToUIThread);
    }



    // Override / Virtual methods
    #region Override / Virtual methods

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        ApplyTheme(Config.Theme.Settings.IsDarkMode);
        ApplyLanguage();


        // load initial data
        _inputSize =
            _outputSize = new(
                (int)Local.FrmMain.PicMain.SourceWidth,
                (int)Local.FrmMain.PicMain.SourceHeight);

        // thousands place for number
        LblCurrentSizeValue.Text = $"{_outputSize.Width:n0} x {_outputSize.Height:n0} px";


        // set type of resizing
        LoadResamplingMethods();

        var initSize = RadResizeByPixels.Checked ? _inputSize : new(100, 100);
        UpdateSize(initSize.Width, initSize.Height);

        NumWidth.Minimum = NumHeight.Minimum = 1;
        NumWidth.Maximum = Const.MAX_IMAGE_DIMENSION;
        NumHeight.Maximum = Const.MAX_IMAGE_DIMENSION;

        // add event listeners
        NumWidth.ValueChanged += NumSize_ValueChanged;
        NumHeight.ValueChanged += NumSize_ValueChanged;


        // check if image source is supported for resizing
        TableTop.Enabled =
            BtnAccept.Enabled =
                Local.FrmMain.PicMain.Source == Viewer.ImageSource.Direct2D;
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        NumWidth.Focus();
    }


    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        NumWidth.ValueChanged -= NumSize_ValueChanged;
        NumHeight.ValueChanged -= NumSize_ValueChanged;

        base.OnFormClosing(e);
    }

    protected override void ApplyTheme(bool darkMode, BackdropStyle? style = null)
    {
        SuspendLayout();


        TableTop.BackColor = Config.Theme.ColorPalatte.AppBg;
        CmbResample.DarkMode = darkMode;

        RadResizeByPixels.ForeColor =
            RadResizeByPercentage.ForeColor = Config.Theme.ColorPalatte.AppText;

        base.ApplyTheme(darkMode, style);
        ResumeLayout();
    }


    protected override int OnUpdateHeight(bool performUpdate = true)
    {
        var baseHeight = base.OnUpdateHeight(false);
        var formHeight = TableTop.Height + baseHeight;

        if (performUpdate && Height != formHeight)
        {
            Height = formHeight;
        }

        return formHeight;
    }


    protected override void OnAcceptButtonClicked()
    {
        // check if the value is updated
        // happens when user type and press enter without tab out
        if (_outputSize.Width != NumWidth.Value && NumWidth.Focused)
        {
            UpdateSize((float)NumWidth.Value, null);
        }
        else if (_outputSize.Height != NumHeight.Value && NumHeight.Focused)
        {
            UpdateSize(null, (float)NumHeight.Value);
        }


        // get the final size
        var finalSize = RadResizeByPercentage.Checked
            ? GetOutputSizeInPixel().ToSize()
            : _outputSize;

        var samplingMethod = (ImageResamplingMethod)CmbResample.SelectedIndex;

        // size is not changed
        if (finalSize.Width == _inputSize.Width && finalSize.Height == _inputSize.Height)
        {
            _uiReporter.Report(new("resizer:end"));
            return;
        }


        _tokenSrc?.Cancel();
        _tokenSrc = new();

        _ = BHelper.RunAsThread(async () => await SubmitResizeAsync(finalSize, samplingMethod));
    }

    protected override void OnCancelButtonClicked()
    {
        _tokenSrc?.Cancel();

        base.OnCancelButtonClicked();
    }


    protected override void OnRequestUpdatingLanguage()
    {
        ApplyLanguage();
    }


    #endregion // Override / Virtual methods



    // Private methods
    #region Private methods

    private void ApplyLanguage()
    {
        Text = Config.Language[$"{nameof(FrmMain)}.{nameof(FrmMain.MnuResizeTool)}"];

        BtnAccept.Text = Config.Language["_._OK"];
        BtnCancel.Text = Config.Language["_._Cancel"];
        BtnApply.Text = Config.Language["_._Apply"];

        RadResizeByPixels.Text = Config.Language[$"{nameof(FrmResize)}.{nameof(RadResizeByPixels)}"];
        RadResizeByPercentage.Text = Config.Language[$"{nameof(FrmResize)}.{nameof(RadResizeByPercentage)}"];

        LblSize.Text = Config.Language[$"{nameof(FrmCrop)}.{nameof(LblSize)}"];
        ChkKeepRatio.Text = Config.Language[$"{nameof(FrmResize)}.{nameof(ChkKeepRatio)}"];
        LblResample.Text = Config.Language[$"{nameof(FrmResize)}.{nameof(LblResample)}"];
        LblCurrentSize.Text = Config.Language[$"{nameof(FrmResize)}.{nameof(LblCurrentSize)}"];
        LblNewSize.Text = Config.Language[$"{nameof(FrmResize)}.{nameof(LblNewSize)}"];
    }


    private void LoadResamplingMethods()
    {
        // use cache for the next open
        if (CmbResample.Items.Count > 0) return;

        foreach (ImageResamplingMethod method in Enum.GetValues<ImageResamplingMethod>())
        {
            var displayName = string.Empty;
            var methodKey = Enum.GetName(method);
            var langPath = $"_.{nameof(ImageInterpolation)}._{methodKey}";

            if (!Config.Language.TryGetValue(langPath, out displayName))
            {
                displayName = methodKey;
            }

            CmbResample.Items.Add(displayName);
        }

        CmbResample.SelectedIndex = 0;
    }


    private void RadResizeByPixels_CheckedChanged(object? sender, EventArgs e)
    {
        var usePixels = RadResizeByPixels.Checked;

        // convert percentage to pixels
        if (usePixels)
        {
            var size = GetOutputSizeInPixel();

            UpdateSize(size.Width, size.Height, true);
            LblSizeUnit.Text = "px";
        }
    }


    private void RadResizeByPercentage_CheckedChanged(object sender, EventArgs e)
    {
        var usePercentage = RadResizeByPercentage.Checked;

        // convert pixels to percentage
        if (usePercentage)
        {
            var size = GetOutputSizeInPercentage();

            UpdateSize(size.Width, size.Height, true);
            LblSizeUnit.Text = "%";
        }
    }


    private void NumSize_ValueChanged(object? sender, EventArgs e)
    {
        // disable the events
        NumWidth.ValueChanged -= NumSize_ValueChanged;
        NumHeight.ValueChanged -= NumSize_ValueChanged;

        UpdateSize((int)NumWidth.Value, (int)NumHeight.Value);

        // re-enable the events
        NumWidth.ValueChanged += NumSize_ValueChanged;
        NumHeight.ValueChanged += NumSize_ValueChanged;
    }


    private async Task SubmitResizeAsync(Size size, ImageResamplingMethod samplingMethod)
    {
        _uiReporter.Report(new("resizer:start"));


        // get the current bitmap
        var bmp = await Local.FrmMain.GetCurrentImageDataAsync();
        if (bmp == null)
        {
            _uiReporter.Report(new("resizer:end"));
            return;
        }


        // perform resizing
        WicBitmapSource? resizedBmp = null;
        if (!_tokenSrc.IsCancellationRequested)
        {
            resizedBmp = await BHelper.ResizeImageAsync(bmp, size.Width, size.Height, samplingMethod);
        }

        _uiReporter.Report(new("resizer:end", resizedBmp));
    }


    private void ReportToUIThread(ImageResizedEventArgs e)
    {
        if (e.Type == "resizer:start")
        {
            SetUiBusy(true);
            return;
        }

        if (e.Type == "resizer:end")
        {
            if (e.Image != null && !_tokenSrc.IsCancellationRequested)
            {
                Result = e.Image;
                base.OnAcceptButtonClicked();
            }
            else
            {
                base.OnCancelButtonClicked();
            }

            SetUiBusy(false);
            return;
        }

        if (e.Type == "update:height")
        {
            this.OnUpdateHeight();
        }
    }


    private void SetUiBusy(bool isBusy)
    {
        ProgStatus.Visible = isBusy;
        BtnAccept.Enabled = !isBusy;

        PanResizeBy.Enabled = !isBusy;
        NumWidth.Enabled = !isBusy;
        NumHeight.Enabled = !isBusy;
        CmbResample.Enabled = !isBusy;
        ChkKeepRatio.Enabled = !isBusy;

        // delay showing progress bar
        if (isBusy)
        {
            Task.Delay(1000).ContinueWith(t =>
            {
                _uiReporter.Report(new("update:height"));
            });
        }
        else
        {
            this.OnUpdateHeight();
        }
    }


    private void UpdateSize(float? width = null, float? height = null, bool disableEvents = false)
    {
        if (width == null && height == null) return;

        // disable the events
        if (disableEvents)
        {
            NumWidth.ValueChanged -= NumSize_ValueChanged;
            NumHeight.ValueChanged -= NumSize_ValueChanged;
        }

        var usePixels = RadResizeByPixels.Checked;
        var newWidth = 1f * (width ?? _outputSize.Width);
        var newHeight = 1f * (height ?? _outputSize.Height);

        // limit the max size
        newWidth = Math.Min(newWidth, MaxWidthValue);
        newHeight = Math.Min(newHeight, MaxHeightValue);

        // keep ratio
        if (ChkKeepRatio.Checked)
        {
            // get the new size
            var ratio = usePixels
                ? 1f * _inputSize.Width / _inputSize.Height
                : 1f;


            // if changing width
            if (newHeight == _outputSize.Height)
            {
                newHeight = Math.Max(1, newWidth / ratio);
            }
            // if changing height
            if (newWidth == _outputSize.Width)
            {
                newWidth = Math.Max(1, newHeight * ratio);
            }
        }

        // finalize size value
        _outputSize = new Size((int)Math.Round(newWidth), (int)Math.Round(newHeight));

        NumWidth.Value = _outputSize.Width;
        NumHeight.Value = _outputSize.Height;

        // show size preview
        if (usePixels)
        {
            LblNewSizeValue.Text = $"{_outputSize.Width:n0} x {_outputSize.Height:n0} px";
        }
        else
        {
            var size = GetOutputSizeInPixel();
            LblNewSizeValue.Text = $"{size.Width:n0} x {size.Height:n0} px";
        }


        // re-enable the events
        if (disableEvents)
        {
            NumWidth.ValueChanged += NumSize_ValueChanged;
            NumHeight.ValueChanged += NumSize_ValueChanged;
        }
    }


    private SizeF GetOutputSizeInPercentage()
    {
        var w = 100f * _outputSize.Width / _inputSize.Width;
        var h = 100f * _outputSize.Height / _inputSize.Height;

        return new SizeF(w, h);
    }


    private SizeF GetOutputSizeInPixel()
    {
        var w = _outputSize.Width * _inputSize.Width / 100f;
        var h = _outputSize.Height * _inputSize.Height / 100f;

        return new SizeF(w, h);
    }

    #endregion // Private methods

}



public class ImageResizedEventArgs(string type = "", WicBitmapSource? img = null) : EventArgs
{
    public WicBitmapSource? Image { get; set; } = img;
    public string Type { get; set; } = type;
}

