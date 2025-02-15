
@echo off


echo:
echo *********************************************************************
echo * ImageGlass Code Signing tool V9
echo * https://imageglass.org
echo *
echo *
echo * https://www.ssl.com/how-to/using-your-code-signing-certificate
echo *********************************************************************
echo:
echo:


set PATH=..\..\Source\ImageGlass\bin\Publish\net8.0-windows-x64

:: Executable files
set FILES[0]=ImageGlass.exe
set FILES[1]=igcmd.exe

:: Library files
set FILES[2]=ImageGlass.dll
set FILES[3]=igcmd.dll

set FILES[4]=ImageGlass.Base.dll
set FILES[5]=ImageGlass.Gallery.dll
set FILES[6]=ImageGlass.Settings.dll
set FILES[7]=ImageGlass.Tools.dll
set FILES[8]=ImageGlass.UI.dll
set FILES[9]=ImageGlass.Viewer.dll
set FILES[10]=ImageGlass.WebP.dll
set FILES[11]=ImageGlass.WinTouch.dll

set FILES[12]=D2Phap.DXControl.dll
set FILES[13]=DirectNStandard.dll
set FILES[14]=D2Phap.EggShell.dll
set FILES[15]=FileWatcherEx.dll

set FILES[16]=libsharpyuv.dll
set FILES[17]=libwebp.dll
set FILES[18]=libwebpdecoder.dll
set FILES[19]=libwebpdemux.dll
set FILES[20]=PhotoSauce.MagicScaler.dll
set FILES[21]=WicNet.dll
set FILES[22]=ZString.dll



::set TOOL="signtool.exe"
set TOOL="C:\Program Files (x86)\Windows Kits\10\bin\10.0.26100.0\x64\signtool.exe"
set x=0


:SymLoop
if defined FILES[%x%] (
    call echo _________________________________________________________________________________________
    call echo [%x%]. %%FILES[%x%]%%
    call echo:
    call %TOOL% sign /fd sha256 /tr http://ts.ssl.com /td sha256 /n "Duong Dieu Phap" /a %PATH%\%%FILES[%x%]%%
    call %TOOL% verify /pa %PATH%\%%FILES[%x%]%%
    call echo:
    call echo:
    call echo:

    set /a "x+=1"
    GOTO :SymLoop
)

echo:
echo:
pause
