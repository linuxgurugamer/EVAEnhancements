set DEFHOMEDRIVE=d:
set DEFHOMEDIR=%DEFHOMEDRIVE%%HOMEPATH%
set HOMEDIR=
set HOMEDRIVE=%CD:~0,2%

set RELEASEDIR=d:\Users\jbb\release
set ZIP="c:\Program Files\7-zip\7z.exe"
echo Default homedir: %DEFHOMEDIR%
if "%HOMEDIR%" == "" (
set HOMEDIR=%DEFHOMEDIR%
)
echo %HOMEDIR%


copy bin\Release\EVAEnhancementsContinued.dll ..\GameData\EVAEnhancementsContinued\Plugins

copy ..\EVAEnhancementsContinued.version ..\GameData\EVAEnhancementsContinued
copy ..\..\MiniAVC.dll ..\GameData\EVAEnhancementsContinued

mkdir %HOMEDIR%\install\GameData\EVAEnhancementsContinued
mkdir %HOMEDIR%\install\GameData\EVAEnhancementsContinued\Plugins
mkdir %HOMEDIR%\install\GameData\EVAEnhancementsContinued\PluginData


xcopy /y /s "..\GameData\EVAEnhancementsContinued" %HOMEDIR%\install\GameData\EVAEnhancementsContinued

type ..\EVAEnhancementsContinued.version
set /p VERSION= "Enter version: "

%HOMEDRIVE%
cd %HOMEDIR%\install

set FILE="%RELEASEDIR%\EVAEnhancementsContinued-%VERSION%.zip"
IF EXIST %FILE% del /F %FILE%
%ZIP% a -tzip %FILE% Gamedata\EVAEnhancementsContinued
