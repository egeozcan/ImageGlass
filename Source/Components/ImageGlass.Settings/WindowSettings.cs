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
using ImageGlass.Base.WinApi;

namespace ImageGlass.Settings;


/// <summary>
/// Provides extra and correct settings for Window
/// </summary>
public partial class WindowSettings
{

    /// <summary>
    /// Loads the given placement to window.
    /// </summary>
    public static void LoadPlacementToWindow(Form frm, Rectangle bounds, FormWindowState formState, bool autoCorrectBounds)
    {
        var isBoundsVisible = true;

        // test if the bounds is visible on a screen
        if (autoCorrectBounds)
        {
            var testBounds = bounds;
            testBounds.Inflate(-10, -10);

            isBoundsVisible = BHelper.IsVisibleOnAnyScreen(testBounds);
        }

        if (isBoundsVisible)
        {
            frm.Bounds = bounds;
        }
        else
        {
            // The saved position no longer exists (e.g. 2d monitor removed).
            // Prevent us from appearing off-screen.
            frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            frm.Size = new(bounds.Width, bounds.Height);
        }

        frm.WindowState = formState;

        // flush all pending paint tasks
        Application.DoEvents();
    }



    // FrmMain placement
    #region FrmMain placement

    /// <summary>
    /// Loads size and position for FrmMain window.
    /// </summary>
    public static void LoadFrmMainPlacementFromConfig(Form frm,
        int extraX = 0, int extraY = 0,
        bool autoCorrectBounds = true)
    {
        var bounds = new Rectangle(
            Config.FrmMainPositionX + extraX,
            Config.FrmMainPositionY + extraY,
            Config.FrmMainWidth,
            Config.FrmMainHeight);

        LoadPlacementToWindow(frm, bounds, Config.FrmMainState, autoCorrectBounds);
    }


    /// <summary>
    /// Saves size and position of FrmMain to config
    /// </summary>
    /// <param name="frm"></param>
    public static void SaveFrmMainPlacementToConfig(Form frm)
    {
        var extraW = 0;
        var extraH = 0;

        // if window is borderless, we need to get the size of bordered window.
        // if not, the frameless window size in next launch will be shrunk
        // https://github.com/d2phap/ImageGlass/issues/1924
        if (frm.FormBorderStyle == FormBorderStyle.None)
        {
            using var tempFrm = new Form();
            extraW = tempFrm.Bounds.Width - tempFrm.ClientSize.Width;
            extraH = tempFrm.Bounds.Height - tempFrm.ClientSize.Height;
        }

        var placement = WindowApi.GetWindowPlacement(frm);

        Config.FrmMainPositionX = placement.Bounds.Left;
        Config.FrmMainPositionY = placement.Bounds.Top;
        Config.FrmMainWidth = placement.Bounds.Width + extraW;
        Config.FrmMainHeight = placement.Bounds.Height + extraH;

        Config.FrmMainState = placement.State;
    }


    #endregion // FrmMain placement



    // FrmSettings placement
    #region FrmSettings placement

    /// <summary>
    /// Loads size and position for FrmSettings window.
    /// </summary>
    public static void LoadFrmSettingsPlacementFromConfig(Form frm, int extraX = 0, int extraY = 0)
    {
        var bounds = new Rectangle(
            Config.FrmSettingsPositionX + extraX,
            Config.FrmSettingsPositionY + extraY,
            Config.FrmSettingsWidth,
            Config.FrmSettingsHeight);

        LoadPlacementToWindow(frm, bounds, Config.FrmSettingsState, true);
    }


    /// <summary>
    /// Saves size and position of FrmSettings to config
    /// </summary>
    /// <param name="frm"></param>
    public static void SaveFrmSettingsPlacementToConfig(Form frm)
    {
        var placement = WindowApi.GetWindowPlacement(frm);

        Config.FrmSettingsPositionX = placement.Bounds.Left;
        Config.FrmSettingsPositionY = placement.Bounds.Top;
        Config.FrmSettingsWidth = placement.Bounds.Width;
        Config.FrmSettingsHeight = placement.Bounds.Height;

        Config.FrmSettingsState = placement.State;
    }


    #endregion // FrmSettings placement


}