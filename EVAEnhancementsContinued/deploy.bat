

set H=R:\KSP_1.1.4_dev
echo %H%

set d=%H%
if exist %d% goto one
mkdir %d%
:one
set d=%H%\Gamedata
if exist %d% goto two
mkdir %d%
:two
set d=%H%\Gamedata\EVAEnhancementsContinued
if exist %d% goto three
mkdir %d%
:three
set d=%H%\Gamedata\EVAEnhancementsContinued\Plugins
if exist %d% goto four
mkdir %d%
:four
set d=%H%\Gamedata\EVAEnhancementsContinued\PluginData
mkdir %d%
:five

copy bin\Debug\EVAEnhancementsContinued.dll ..\GameData\EVAEnhancementsContinued\Plugins


xcopy /y /s "..\GameData\EVAEnhancementsContinued" "%H%\GameData\EVAEnhancementsContinued"
