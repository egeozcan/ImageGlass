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
using DirectN;
using System.Runtime.InteropServices;
using WicNet;

namespace ImageGlass.Base;

public static class ID2D1Bitmap1Extensions
{
    /// <summary>
    /// Create bitmap for CPU Read only.
    /// </summary>
    public static IComObject<ID2D1Bitmap1>? CreateCpuReadBitmap(
        this IComObject<ID2D1Bitmap1>? srcBitmap1,
        IComObject<ID2D1DeviceContext6>? dc)
    {
        if (srcBitmap1 == null || dc == null) return null;

        var bmpProps = new D2D1_BITMAP_PROPERTIES1()
        {
            bitmapOptions = D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_CANNOT_DRAW | D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_CPU_READ,
            pixelFormat = new D2D1_PIXEL_FORMAT()
            {
                alphaMode = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_PREMULTIPLIED,
                format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM,
            },
            dpiX = 96.0f,
            dpiY = 96.0f,
        };


        // get bitmap size
        var dsize = srcBitmap1.GetSize().ToD2D_SIZE_U();


        // create CPU-read bitmap
        var bitmapCpu = dc.CreateBitmap<ID2D1Bitmap1>(dsize, bmpProps);
        bitmapCpu.CopyFromBitmap(srcBitmap1);

        return bitmapCpu;
    }


    /// <summary>
    /// Gets pixel data of <see cref="ID2D1Bitmap1"/> bitmap.
    /// </summary>
    public static (byte[] Pixels, Size Size, int Stripe) GetBitmapData(
        this IComObject<ID2D1Bitmap1>? srcBitmap1,
        IComObject<ID2D1DeviceContext6>? dc)
    {
        var pixels = Span<byte>.Empty;
        var size = new Size();
        var stripe = 0;

        if (srcBitmap1 == null || dc == null) return (pixels.ToArray(), size, stripe);


        // get bitmap size
        var dsize = srcBitmap1.GetSize();
        size = new((int)dsize.width, (int)dsize.height);

        // create CPU-read bitmap
        using var bitmapCpu = srcBitmap1.CreateCpuReadBitmap(dc);

        // copy all raw pixel data
        var map = bitmapCpu.Map(D2D1_MAP_OPTIONS.D2D1_MAP_OPTIONS_READ);
        stripe = (int)map.pitch;
        var totalDataSize = size.Height * stripe;

        var bytes = new byte[totalDataSize];
        Marshal.Copy(map.bits, bytes, 0, totalDataSize);
        bitmapCpu.Unmap();


        // process raw pixel data
        // since pixel data is D2D1_ALPHA_MODE_PREMULTIPLIED,
        // we need to re-calculate the color values
        pixels = bytes.AsSpan();
        for (int i = 0; i < pixels.Length; i += 4)
        {
            var a = pixels[i + 3];
            var alphaPremultiplied = a / 255f;

            pixels[i + 2] = (byte)(pixels[i + 2] / alphaPremultiplied); // r
            pixels[i + 1] = (byte)(pixels[i + 1] / alphaPremultiplied); // g
            pixels[i] = (byte)(pixels[i] / alphaPremultiplied); // b
        }

        return (pixels.ToArray(), size, stripe);
    }


    /// <summary>
    /// Converts <see cref="ID2D1Bitmap1"/> to <see cref="WicBitmapSource"/>
    /// </summary>
    public static WicBitmapSource? ToWicBitmapSource(this IComObject<ID2D1Bitmap1>? srcBitmap1, IComObject<ID2D1DeviceContext6>? dc)
    {
        var data = srcBitmap1.GetBitmapData(dc);
        if (data.Pixels.Length == 0) return null;

        var bmp = WicBitmapSource.FromMemory(data.Size.Width, data.Size.Height,
            WicPixelFormat.GUID_WICPixelFormat32bppPBGRA, data.Stripe, data.Pixels);

        return bmp;
    }


    /// <summary>
    /// Clones <see cref="ID2D1Bitmap1"/>.
    /// </summary>
    public static IComObject<ID2D1Bitmap1>? Clone(this IComObject<ID2D1Bitmap1>? srcBitmap1, IComObject<ID2D1DeviceContext6>? dc)
    {
        if (srcBitmap1 == null) return null;


        var bmpProps = new D2D1_BITMAP_PROPERTIES1()
        {
            bitmapOptions = D2D1_BITMAP_OPTIONS.D2D1_BITMAP_OPTIONS_TARGET,
            pixelFormat = new D2D1_PIXEL_FORMAT()
            {
                alphaMode = D2D1_ALPHA_MODE.D2D1_ALPHA_MODE_PREMULTIPLIED,
                format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM,
            },
            dpiX = 96.0f,
            dpiY = 96.0f,
        };

        // create an empty bitmap
        dc.Object.GetImageLocalBounds(srcBitmap1.Object, out var outputRect);
        var newD2dBitmap = dc.CreateBitmap<ID2D1Bitmap1>(outputRect.SizeU, bmpProps);

        // copy bitmap source
        newD2dBitmap.CopyFromBitmap(srcBitmap1);

        return newD2dBitmap;
    }


    /// <summary>
    /// Gets pixel color.
    /// </summary>
    /// <returns>
    /// <see cref="Color.Transparent"/> if
    /// <paramref name="srcBitmap1"/> or <paramref name="dc"/> is <c>null</c>.
    /// </returns>
    public static Color GetPixelColor(this IComObject<ID2D1Bitmap1>? srcBitmap1,
        IComObject<ID2D1DeviceContext6>? dc, int x, int y)
    {
        if (srcBitmap1 == null || dc == null) return Color.Transparent;

        // create CPU-read bitmap
        using var bitmapCpu = srcBitmap1.CreateCpuReadBitmap(dc);
        var map = bitmapCpu.Map(D2D1_MAP_OPTIONS.D2D1_MAP_OPTIONS_READ);
        var startIndex = (y * map.pitch) + (x * 4);

        var bytes = new byte[4];
        Marshal.Copy((nint)(map.bits + startIndex), bytes, 0, bytes.Length);
        bitmapCpu.Unmap();


        // since pixel data is D2D1_ALPHA_MODE_PREMULTIPLIED,
        // we need to re-calculate the color values
        var a = bytes[3];
        var alphaPremultiplied = a / 255f;

        var r = (byte)(bytes[2] / alphaPremultiplied);
        var g = (byte)(bytes[1] / alphaPremultiplied);
        var b = (byte)(bytes[0] / alphaPremultiplied);


        var color = Color.FromArgb(a, r, g, b);

        return color;
    }

}
