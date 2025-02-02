/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2010 - 2025 DUONG DIEU PHAP
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
using ImageGlass.Base.WinApi;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ImageGlass.UI;

public class ModernRadioButton : RadioButton
{
    private ModernControlState _controlState = ModernControlState.Normal;
    private bool _spacePressed = false;
    private bool _darkMode = false;
    private IColors ColorPalatte => BHelper.GetThemeColorPalatte(_darkMode);



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


    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Appearance Appearance
    {
        get { return base.Appearance; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool AutoEllipsis
    {
        get { return base.AutoEllipsis; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Image BackgroundImage
    {
        get { return base.BackgroundImage; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImageLayout BackgroundImageLayout
    {
        get { return base.BackgroundImageLayout; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool FlatAppearance
    {
        get { return false; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new FlatStyle FlatStyle
    {
        get { return base.FlatStyle; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Image Image
    {
        get { return base.Image; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ContentAlignment ImageAlign
    {
        get { return base.ImageAlign; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int ImageIndex
    {
        get { return base.ImageIndex; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string ImageKey
    {
        get { return base.ImageKey; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImageList ImageList
    {
        get { return base.ImageList; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ContentAlignment TextAlign
    {
        get { return base.TextAlign; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new TextImageRelation TextImageRelation
    {
        get { return base.TextImageRelation; }
    }

    #endregion


    public ModernRadioButton()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint, true);

        BackColor = Color.Transparent;
    }


    private void SetControlState(ModernControlState controlState)
    {
        if (_controlState != controlState)
        {
            _controlState = controlState;
            Invalidate();
        }
    }



    #region Event Handler Region

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (_spacePressed)
            return;

        if (e.Button == MouseButtons.Left)
        {
            if (ClientRectangle.Contains(e.Location))
                SetControlState(ModernControlState.Pressed);
            else
                SetControlState(ModernControlState.Hover);
        }
        else
        {
            SetControlState(ModernControlState.Hover);
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (!ClientRectangle.Contains(e.Location))
            return;

        SetControlState(ModernControlState.Pressed);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);

        if (_spacePressed)
            return;

        SetControlState(ModernControlState.Normal);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);

        if (_spacePressed)
            return;

        SetControlState(ModernControlState.Normal);
    }

    protected override void OnMouseCaptureChanged(EventArgs e)
    {
        base.OnMouseCaptureChanged(e);

        if (_spacePressed)
            return;

        var location = Cursor.Position;

        if (!ClientRectangle.Contains(location))
            SetControlState(ModernControlState.Normal);
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);

        Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);

        _spacePressed = false;

        var location = Cursor.Position;

        if (!ClientRectangle.Contains(location))
            SetControlState(ModernControlState.Normal);
        else
            SetControlState(ModernControlState.Hover);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.KeyCode == Keys.Space)
        {
            _spacePressed = true;
            SetControlState(ModernControlState.Pressed);
        }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);

        if (e.KeyCode == Keys.Space)
        {
            _spacePressed = false;

            var location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
                SetControlState(ModernControlState.Normal);
            else
                SetControlState(ModernControlState.Hover);
        }
    }

    #endregion



    protected override void OnPaint(PaintEventArgs e)
    {
        // draw parent background
        ButtonRenderer.DrawParentBackground(e.Graphics, e.ClipRectangle, this);

        var g = e.Graphics;

        var textColor = ColorPalatte.AppText;
        var borderColor = ColorPalatte.ControlBorder;
        var fillColor = Checked ? ColorPalatte.ControlBorderAccent : ColorPalatte.ControlBg;


        if (Enabled)
        {
            if (_controlState == ModernControlState.Hover)
            {
                borderColor = borderColor.WithBrightness(0.3f);
                fillColor = fillColor.WithBrightness(0.1f);
            }
            else if (_controlState == ModernControlState.Pressed)
            {
                borderColor = borderColor.WithBrightness(-0.15f);
                fillColor = fillColor.WithBrightness(-0.2f);
            }

            if (Focused)
            {
                borderColor = ColorPalatte.ControlBorderAccent;
            }
        }
        else
        {
            textColor = ColorPalatte.AppTextDisabled;
            borderColor = ColorPalatte.ControlBorder;
            fillColor = ColorPalatte.ControlBorder;
        }

        var initX = Padding.Left;
        var initY = Padding.Top;
        var boxSize = Font.Height * 0.8f;
        var boxRect = new RectangleF(
            initX,
            initY + boxSize / 4,
            boxSize,
            boxSize);

        // draw radio border
        using (var p = new Pen(borderColor, DpiApi.Scale(1f)))
        {
            p.Alignment = PenAlignment.Outset;
            p.LineJoin = LineJoin.Round;
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(p, boxRect);
            g.SmoothingMode = SmoothingMode.None;
        }

        if (Checked)
        {
            // fill radio box
            using (var b = new SolidBrush(fillColor))
            {
                var fillSize = Font.Height * 0.5f;
                var fillBox = new RectangleF(
                    boxRect.X + (boxSize - fillSize) / 2,
                    boxRect.Y + (boxSize - fillSize) / 2,
                    fillSize, fillSize);


                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillEllipse(b, fillBox);
                g.SmoothingMode = SmoothingMode.None;
            }
        }

        // draw text
        using (var b = new SolidBrush(textColor))
        {
            var stringFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near,
                Trimming = StringTrimming.EllipsisWord,
            };

            var modRect = new RectangleF(
                boxRect.Right + boxSize / 3,
                initY,
                Bounds.Width - boxSize - Padding.Horizontal,
                Bounds.Height - Padding.Vertical);

            g.DrawString(Text, Font, b, modRect, stringFormat);
        }
    }


}
