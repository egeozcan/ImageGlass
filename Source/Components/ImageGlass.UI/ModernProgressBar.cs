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
using Timer = System.Windows.Forms.Timer;

namespace ImageGlass.UI;

public class ModernProgressBar : ProgressBar
{
    private bool _darkMode = false;
    private IColors ColorPalatte => BHelper.GetThemeColorPalatte(_darkMode);
    private Timer? _timer;
    private bool _useMarqueeStyle;
    private float _marqueeValue = 0f;

    private float MarqueeWidth => Width / 5f;
    private float MinMarquee => -MarqueeWidth;
    private float MaxMarquee => Width + MarqueeWidth;


    #region Property Region

    /// <summary>
    /// Toggles dark mode for this <see cref="ModernButton"/> control.
    /// </summary>
    public bool DarkMode
    {
        get => _darkMode;
        set
        {
            _darkMode = value;

            Invalidate();
        }
    }

    /// <summary>
    /// Use Marquee style.
    /// </summary>
    public bool UseMarqueeStyle
    {
        get => _useMarqueeStyle;
        set
        {
            _useMarqueeStyle = value;

            if (_timer != null)
            {
                _timer.Tick -= MarqueeTimer_Tick;
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }


            if (value)
            {
                _timer = new Timer()
                {
                    Interval = 10,
                };

                _timer.Tick += MarqueeTimer_Tick;
                _timer.Start();
            }
        }
    }

    #endregion


    public ModernProgressBar()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint, true);

        BackColor = Color.Transparent;
    }

    protected override void Dispose(bool disposing)
    {
        if (_timer != null)
        {
            _timer.Tick -= MarqueeTimer_Tick;
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }

        base.Dispose(disposing);
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

        var borderRadius = BHelper.IsOS(WindowsOS.Win11OrLater) ? this.ScaleToDpi(2.5f) : 0;
        var borderWidth = this.ScaleToDpi(1.1f);
        var borderRect = new RectangleF()
        {
            X = e.ClipRectangle.X,
            Y = e.ClipRectangle.Y,
            Width = e.ClipRectangle.Width,
            Height = e.ClipRectangle.Height - borderWidth,
        };

        // limit drawing within the border rect
        using var borderRectPath = BHelper.GetRoundRectanglePath(borderRect, borderRadius);
        e.Graphics.SetClip(borderRectPath);


        // background
        using var bgBrush = new SolidBrush(ColorPalatte.ControlBg);
        e.Graphics.FillRoundedRectangle(bgBrush, borderRect, borderRadius);


        // value bar
        using var valueBrush = new SolidBrush(ColorPalatte.Accent);
        var valueRect = new RectangleF(borderRect.X, borderRect.Y,
            Value * borderRect.Width / (Maximum - Minimum),
            borderRect.Height);

        if (UseMarqueeStyle)
        {
            valueRect.X += _marqueeValue;
            valueRect.Width = MarqueeWidth;
        }
        e.Graphics.FillRoundedRectangle(valueBrush, valueRect, borderRadius);


        // border
        using var borderPen = new Pen(ColorPalatte.ControlBorder, borderWidth);
        e.Graphics.DrawRoundedRectangle(borderPen, borderRect, borderRadius);

    }


    protected override void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);

        if (_timer != null)
        {
            _timer.Enabled = Visible && UseMarqueeStyle;
        }
    }

    private void MarqueeTimer_Tick(object? sender, EventArgs e)
    {
        _marqueeValue += this.ScaleToDpi(2f);
        if (_marqueeValue > MaxMarquee) _marqueeValue = MinMarquee;

        this.Invalidate();
    }

}
