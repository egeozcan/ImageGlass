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

using Cysharp.Text;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace ImageGlass.Base;

public partial class BHelper
{
    /// <summary>
    /// Center the given rectangle to the rectangle.
    /// </summary>
    /// <param name="rect1"></param>
    /// <param name="rect2"></param>
    /// <param name="limitRect1Size"></param>
    public static Rectangle CenterRectToRect(Rectangle rect1, Rectangle rect2, bool limitRect1Size = false)
    {
        var x = rect2.X + ((rect2.Width - rect1.Width) / 2);
        var y = rect2.Y + ((rect2.Height - rect1.Height) / 2);
        var width = rect1.Width;
        var height = rect1.Height;

        if (limitRect1Size)
        {
            x = Math.Max(rect2.X, x);
            y = Math.Max(rect2.Y, y);
            width = Math.Min(rect1.Width, rect2.Width);
            height = Math.Min(rect1.Height, rect2.Height);
        }

        return new Rectangle(x, y, width, height);
    }


    /// <summary>
    /// Get all controls by type
    /// </summary>
    public static IEnumerable<Control> GetAllControls(Control control, Type type)
    {
        var controls = control.Controls.Cast<Control>();

        return controls.SelectMany(ctrl => GetAllControls(ctrl, type))
                                  .Concat(controls)
                                  .Where(c => c.GetType() == type);
    }


    /// <summary>
    /// Checks if the given rectangle is visible on any screen
    /// </summary>
    public static bool IsVisibleOnAnyScreen(Rectangle rect)
    {
        return Screen.AllScreens.Any(i => i.WorkingArea.IntersectsWith(rect));
    }


    /// <summary>
    /// Checks if the given Windows version is matched.
    /// </summary>
    public static bool IsOS(WindowsOS ver)
    {
        if (ver == WindowsOS.Win11_22H2_OrLater)
        {
            return Environment.OSVersion.Version.Major >= 10
                && Environment.OSVersion.Version.Build >= 22621;
        }

        if (ver == WindowsOS.Win11OrLater)
        {
            return Environment.OSVersion.Version.Major >= 10
                && Environment.OSVersion.Version.Build >= 22000;
        }

        if (ver == WindowsOS.Win10)
        {
            return Environment.OSVersion.Version.Major == 10
                && Environment.OSVersion.Version.Build < 22000;
        }

        if (ver == WindowsOS.Win10OrLater)
        {
            return Environment.OSVersion.Version.Major >= 10;
        }


        return false;
    }


    /// <summary>
    /// Checks if the OS is Windows 10 or greater or equals the given build number.
    /// </summary>
    /// <param name="build">Build number of Windows.</param>
    public static bool IsOSBuildOrGreater(int build)
    {
        return Environment.OSVersion.Version.Major >= 10
            && Environment.OSVersion.Version.Build >= build;
    }


    /// <summary>
    /// Gets selection rectangle from 2 points.
    /// </summary>
    /// <param name="point1">The first point</param>
    /// <param name="point2">The second point</param>
    /// <param name="aspectRatio">Aspect ratio</param>
    /// <param name="limitRect">The rectangle to limit the selection</param>
    public static RectangleF GetSelection(PointF? point1, PointF? point2,
        SizeF aspectRatio, float srcWidth, float srcHeight,
        RectangleF limitRect)
    {
        var selectedArea = new RectangleF();
        var fromPoint = point1 ?? new PointF();
        var toPoint = point2 ?? new PointF();

        if (fromPoint.IsEmpty || toPoint.IsEmpty) return selectedArea;

        // swap fromPoint and toPoint value if toPoint is less than fromPoint
        if (toPoint.X < fromPoint.X)
        {
            var tempX = fromPoint.X;
            fromPoint.X = toPoint.X;
            toPoint.X = tempX;
        }
        if (toPoint.Y < fromPoint.Y)
        {
            var tempY = fromPoint.Y;
            fromPoint.Y = toPoint.Y;
            toPoint.Y = tempY;
        }

        float width = Math.Abs(fromPoint.X - toPoint.X);
        float height = Math.Abs(fromPoint.Y - toPoint.Y);

        selectedArea.X = fromPoint.X;
        selectedArea.Y = fromPoint.Y;
        selectedArea.Width = width;
        selectedArea.Height = height;

        // limit the selected area to the limitRect
        selectedArea.Intersect(limitRect);


        // free aspect ratio
        if (aspectRatio.Width <= 0 || aspectRatio.Height <= 0)
            return selectedArea;


        var wRatio = aspectRatio.Width / aspectRatio.Height;
        var hRatio = aspectRatio.Height / aspectRatio.Width;

        // update selection size according to the ratio
        if (wRatio > hRatio)
        {
            selectedArea.Height = selectedArea.Width / wRatio;

            if (selectedArea.Bottom >= limitRect.Bottom)
            {
                var maxHeight = limitRect.Bottom - selectedArea.Y;
                selectedArea.Width = maxHeight * wRatio;
                selectedArea.Height = maxHeight;
            }
        }
        else
        {
            selectedArea.Width = selectedArea.Height / hRatio;

            if (selectedArea.Right >= limitRect.Right)
            {
                var maxWidth = limitRect.Right - selectedArea.X; ;
                selectedArea.Width = maxWidth;
                selectedArea.Height = maxWidth * hRatio;
            }
        }


        return selectedArea;
    }


    /// <summary>
    /// Opens ImageGlass site om Microsoft Store.
    /// </summary>
    public static void OpenImageGlassMsStore()
    {
        var campaignId = $"IgInAppBadgeV{App.Version}";
        var source = "AboutWindow";

        try
        {
            var url = $"ms-windows-store://pdp/?productid={Const.MS_APPSTORE_ID}&cid={campaignId}&referrer=appbadge&source={source}";

            _ = BHelper.OpenUrlAsync(url);
        }
        catch
        {
            try
            {
                var url = $"https://www.microsoft.com/store/productId/{Const.MS_APPSTORE_ID}?cid={campaignId}&referrer=appbadge&source={source}";

                _ = BHelper.OpenUrlAsync(url);
            }
            catch { }
        }
    }


    /// <summary>
    /// Create an unique key for the input file.
    /// </summary>
    public static string CreateUniqueFileKey(string filePath, Size? size = null)
    {
        var fi = new FileInfo(filePath);
        using var sb = ZString.CreateStringBuilder();

        sb.Append(filePath);
        sb.Append(':');
        sb.Append(fi.LastWriteTimeUtc.ToBinary());

        // Thumbnail size
        if (size is Size s)
        {
            sb.Append(':');
            sb.Append(s.Width);
            sb.Append(',');
            sb.Append(s.Height);
        }


        var hash = MD5.HashData(Encoding.ASCII.GetBytes(sb.ToString()));

        return Convert.ToHexString(hash).ToLowerInvariant();
    }

}