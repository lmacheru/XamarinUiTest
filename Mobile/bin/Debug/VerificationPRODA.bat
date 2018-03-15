set SAVESTAMP=%DATE:/=-%-%TIME::=_%
set SAVESTAMP=%SAVESTAMP: =%
set SAVESTAMP=%SAVESTAMP:,=.%.xml
mkdir Backup
nunit-console VerifiOne.dll -xml="VerifiOnePRODA_%SAVESTAMP%" 
copy VerifiOnePRODA_%SAVESTAMP% "Y:\AppXML\PRODA\VERIFICATION\9.5 apk Results"
MOVE VerifiOnePRODA_%SAVESTAMP% "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Backup"

nunit-console VerifiTwo.dll -xml="VerifiTwoPRODA_%SAVESTAMP%"
copy VerifiTwoPRODA_%SAVESTAMP% "Y:\AppXML\PRODA\VERIFICATION\9.5 apk Results"
MOVE VerifiTwoPRODA_%SAVESTAMP% "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Backup"

nunit-console VerifiThree.dll -xml="VerifiThreePRODA_%SAVESTAMP%"
copy VerifiThreePRODA_%SAVESTAMP% "Y:\AppXML\PRODA\VERIFICATION\9.5 apk Results"
MOVE VerifiThreePRODA_%SAVESTAMP% "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Backup"

xcopy "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Screenshot PRODA External9.5.0 (310)" "Y:\Media Backup\Mobile Apps\PRODA\VERIFICATION" /e /i
xcopy "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Screenshot PRODA External9.5.0 (310)" "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Backup" /e /i
rmdir /s /q "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Screenshot PRODA External9.5.0 (310)\"
