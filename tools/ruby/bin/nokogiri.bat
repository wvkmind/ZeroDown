@ECHO OFF
IF NOT "%~f0" == "~f0" GOTO :WinNT
@"D:\env\ruby-2.2.6-x64-mingw32\bin\ruby.exe" "D:/env/ruby-2.2.6-x64-mingw32/bin/nokogiri" %1 %2 %3 %4 %5 %6 %7 %8 %9
GOTO :EOF
:WinNT
@"D:\env\ruby-2.2.6-x64-mingw32\bin\ruby.exe" "%~dpn0" %*
