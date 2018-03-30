@echo off
cd bin/Release
set /P apikey=Enter apikey: 
set /P nupkgfilename=Enter .nupgk filename (Use Tab to browse files): 
@echo on
dotnet nuget push %nupkgfilename% -k %apikey% -s https://api.nuget.org/v3/index.json
@echo off
set /p DUMMY=Hit ENTER to continue...