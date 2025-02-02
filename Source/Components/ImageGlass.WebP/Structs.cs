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

namespace ImageGlass.WebP;


/// <summary>Features gathered from the bit stream</summary>
[StructLayoutAttribute(LayoutKind.Sequential)]
internal struct WebPBitstreamFeatures
{
    /// <summary>Width in pixels, as read from the bit stream</summary>
    public int Width;
    /// <summary>Height in pixels, as read from the bit stream</summary>
    public int Height;
    /// <summary>True if the bit stream contains an alpha channel</summary>
    public int Has_alpha;
    /// <summary>True if the bit stream is an animation</summary>
    public int Has_animation;
    /// <summary>0 = undefined (/mixed), 1 = lossy, 2 = lossless</summary>
    public int Format;
    /// <summary>Padding for later use</summary>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad;
};


/// <summary>Compression parameters</summary>
[StructLayoutAttribute(LayoutKind.Sequential)]
internal struct WebPConfig
{
    /// <summary>Lossless encoding (0=lossy(default), 1=lossless)</summary>
    public int lossless;
    /// <summary>Between 0 (smallest file) and 100 (biggest)</summary>
    public float quality;
    /// <summary>Quality/speed trade-off (0=fast, 6=slower-better)</summary>
    public int method;
    /// <summary>Hint for image type (lossless only for now)</summary>
    public WebPImageHint image_hint;
    /// <summary>If non-zero, set the desired target size in bytes. Takes precedence over the 'compression' parameter</summary>
    public int target_size;
    /// <summary>If non-zero, specifies the minimal distortion to try to achieve. Takes precedence over target_size</summary>
    public float target_PSNR;
    /// <summary>Maximum number of segments to use, in [1..4]</summary>
    public int segments;
    /// <summary>Spatial Noise Shaping. 0=off, 100=maximum</summary>
    public int sns_strength;
    /// <summary>Range: [0 = off .. 100 = strongest]</summary>
    public int filter_strength;
    /// <summary>Range: [0 = off .. 7 = least sharp]</summary>
    public int filter_sharpness;
    /// <summary>Filtering type: 0 = simple, 1 = strong (only used if filter_strength > 0 or auto-filter > 0)</summary>
    public int filter_type;
    /// <summary>Auto adjust filter's strength [0 = off, 1 = on]</summary>
    public int autofilter;
    /// <summary>Algorithm for encoding the alpha plane (0 = none, 1 = compressed with WebP lossless). Default is 1</summary>
    public int alpha_compression;
    /// <summary>Predictive filtering method for alpha plane. 0: none, 1: fast, 2: best. Default if 1</summary>
    public int alpha_filtering;
    /// <summary>Between 0 (smallest size) and 100 (lossless). Default is 100</summary>
    public int alpha_quality;
    /// <summary>Number of entropy-analysis passes (in [1..10])</summary>
    public int pass;
    /// <summary>If true, export the compressed picture back. In-loop filtering is not applied</summary>
    public int show_compressed;
    /// <summary>Preprocessing filter (0=none, 1=segment-smooth, 2=pseudo-random dithering)</summary>
    public int preprocessing;
    /// <summary>Log2(number of token partitions) in [0..3] Default is set to 0 for easier progressive decoding</summary>
    public int partitions;
    /// <summary>Quality degradation allowed to fit the 512k limit on prediction modes coding (0: no degradation, 100: maximum possible degradation)</summary>
    public int partition_limit;
    /// <summary>If true, compression parameters will be remapped to better match the expected output size from JPEG compression. Generally, the output size will be similar but the degradation will be lower</summary>
    public int emulate_jpeg_size;
    /// <summary>If non-zero, try and use multi-threaded encoding</summary>
    public int thread_level;
    /// <summary>If set, reduce memory usage (but increase CPU use)</summary>
    public int low_memory;
    /// <summary>Near lossless encoding [0 = max loss .. 100 = off (default)]</summary>
    public int near_lossless;
    /// <summary>If non-zero, preserve the exact RGB values under transparent area. Otherwise, discard this invisible RGB information for better compression. The default value is 0</summary>
    public int exact;
    /// <summary>Reserved for future lossless feature</summary>
    public int delta_palettization;
    /// <summary>If needed, use sharp (and slow) RGB->YUV conversion</summary>
    public int use_sharp_yuv;
    /// <summary>Padding for later use</summary>
    private readonly int pad1;
    private readonly int pad2;
};


/// <summary>Main exchange structure (input samples, output bytes, statistics)</summary>
[StructLayoutAttribute(LayoutKind.Sequential)]
internal struct WebPPicture
{
    /// <summary>Main flag for encoder selecting between ARGB or YUV input. Recommended to use ARGB input (*argb, argb_stride) for lossless, and YUV input (*y, *u, *v, etc.) for lossy</summary>
    public int use_argb;
    /// <summary>Color-space: should be YUV420 for now (=Y'CbCr). Value = 0</summary>
    public UInt32 colorspace;
    /// <summary>Width of picture (less or equal to WEBP_MAX_DIMENSION)</summary>
    public int width;
    /// <summary>Height of picture (less or equal to WEBP_MAX_DIMENSION)</summary>
    public int height;
    /// <summary>Pointer to luma plane</summary>
    public IntPtr y;
    /// <summary>Pointer to chroma U plane</summary>
    public IntPtr u;
    /// <summary>Pointer to chroma V plane</summary>
    public IntPtr v;
    /// <summary>Luma stride</summary>
    public int y_stride;
    /// <summary>Chroma stride</summary>
    public int uv_stride;
    /// <summary>Pointer to the alpha plane</summary>
    public IntPtr a;
    /// <summary>stride of the alpha plane</summary>
    public int a_stride;
    /// <summary>Padding for later use</summary>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad1;
    /// <summary>Pointer to ARGB (32 bit) plane</summary>
    public IntPtr argb;
    /// <summary>This is stride in pixels units, not bytes</summary>
    public int argb_stride;
    /// <summary>Padding for later use</summary>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad2;
    /// <summary>Byte-emission hook, to store compressed bytes as they are ready</summary>
    public IntPtr writer;
    /// <summary>Can be used by the writer</summary>
    public IntPtr custom_ptr;
    // map for extra information (only for lossy compression mode)
    /// <summary>1: intra type, 2: segment, 3: quant, 4: intra-16 prediction mode, 5: chroma prediction mode, 6: bit cost, 7: distortion</summary>
    public int extra_info_type;
    /// <summary>If not NULL, points to an array of size ((width + 15) / 16) * ((height + 15) / 16) that will be filled with a macroblock map, depending on extra_info_type</summary>
    public IntPtr extra_info;
    /// <summary>Pointer to side statistics (updated only if not NULL)</summary>
    public IntPtr stats;
    /// <summary>Error code for the latest error encountered during encoding</summary>
    public UInt32 error_code;
    /// <summary>If not NULL, report progress during encoding</summary>
    public IntPtr progress_hook;
    /// <summary>This field is free to be set to any value and used during callbacks (like progress-report e.g.)</summary>
    public IntPtr user_data;
    /// <summary>Padding for later use</summary>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 13, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad3;
    /// <summary>Row chunk of memory for YUVA planes</summary>
    private readonly IntPtr memory_;
    /// <summary>Row chunk of memory for ARGB planes</summary>
    private readonly IntPtr memory_argb_;
    /// <summary>Padding for later use</summary>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad4;
};


/// <summary>Structure for storing auxiliary statistics (mostly for lossy encoding)</summary>
[StructLayoutAttribute(LayoutKind.Sequential)]
internal struct WebPAuxStats
{
    /// <summary>Final size</summary>
    public int coded_size;
    /// <summary>Peak-signal-to-noise ratio for Y</summary>
    public float PSNRY;
    /// <summary>Peak-signal-to-noise ratio for U</summary>
    public float PSNRU;
    /// <summary>Peak-signal-to-noise ratio for V</summary>
    public float PSNRV;
    /// <summary>Peak-signal-to-noise ratio for All</summary>
    public float PSNRALL;
    /// <summary>Peak-signal-to-noise ratio for Alpha</summary>
    public float PSNRAlpha;
    /// <summary>Number of intra4</summary>
    public int block_count_intra4;
    /// <summary>Number of intra16</summary>
    public int block_count_intra16;
    /// <summary>Number of skipped macro-blocks</summary>
    public int block_count_skipped;
    /// <summary>Approximate number of bytes spent for header</summary>
    public int header_bytes;
    /// <summary>Approximate number of bytes spent for  mode-partition #0</summary>
    public int mode_partition_0;
    /// <summary>Approximate number of bytes spent for DC coefficients for segment 0</summary>
    public int residual_bytes_DC_segments0;
    /// <summary>Approximate number of bytes spent for AC coefficients for segment 0</summary>
    public int residual_bytes_AC_segments0;
    /// <summary>Approximate number of bytes spent for UV coefficients for segment 0</summary>
    public int residual_bytes_uv_segments0;
    /// <summary>Approximate number of bytes spent for DC coefficients for segment 1</summary>
    public int residual_bytes_DC_segments1;
    /// <summary>Approximate number of bytes spent for AC coefficients for segment 1</summary>
    public int residual_bytes_AC_segments1;
    /// <summary>Approximate number of bytes spent for UV coefficients for segment 1</summary>
    public int residual_bytes_uv_segments1;
    /// <summary>Approximate number of bytes spent for DC coefficients for segment 2</summary>
    public int residual_bytes_DC_segments2;
    /// <summary>Approximate number of bytes spent for AC coefficients for segment 2</summary>
    public int residual_bytes_AC_segments2;
    /// <summary>Approximate number of bytes spent for UV coefficients for segment 2</summary>
    public int residual_bytes_uv_segments2;
    /// <summary>Approximate number of bytes spent for DC coefficients for segment 3</summary>
    public int residual_bytes_DC_segments3;
    /// <summary>Approximate number of bytes spent for AC coefficients for segment 3</summary>
    public int residual_bytes_AC_segments3;
    /// <summary>Approximate number of bytes spent for UV coefficients for segment 3</summary>
    public int residual_bytes_uv_segments3;
    /// <summary>Number of macro-blocks in segments 0</summary>
    public int segment_size_segments0;
    /// <summary>Number of macro-blocks in segments 1</summary>
    public int segment_size_segments1;
    /// <summary>Number of macro-blocks in segments 2</summary>
    public int segment_size_segments2;
    /// <summary>Number of macro-blocks in segments 3</summary>
    public int segment_size_segments3;
    /// <summary>Quantizer values for segment 0</summary>
    public int segment_quant_segments0;
    /// <summary>Quantizer values for segment 1</summary>
    public int segment_quant_segments1;
    /// <summary>Quantizer values for segment 2</summary>
    public int segment_quant_segments2;
    /// <summary>Quantizer values for segment 3</summary>
    public int segment_quant_segments3;
    /// <summary>Filtering strength for segment 0 [0..63]</summary>
    public int segment_level_segments0;
    /// <summary>Filtering strength for segment 1 [0..63]</summary>
    public int segment_level_segments1;
    /// <summary>Filtering strength for segment 2 [0..63]</summary>
    public int segment_level_segments2;
    /// <summary>Filtering strength for segment 3 [0..63]</summary>
    public int segment_level_segments3;
    /// <summary>Size of the transparency data</summary>
    public int alpha_data_size;
    /// <summary>Size of the enhancement layer data</summary>
    public int layer_data_size;

    // lossless encoder statistics
    /// <summary>bit0:predictor bit1:cross-color transform bit2:subtract-green bit3:color indexing</summary>
    public Int32 lossless_features;
    /// <summary>Number of precision bits of histogram</summary>
    public int histogram_bits;
    /// <summary>Precision bits for transform</summary>
    public int transform_bits;
    /// <summary>Number of bits for color cache lookup</summary>
    public int cache_bits;
    /// <summary>Number of color in palette, if used</summary>
    public int palette_size;
    /// <summary>Final lossless size</summary>
    public int lossless_size;
    /// <summary>Lossless header (transform, Huffman, etc) size</summary>
    public int lossless_hdr_size;
    /// <summary>Lossless image data size</summary>
    public int lossless_data_size;
    /// <summary>Padding for later use</summary>
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad;
};


[StructLayoutAttribute(LayoutKind.Sequential)]
internal struct WebPDecoderConfig
{
    /// <summary>Immutable bit stream features (optional)</summary>
    public WebPBitstreamFeatures input;
    /// <summary>Output buffer (can point to external memory)</summary>
    public WebPDecBuffer output;
    /// <summary>Decoding options</summary>
    public WebPDecoderOptions options;
}


/// <summary>Output buffer</summary>
[StructLayoutAttribute(LayoutKind.Sequential)]
internal struct WebPDecBuffer
{
    /// <summary>Color space</summary>
    public WEBP_CSP_MODE colorspace;
    /// <summary>Width of image</summary>
    public int width;
    /// <summary>Height of image</summary>
    public int height;
    /// <summary>If non-zero, 'internal_memory' pointer is not used. If value is '2' or more, the external memory is considered 'slow' and multiple read/write will be avoided</summary>
    public int is_external_memory;
    /// <summary>Output buffer parameters</summary>
    public RGBA_YUVA_Buffer u;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad1;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad2;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad3;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad4;
    /// <summary>Internally allocated memory (only when is_external_memory is 0). Should not be used externally, but accessed via WebPRGBABuffer</summary>
    public IntPtr private_memory;
}


/// <summary>Union of buffer parameters</summary>
[StructLayoutAttribute(LayoutKind.Explicit)]
internal struct RGBA_YUVA_Buffer
{
    [FieldOffsetAttribute(0)]
    public WebPRGBABuffer RGBA;

    [FieldOffsetAttribute(0)]
    public WebPYUVABuffer YUVA;
}


[StructLayoutAttribute(LayoutKind.Sequential)]
internal struct WebPYUVABuffer
{
    /// <summary>Pointer to luma samples</summary>
    public IntPtr y;
    /// <summary>Pointer to chroma U samples</summary>
    public IntPtr u;
    /// <summary>Pointer to chroma V samples</summary>
    public IntPtr v;
    /// <summary>Pointer to alpha samples</summary>
    public IntPtr a;
    /// <summary>Luma stride</summary>
    public int y_stride;
    /// <summary>Chroma U stride</summary>
    public int u_stride;
    /// <summary>Chroma V stride</summary>
    public int v_stride;
    /// <summary>Alpha stride</summary>
    public int a_stride;
    /// <summary>Luma plane size</summary>
    public UIntPtr y_size;
    /// <summary>Chroma plane U size</summary>
    public UIntPtr u_size;
    /// <summary>Chroma plane V size</summary>
    public UIntPtr v_size;
    /// <summary>Alpha plane size</summary>
    public UIntPtr a_size;
}


/// <summary>Generic structure for describing the output sample buffer</summary>
[StructLayoutAttribute(LayoutKind.Sequential)]
internal struct WebPRGBABuffer
{
    /// <summary>Pointer to RGBA samples</summary>
    public IntPtr rgba;
    /// <summary>Stride in bytes from one scanline to the next</summary>
    public int stride;
    /// <summary>Total size of the RGBA buffer</summary>
    public UIntPtr size;
}


/// <summary>Decoding options</summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPDecoderOptions
{
    /// <summary>If true, skip the in-loop filtering</summary>
    public int bypass_filtering;
    /// <summary>If true, use faster point-wise up-sampler</summary>
    public int no_fancy_upsampling;
    /// <summary>If true, cropping is applied _first_</summary>
    public int use_cropping;
    /// <summary>Left position for cropping. Will be snapped to even values</summary>
    public int crop_left;
    /// <summary>Top position for cropping. Will be snapped to even values</summary>
    public int crop_top;
    /// <summary>Width of the cropping area</summary>
    public int crop_width;
    /// <summary>Height of the cropping area</summary>
    public int crop_height;
    /// <summary>If true, scaling is applied _afterward_</summary>
    public int use_scaling;
    /// <summary>Final width</summary>
    public int scaled_width;
    /// <summary>Final height</summary>
    public int scaled_height;
    /// <summary>If true, use multi-threaded decoding</summary>
    public int use_threads;
    /// <summary>Dithering strength (0=Off, 100=full)</summary>
    public int dithering_strength;
    /// <summary>Flip output vertically</summary>
    public int flip;
    /// <summary>Alpha dithering strength in [0..100]</summary>
    public int alpha_dithering_strength;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad1;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad2;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad3;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad4;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad5;
};


/// <summary>Anim decoder options (from WebPAnimDecoder API)</summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPAnimDecoderOptions
{
    /// <summary>Output colorspace. Only the following modes are supported:
    /// MODE_RGBA, MODE_BGRA, MODE_rgbA and MODE_bgrA.</summary>
    public WEBP_CSP_MODE color_mode;
    /// <summary>If true, use multi-threaded decoding</summary>
    public int use_threads;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad1;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad2;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad3;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad4;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad5;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad6;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad7;
};


/// <summary>
/// Data type used to describe 'raw' data, e.g., chunk data
/// (ICC profile, metadata) and WebP compressed image data.
/// 'bytes' memory must be allocated using WebPMalloc() and such.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPData
{
    public IntPtr data;
    public UIntPtr size;
}


/// <summary>Main opaque object.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPAnimDecoder
{
    public IntPtr decoder;
}


/// <summary>Global information about the animation</summary>
[StructLayout(LayoutKind.Sequential)]
public struct WebPAnimInfo
{
    public UInt32 canvas_width;
    public UInt32 canvas_height;
    public UInt32 loop_count;
    public UInt32 bgcolor;
    public UInt32 frame_count;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad1;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad2;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad3;
    /// <summary>Padding for later use</summary>
    private readonly UInt32 pad4;
}
