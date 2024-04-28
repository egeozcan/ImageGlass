# Building libwebp as a Windows native library (.dll)

1. Get the latest [source code release from libwebp repo](https://github.com/webmproject/libwebp/releases).
2. Extract the archive to a new folder.
3. Launch `x64 Native Tools Command Prompt` which is come from C++ BuildTools (Get it from ["Tools for Visual Studio"](https://visualstudio.microsoft.com/downloads/)).
4. Navigate to the extracted folder `libwebp-main`:
  ```batch
  cd /d "F:\path\to\libwebp-main"
  ```
5. Start compiling the source with `nmake`:
  ```batch
  nmake /f Makefile.vc CFG=release-dynamic RTLIBCFG=dynamic   OBJDIR=output
  ```
6. Copy the generated DLL files in `\output\release-dynamic\x64\bin` to `\Source\Libs\libwebp\x64` folder.
7. Build ImageGlass solution.



## Notes
- To build x86 DLL, use `x86 Native Tools Command Prompt` instead.

