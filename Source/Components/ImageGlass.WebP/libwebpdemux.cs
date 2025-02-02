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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace ImageGlass.WebP;


[SuppressUnmanagedCodeSecurityAttribute]
internal sealed partial class LibWebpDemux
{
    // from WebPAnimDecoder API
    private static readonly int WEBP_DEMUX_ABI_VERSION = 0x0107;



    /// <summary>Should always be called, to initialize a fresh WebPAnimDecoderOptions
    /// structure before modification. Returns false in case of version mismatch.
    /// WebPAnimDecoderOptionsInit() must have succeeded before using the
    /// 'dec_options' object.</summary>
    /// <param name="dec_options">(in/out) options used for decoding animation</param>
    /// <returns>true/false - success/error</returns>
    internal static bool WebPAnimDecoderOptionsInit(ref WebPAnimDecoderOptions dec_options)
    {
        return WebPAnimDecoderOptionsInitInternal(ref dec_options, WEBP_DEMUX_ABI_VERSION) == 1;
    }
    [LibraryImport("libwebpdemux.dll", EntryPoint = "WebPAnimDecoderOptionsInitInternal")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private static partial int WebPAnimDecoderOptionsInitInternal(ref WebPAnimDecoderOptions dec_options, int WEBP_DEMUX_ABI_VERSION);



    /// <summary>
    /// Creates and initializes a WebPAnimDecoder object.
    /// </summary>
    /// <param name="webp_data">(in) WebP bitstream. This should remain unchanged during the 
    ///     lifetime of the output WebPAnimDecoder object.</param>
    /// <param name="dec_options">(in) decoding options. Can be passed NULL to choose 
    ///     reasonable defaults (in particular, color mode MODE_RGBA 
    ///     will be picked).</param>
    /// <returns>A pointer to the newly created WebPAnimDecoder object, or NULL in case of
    ///     parsing error, invalid option or memory error.</returns>
    internal static WebPAnimDecoder WebPAnimDecoderNew(ref WebPData webp_data, ref WebPAnimDecoderOptions dec_options)
    {
        ////ValidatePlatform();

        IntPtr ptr = WebPAnimDecoderNewInternal(ref webp_data, ref dec_options, WEBP_DEMUX_ABI_VERSION);
        WebPAnimDecoder decoder = new WebPAnimDecoder() { decoder = ptr };
        return decoder;

        ////switch (IntPtr.Size)
        ////{
        ////    case 4:
        ////        return WebPAnimDecoderNewInternal_x86(ref webp_data, ref dec_options, WEBP_DEMUX_ABI_VERSION);
        ////    case 8:
        ////        return WebPAnimDecoderNewInternal_x64(ref webp_data, ref dec_options, WEBP_DEMUX_ABI_VERSION);
        ////    default:
        ////        throw new InvalidOperationException("Invalid platform. Can not find proper function");
        ////}
    }
    [LibraryImport("libwebpdemux.dll", EntryPoint = "WebPAnimDecoderNewInternal")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private static partial IntPtr WebPAnimDecoderNewInternal(ref WebPData webp_data, ref WebPAnimDecoderOptions dec_options, int WEBP_DEMUX_ABI_VERSION);



    /// <summary>Get global information about the animation.</summary>
    /// <param name="dec">(in) decoder instance to get information from.</param>
    /// <param name="info">(out) global information fetched from the animation.</param>
    /// <returns>True on success.</returns>
    internal static bool WebPAnimDecoderGetInfo(IntPtr dec, out WebPAnimInfo info)
    {
        return WebPAnimDecoderGetInfoInternal(dec, out info) == 1;
    }
    [LibraryImport("libwebpdemux.dll", EntryPoint = "WebPAnimDecoderGetInfo")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private static partial int WebPAnimDecoderGetInfoInternal(IntPtr dec, out WebPAnimInfo info);



    /// <summary>Check if there are more frames left to decode.</summary>
    /// <param name="dec">(in) decoder instance to be checked.</param>
    /// <returns>
    /// True if 'dec' is not NULL and some frames are yet to be decoded.
    /// Otherwise, returns false.
    /// </returns>
    internal static bool WebPAnimDecoderHasMoreFrames(IntPtr dec)
    {
        return WebPAnimDecoderHasMoreFramesInternal(dec) == 1;
    }
    [LibraryImport("libwebpdemux.dll", EntryPoint = "WebPAnimDecoderHasMoreFrames")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private static partial int WebPAnimDecoderHasMoreFramesInternal(IntPtr dec);



    /// <summary>
    /// Fetch the next frame from 'dec' based on options supplied to
    /// WebPAnimDecoderNew(). This will be a fully reconstructed canvas of size
    /// 'canvas_width * 4 * canvas_height', and not just the frame sub-rectangle. The
    /// returned buffer 'buf' is valid only until the next call to
    /// WebPAnimDecoderGetNext(), WebPAnimDecoderReset() or WebPAnimDecoderDelete().
    /// </summary>
    /// <param name="dec">(in/out) decoder instance from which the next frame is to be fetched.</param>
    /// <param name="buf">(out) decoded frame.</param>
    /// <param name="timestamp">(out) timestamp of the frame in milliseconds.</param>
    /// <returns>
    /// False if any of the arguments are NULL, or if there is a parsing or
    /// decoding error, or if there are no more frames. Otherwise, returns true.
    /// </returns>
    internal static bool WebPAnimDecoderGetNext(IntPtr dec, ref IntPtr buf, ref int timestamp)
    {
        return WebPAnimDecoderGetNextInternal(dec, ref buf, ref timestamp) == 1;
    }
    [LibraryImport("libwebpdemux.dll", EntryPoint = "WebPAnimDecoderGetNext")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private static partial int WebPAnimDecoderGetNextInternal(IntPtr dec, ref IntPtr buf, ref int timestamp);



    /// <summary>
    /// Resets the WebPAnimDecoder object, so that next call to
    /// WebPAnimDecoderGetNext() will restart decoding from 1st frame. This would be
    /// helpful when all frames need to be decoded multiple times (e.g.
    /// info.loop_count times) without destroying and recreating the 'dec' object.
    /// </summary>
    /// <param name="dec">(in/out) decoder instance to be reset</param>
    internal static void WebPAnimDecoderReset(IntPtr dec)
    {
        WebPAnimDecoderResetInternal(dec);
    }
    [LibraryImport("libwebpdemux.dll", EntryPoint = "WebPAnimDecoderReset")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private static partial void WebPAnimDecoderResetInternal(IntPtr dec);



    /// <summary>Deletes the WebPAnimDecoder object.</summary>
    /// <param name="decoder">(in/out) decoder instance to be deleted</param>
    internal static void WebPAnimDecoderDelete(IntPtr decoder)
    {
        WebPAnimDecoderDeleteInternal(decoder);
    }
    [LibraryImport("libwebpdemux.dll", EntryPoint = "WebPAnimDecoderDelete")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private static partial void WebPAnimDecoderDeleteInternal(IntPtr dec);

}

