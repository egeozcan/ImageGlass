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
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using ImageGlass.Base;
using ImageGlass.Base.WinApi;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace ImageGlass.UI;


/// <summary>
/// Make the form movable when dragging itself or its controls
/// </summary>
public class MovableForm
{
    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HT_CAPTION = 0x2;

    private readonly ModernForm _form;
    private bool _isKeyDown = true;
    private Control? _moverControl = null;
    private bool _isMoverControlDarkMode = true;


    #region Public props

    /// <summary>
    /// Manually enable / disable moving
    /// </summary>
    public bool EnableFreeMoving { get; set; } = true;

    /// <summary>
    /// Sets visibility of mover control.
    /// </summary>
    public bool ShowMover { get; set; } = false;

    /// <summary>
    /// Gets, sets the mouse button press for moving
    /// </summary>
    public MouseButtons MouseButton { get; set; } = MouseButtons.Left;

    /// <summary>
    /// Gets, sets the Key press for moving
    /// </summary>
    public Keys Key { get; set; } = Keys.None;

    /// <summary>
    /// Gets, sets the controls that do not require special Key holding to move
    /// </summary>
    public HashSet<string> FreeMoveControlNames { get; set; } = new HashSet<string>();

    #endregion // Public props


    /// <summary>
    /// Initialize the MovableForm
    /// </summary>
    /// <param name="form">The form to make it movable</param>
    public MovableForm(ModernForm form) => _form = form;


    #region Public methods

    /// <summary>
    /// Enable moving ability on the given controls
    /// </summary>
    /// <param name="controls"></param>
    public void Enable(params Control[] controls)
    {
        _isKeyDown = Key == Keys.None;

        _form.KeyDown += Form_KeyDown;
        _form.KeyUp += Form_KeyUp;
        _form.MouseDown += Event_MouseDown;


        foreach (var ctr in controls)
        {
            ctr.MouseDown += Event_MouseDown;
        }
    }

    /// <summary>
    /// Disable moving ability on the given controls
    /// </summary>
    /// <param name="controls"></param>
    public void Disable(params Control[] controls)
    {
        _form.KeyDown -= Form_KeyDown;
        _form.KeyUp -= Form_KeyUp;
        _form.MouseDown -= Event_MouseDown;

        foreach (var ctr in controls)
        {
            ctr.MouseDown -= Event_MouseDown;
        }
    }

    #endregion // Public methods


    #region Events: Free form moving

    private void Form_KeyDown(object? sender, KeyEventArgs e)
    {
        if (Key == Keys.None)
        {
            _isKeyDown = true;
        }
        else
        {
            _isKeyDown = e.KeyData == Key;
        }

        if (_isKeyDown && ShowMover)
        {
            SetMoverControlVisibility(true);
        }
    }


    private void Form_KeyUp(object? sender, KeyEventArgs e)
    {
        _isKeyDown = Key == Keys.None;
        SetMoverControlVisibility(false);
    }


    private void Event_MouseDown(object? sender, MouseEventArgs e)
    {
        // check if 'sender' can move without keydown event
        var control = (Control?)sender;
        var isFreeMove = control.Name == _moverControl?.Name
            || (FreeMoveControlNames.Count > 0
                && FreeMoveControlNames.Contains(control.Name));

        if (e.Clicks == 1
            && e.Button == MouseButton
            && EnableFreeMoving
            && (_isKeyDown || isFreeMove))
        {
            PInvoke.ReleaseCapture();
            PInvoke.SendMessage(new HWND(_form.Handle), WM_NCLBUTTONDOWN, new WPARAM(HT_CAPTION), new LPARAM(0));
        }
    }


    private void SetMoverControlVisibility(bool visible)
    {
        if (_moverControl != null)
        {
            _form.Controls.Remove(_moverControl);
            _moverControl.MouseDown -= Event_MouseDown;
        }


        if (visible)
        {
            _moverControl ??= new Panel()
            {
                Name = "PanMoverControl",
                Width = DpiApi.Scale(100),
                Height = DpiApi.Scale(100),
                ForeColor = Color.White,
                BackgroundImageLayout = ImageLayout.Center,
                Cursor = Cursors.SizeAll,
            };

            // set movable icon
            if (_moverControl.BackgroundImage == null || _isMoverControlDarkMode != _form.DarkMode)
            {
                _isMoverControlDarkMode = _form.DarkMode;

                var iconSize = DpiApi.Scale(50u);
                var svgPath = IconFile.GetFullPath(IconName.ArrowMove);
                _moverControl.BackgroundImage?.Dispose();
                _moverControl.BackgroundImage = BHelper.ToGdiPlusBitmapFromSvg(svgPath, _form.DarkMode, iconSize, iconSize);
            }

            // set center position
            _moverControl.Left = _form.Width / 2 - _moverControl.Width / 2;
            _moverControl.Top = _form.Height / 2 - _moverControl.Height / 2 - SystemInformation.CaptionHeight;

            // set mouse event
            _moverControl.MouseDown += Event_MouseDown;

            _form.Controls.Add(_moverControl);
            _moverControl.BringToFront();
        }
    }


    #endregion // Events: Free form moving


}

