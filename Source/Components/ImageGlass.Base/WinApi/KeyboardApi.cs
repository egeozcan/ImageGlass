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
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;

namespace ImageGlass.Base.WinApi;

public class KeyboardApi
{

    [DllImport("user32.dll")]
    private static extern bool ToAsciiEx(int virtualKey, int scanCode, byte[] lpKeyState, ref uint lpChar, int uFlags, IntPtr dwhkl);



    /// <summary>
    /// Converts virtual key to string.
    /// </summary>
    public static unsafe char KeyCodeToChar(Keys key, bool withShiftKey)
    {
        var lpChar = 0u;
        var lpKeyState = new byte[256];

        if (withShiftKey)
        {
            foreach (Keys sKey in Enum.GetValues(typeof(Keys)))
            {
                if (sKey.HasFlag(Keys.ShiftKey))
                {
                    lpKeyState[(int)sKey] = 0x80;
                }
            }
        }

        // always use en-US keyboard layout
        nint langHandle;
        try
        {
            var culture = new System.Globalization.CultureInfo("en-US");
            langHandle = InputLanguage.FromCulture(culture).Handle;
        }
        catch
        {
            langHandle = InputLanguage.DefaultInputLanguage.Handle;
        }

        var keyboardLayoutPtr = new HKL(langHandle);
        var virtualKeyCode = (uint)key;
        var scanCode = PInvoke.MapVirtualKey(virtualKeyCode, MAP_VIRTUAL_KEY_TYPE.MAPVK_VK_TO_VSC);

        _ = ToAsciiEx((int)key, (int)scanCode, lpKeyState, ref lpChar, 0, keyboardLayoutPtr);

        return (char)lpChar;
    }


    /// <summary>
    /// Convert character to virtual key
    /// </summary>
    public static Keys CharToKeyCode(char c)
    {
        // always use en-US keyboard layout
        nint langHandle;
        try
        {
            var culture = new System.Globalization.CultureInfo("en-US");
            langHandle = InputLanguage.FromCulture(culture).Handle;
        }
        catch
        {
            langHandle = InputLanguage.DefaultInputLanguage.Handle;
        }

        var keyboardLayoutPtr = new HKL(langHandle);
        var vkey = PInvoke.VkKeyScanEx(c, keyboardLayoutPtr);
        var keys = (Keys)(vkey & 0xff);
        var modifiers = vkey >> 8;

        if ((modifiers & 1) != 0) keys |= Keys.Shift;
        if ((modifiers & 2) != 0) keys |= Keys.Control;
        if ((modifiers & 4) != 0) keys |= Keys.Alt;

        return keys;
    }

}
