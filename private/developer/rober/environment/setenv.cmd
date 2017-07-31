@echo off
REM SetEnv.cmd for rober
set path=%path%;c:\bin\;%devdir%\scripts
set path=%path%;C:\Program Files\IIS\Microsoft Web Deploy V3
Set _NT_SYMBOL_PATH = symsrv*symsrv.dll*C:\symbols*http://msdl.microsoft.com/download/symbols
prompt [$P\]$S
pushd "%product%"
git status
pushd "%sln%

