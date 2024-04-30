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
using System.Runtime.InteropServices;
using System.Security;

namespace ImageGlass.WebP;



[SuppressUnmanagedCodeSecurityAttribute]
internal sealed partial class LibWebp
{
    private static readonly int WEBP_DECODER_ABI_VERSION = 0x0208;


    /// <summary>The writer type for output compress data</summary>
    /// <param name="data">Data returned</param>
    /// <param name="data_size">Size of data returned</param>
    /// <param name="wpic">Picture structure</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int WebPMemoryWrite([In()] IntPtr data, uint data_size, ref WebPPicture wpic);
    internal static WebPMemoryWrite? OnCallback;




    /// <summary>This function will initialize the configuration according to a predefined set of parameters (referred to by 'preset') and a given quality factor</summary>
    /// <param name="config">The WebPConfig structure</param>
    /// <param name="preset">Type of image</param>
    /// <param name="quality">Quality of compression</param>
    /// <returns>0 if error</returns>
    internal static int WebPConfigInit(ref WebPConfig config, WebPPreset preset, float quality)
    {
        return WebPConfigInitInternal_x64(ref config, preset, quality, WEBP_DECODER_ABI_VERSION);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigInitInternal")]
    private static extern int WebPConfigInitInternal_x64(ref WebPConfig config, WebPPreset preset, float quality, int WEBP_DECODER_ABI_VERSION);



    /// <summary>Get info of WepP image</summary>
    /// <param name="rawWebP">Bytes[] of WebP image</param>
    /// <param name="data_size">Size of rawWebP</param>
    /// <param name="features">Features of WebP image</param>
    /// <returns>VP8StatusCode</returns>
    internal static VP8StatusCode WebPGetFeatures(IntPtr rawWebP, int data_size, ref WebPBitstreamFeatures features)
    {
        return WebPGetFeaturesInternal_x64(rawWebP, (UIntPtr)data_size, ref features, WEBP_DECODER_ABI_VERSION);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetFeaturesInternal")]
    private static extern VP8StatusCode WebPGetFeaturesInternal_x64([InAttribute()] IntPtr rawWebP, UIntPtr data_size, ref WebPBitstreamFeatures features, int WEBP_DECODER_ABI_VERSION);



    /// <summary>Activate the lossless compression mode with the desired efficiency</summary>
    /// <param name="config">The WebPConfig struct</param>
    /// <param name="level">between 0 (fastest, lowest compression) and 9 (slower, best compression)</param>
    /// <returns>0 in case of parameter error</returns>
    internal static int WebPConfigLosslessPreset(ref WebPConfig config, int level)
    {
        return WebPConfigLosslessPreset_x64(ref config, level);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigLosslessPreset")]
    private static extern int WebPConfigLosslessPreset_x64(ref WebPConfig config, int level);



    /// <summary>Check that configuration is non-NULL and all configuration parameters are within their valid ranges</summary>
    /// <param name="config">The WebPConfig structure</param>
    /// <returns>1 if configuration is OK</returns>
    internal static int WebPValidateConfig(ref WebPConfig config)
    {
        return WebPValidateConfig_x64(ref config);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPValidateConfig")]
    private static extern int WebPValidateConfig_x64(ref WebPConfig config);



    /// <summary>Initialize the WebPPicture structure checking the DLL version</summary>
    /// <param name="wpic">The WebPPicture structure</param>
    /// <returns>1 if not error</returns>
    internal static int WebPPictureInitInternal(ref WebPPicture wpic)
    {
        return WebPPictureInitInternal_x64(ref wpic, WEBP_DECODER_ABI_VERSION);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureInitInternal")]
    private static extern int WebPPictureInitInternal_x64(ref WebPPicture wpic, int WEBP_DECODER_ABI_VERSION);



    /// <summary>Colorspace conversion function to import RGB samples</summary>
    /// <param name="wpic">The WebPPicture structure</param>
    /// <param name="bgr">Point to BGR data</param>
    /// <param name="stride">stride of BGR data</param>
    /// <returns>Returns 0 in case of memory error.</returns>
    internal static int WebPPictureImportBGR(ref WebPPicture wpic, IntPtr bgr, int stride)
    {
        return WebPPictureImportBGR_x64(ref wpic, bgr, stride);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGR")]
    private static extern int WebPPictureImportBGR_x64(ref WebPPicture wpic, IntPtr bgr, int stride);




    /// <summary>Color-space conversion function to import RGB samples</summary>
    /// <param name="wpic">The WebPPicture structure</param>
    /// <param name="bgra">Point to BGRA data</param>
    /// <param name="stride">stride of BGRA data</param>
    /// <returns>Returns 0 in case of memory error.</returns>
    internal static int WebPPictureImportBGRA(ref WebPPicture wpic, IntPtr bgra, int stride)
    {
        return WebPPictureImportBGRA_x64(ref wpic, bgra, stride);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRA")]
    private static extern int WebPPictureImportBGRA_x64(ref WebPPicture wpic, IntPtr bgra, int stride);




    /// <summary>Color-space conversion function to import RGB samples</summary>
    /// <param name="wpic">The WebPPicture structure</param>
    /// <param name="bgr">Point to BGR data</param>
    /// <param name="stride">stride of BGR data</param>
    /// <returns>Returns 0 in case of memory error.</returns>
    internal static int WebPPictureImportBGRX(ref WebPPicture wpic, IntPtr bgr, int stride)
    {
        return WebPPictureImportBGRX_x64(ref wpic, bgr, stride);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRX")]
    private static extern int WebPPictureImportBGRX_x64(ref WebPPicture wpic, IntPtr bgr, int stride);




    /// <summary>Compress to WebP format</summary>
    /// <param name="config">The configuration structure for compression parameters</param>
    /// <param name="picture">'picture' hold the source samples in both YUV(A) or ARGB input</param>
    /// <returns>Returns 0 in case of error, 1 otherwise. In case of error, picture->error_code is updated accordingly.</returns>
    internal static int WebPEncode(ref WebPConfig config, ref WebPPicture picture)
    {
        return WebPEncode_x64(ref config, ref picture);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncode")]
    private static extern int WebPEncode_x64(ref WebPConfig config, ref WebPPicture picture);




    /// <summary>Release the memory allocated by WebPPictureAlloc() or WebPPictureImport*()
    /// Note that this function does _not_ free the memory used by the 'picture' object itself.
    /// Besides memory (which is reclaimed) all other fields of 'picture' are preserved</summary>
    /// <param name="picture">Picture structure</param>
    internal static void WebPPictureFree(ref WebPPicture picture)
    {
        WebPPictureFree_x64(ref picture);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureFree")]
    private static extern void WebPPictureFree_x64(ref WebPPicture wpic);




    /// <summary>Validate the WebP image header and retrieve the image height and width. Pointers *width and *height can be passed NULL if deemed irrelevant</summary>
    /// <param name="data">Pointer to WebP image data</param>
    /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <returns>1 if success, otherwise error code returned in the case of (a) formatting error(s).</returns>
    internal static int WebPGetInfo(IntPtr data, int data_size, out int width, out int height)
    {
        return WebPGetInfo_x64(data, (UIntPtr)data_size, out width, out height);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetInfo")]
    private static extern int WebPGetInfo_x64([InAttribute()] IntPtr data, UIntPtr data_size, out int width, out int height);




    /// <summary>Decode WEBP image pointed to by *data and returns BGR samples into a preallocated buffer</summary>
    /// <param name="data">Pointer to WebP image data</param>
    /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
    /// <param name="output_buffer">Pointer to decoded WebP image</param>
    /// <param name="output_buffer_size">Size of allocated buffer</param>
    /// <param name="output_stride">Specifies the distance between scan lines</param>
    internal static void WebPDecodeBGRInto(IntPtr data, int data_size, IntPtr output_buffer, int output_buffer_size, int output_stride)
    {
        if (WebPDecodeBGRInto_x64(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
            throw new InvalidOperationException("Cannot decode WebP");
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRInto")]
    private static extern IntPtr? WebPDecodeBGRInto_x64([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);




    /// <summary>Decode WEBP image pointed to by *data and returns BGRA samples into a preallocated buffer</summary>
    /// <param name="data">Pointer to WebP image data</param>
    /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
    /// <param name="output_buffer">Pointer to decoded WebP image</param>
    /// <param name="output_buffer_size">Size of allocated buffer</param>
    /// <param name="output_stride">Specifies the distance between scan lines</param>
    internal static void WebPDecodeBGRAInto(IntPtr data, int data_size, IntPtr output_buffer, int output_buffer_size, int output_stride)
    {
        if (WebPDecodeBGRAInto_x64(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
            throw new InvalidOperationException("Can not decode WebP");
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRAInto")]
    private static extern IntPtr? WebPDecodeBGRAInto_x64([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);




    /// <summary>Decode WEBP image pointed to by *data and returns ARGB samples into a preallocated buffer</summary>
    /// <param name="data">Pointer to WebP image data</param>
    /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
    /// <param name="output_buffer">Pointer to decoded WebP image</param>
    /// <param name="output_buffer_size">Size of allocated buffer</param>
    /// <param name="output_stride">Specifies the distance between scan lines</param>
    internal static void WebPDecodeARGBInto(IntPtr data, int data_size, IntPtr output_buffer, int output_buffer_size, int output_stride)
    {
        if (WebPDecodeARGBInto_x64(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
            throw new InvalidOperationException("Can not decode WebP");
    }
    
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeARGBInto")]
    private static extern IntPtr? WebPDecodeARGBInto_x64([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);




    /// <summary>Initialize the configuration as empty. This function must always be called first, unless <see cref="WebPGetFeatures(nint, int, ref WebPBitstreamFeatures)"/> is to be called</summary>
    /// <param name="webPDecoderConfig">Configuration structure</param>
    /// <returns>False in case of mismatched version.</returns>
    internal static int WebPInitDecoderConfig(ref WebPDecoderConfig webPDecoderConfig)
    {
        return WebPInitDecoderConfigInternal_x64(ref webPDecoderConfig, WEBP_DECODER_ABI_VERSION);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPInitDecoderConfigInternal")]
    private static extern int WebPInitDecoderConfigInternal_x64(ref WebPDecoderConfig webPDecoderConfig, int WEBP_DECODER_ABI_VERSION);




    /// <summary>Decodes the full data at once, taking configuration into account</summary>
    /// <param name="data">WebP raw data to decode</param>
    /// <param name="data_size">Size of WebP data </param>
    /// <param name="webPDecoderConfig">Configuration structure</param>
    /// <returns><see cref="VP8StatusCode.VP8_STATUS_OK"/> if the decoding was successful</returns>
    internal static VP8StatusCode WebPDecode(IntPtr data, int data_size, ref WebPDecoderConfig webPDecoderConfig)
    {
        return WebPDecode_x64(data, (UIntPtr)data_size, ref webPDecoderConfig);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecode")]
    private static extern VP8StatusCode WebPDecode_x64(IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);




    /// <summary>Free any memory associated with the buffer. Must always be called last. Doesn't free the 'buffer' structure itself</summary>
    internal static void WebPFreeDecBuffer(ref WebPDecBuffer buffer)
    {
        WebPFreeDecBuffer_x64(ref buffer);
    }
    [LibraryImport("libwebp.dll", EntryPoint = "WebPFreeDecBuffer")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    private static partial void WebPFreeDecBuffer_x64(ref WebPDecBuffer buffer);




    /// <summary>Lossy encoding images</summary>
    /// <param name="bgr">Pointer to BGR image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <param name="stride">Specifies the distance between scanlines</param>
    /// <param name="quality_factor">Ranges from 0 (lower quality) to 100 (highest quality). Controls the loss and quality during compression</param>
    /// <param name="output">output_buffer with WebP image</param>
    /// <returns>Size of WebP Image or 0 if an error occurred</returns>
    internal static int WebPEncodeBGR(IntPtr bgr, int width, int height, int stride, float quality_factor, out IntPtr output)
    {
        return WebPEncodeBGR_x64(bgr, width, height, stride, quality_factor, out output);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGR")]
    private static extern int WebPEncodeBGR_x64([InAttribute()] IntPtr bgr, int width, int height, int stride, float quality_factor, out IntPtr output);




    /// <summary>Lossy encoding images</summary>
    /// <param name="bgr">Pointer to BGRA image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <param name="stride">Specifies the distance between scan lines</param>
    /// <param name="quality_factor">Ranges from 0 (lower quality) to 100 (highest quality). Controls the loss and quality during compression</param>
    /// <param name="output">output_buffer with WebP image</param>
    /// <returns>Size of WebP Image or 0 if an error occurred</returns>
    internal static int WebPEncodeBGRA(IntPtr bgra, int width, int height, int stride, float quality_factor, out IntPtr output)
    {
        return WebPEncodeBGRA_x64(bgra, width, height, stride, quality_factor, out output);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGRA")]
    private static extern int WebPEncodeBGRA_x64([InAttribute()] IntPtr bgra, int width, int height, int stride, float quality_factor, out IntPtr output);




    /// <summary>Lossless encoding images pointed to by *data in WebP format</summary>
    /// <param name="bgr">Pointer to BGR image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <param name="stride">Specifies the distance between scan lines</param>
    /// <param name="output">output_buffer with WebP image</param>
    /// <returns>Size of WebP Image or 0 if an error occurred</returns>
    internal static int WebPEncodeLosslessBGR(IntPtr bgr, int width, int height, int stride, out IntPtr output)
    {
        return WebPEncodeLosslessBGR_x64(bgr, width, height, stride, out output);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGR")]
    private static extern int WebPEncodeLosslessBGR_x64([InAttribute()] IntPtr bgr, int width, int height, int stride, out IntPtr output);




    /// <summary>Lossless encoding images pointed to by *data in WebP format</summary>
    /// <param name="bgra">Pointer to BGRA image data</param>
    /// <param name="width">The range is limited currently from 1 to 16383</param>
    /// <param name="height">The range is limited currently from 1 to 16383</param>
    /// <param name="stride">Specifies the distance between scan lines</param>
    /// <param name="output">output_buffer with WebP image</param>
    /// <returns>Size of WebP Image or 0 if an error occurred</returns>
    internal static int WebPEncodeLosslessBGRA(IntPtr bgra, int width, int height, int stride, out IntPtr output)
    {
        return WebPEncodeLosslessBGRA_x64(bgra, width, height, stride, out output);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGRA")]
    private static extern int WebPEncodeLosslessBGRA_x64([InAttribute()] IntPtr bgra, int width, int height, int stride, out IntPtr output);




    /// <summary>Releases memory returned by the WebPEncode</summary>
    /// <param name="p">Pointer to memory</param>
    internal static void WebPFree(IntPtr p)
    {
        WebPFree_x64(p);
    }
    [LibraryImport("libwebp.dll", EntryPoint = "WebPFree")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    private static partial void WebPFree_x64(IntPtr p);




    /// <summary>Get the WebP version library</summary>
    /// <returns>8bits for each of major/minor/revision packet in integer. E.g: v2.5.7 is 0x020507</returns>
    internal static int WebPGetDecoderVersion()
    {
        return WebPGetDecoderVersion_x64();
    }
    [LibraryImport("libwebp.dll", EntryPoint = "WebPGetDecoderVersion")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    private static partial int WebPGetDecoderVersion_x64();




    /// <summary>Compute PSNR, SSIM or LSIM distortion metric between two pictures</summary>
    /// <param name="srcPicture">Picture to measure</param>
    /// <param name="refPicture">Reference picture</param>
    /// <param name="metric_type">0 = PSNR, 1 = SSIM, 2 = LSIM</param>
    /// <param name="pResult">dB in the Y/U/V/Alpha/All order</param>
    /// <returns>False in case of error (the two pictures don't have same dimension, ...)</returns>
    internal static int WebPPictureDistortion(ref WebPPicture srcPicture, ref WebPPicture refPicture, int metric_type, IntPtr pResult)
    {
        return WebPPictureDistortion_x64(ref srcPicture, ref refPicture, metric_type, pResult);
    }
    [DllImport("libwebp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureDistortion")]
    private static extern int WebPPictureDistortion_x64(ref WebPPicture srcPicture, ref WebPPicture refPicture, int metric_type, IntPtr pResult);




    internal static IntPtr WebPMalloc(int size)
    {
        return WebPMalloc_x64(size);
    }
    [LibraryImport("libwebp.dll", EntryPoint = "WebPMalloc")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    private static partial IntPtr WebPMalloc_x64(int size);

}


