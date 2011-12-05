@ECHO off
set current=%~dp0
for /f %%a in ('dir /b /s packages.config') do "%current%tools\nuget\nuget.exe" install "%%a" -o "%~dp0lib\packages"