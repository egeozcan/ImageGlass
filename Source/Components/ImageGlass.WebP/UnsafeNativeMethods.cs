using System.Runtime.InteropServices;
using System.Security;

namespace ImageGlass.WebP;


[SuppressUnmanagedCodeSecurityAttribute]
internal sealed partial class UnsafeNativeMethods
{
    [LibraryImport("kernel32.dll", EntryPoint = "RtlCopyMemory", SetLastError = false)]
    internal static partial void CopyMemory(IntPtr dest, IntPtr src, uint count);

}
