set SAVESTAMP=%DATE:/=-%-%TIME::=_%
set SAVESTAMP=%SAVESTAMP: =%
set SAVESTAMP=%SAVESTAMP:,=.%.xml
mkdir XMLLiveProdResults
nunit-console VerifiOne.dll -xml="VerifiOneLiveProd_%SAVESTAMP%" 
copy VerifiOneLiveProd_%SAVESTAMP% "Y:\AppXML\LIVE\VERIFICATION\9.5 apk Results"
MOVE VerifiOneLiveProd_%SAVESTAMP% "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\XMLLiveProdResults"

nunit-console VerifiTwo.dll -xml="VerifiTwoLiveProd_%SAVESTAMP%"
copy VerifiTwoLiveProd_%SAVESTAMP% "Y:\AppXML\LIVE\VERIFICATION\9.5 apk Results"
MOVE VerifiTwoLiveProd_%SAVESTAMP% "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\XMLLiveProdResults"

nunit-console VerifiThree.dll -xml="VerifiThreeLiveProd_%SAVESTAMP%"
copy VerifiThreeLiveProd_%SAVESTAMP% "Y:\AppXML\LIVE\VERIFICATION\9.5 apk Results"
MOVE VerifiThreeLiveProd_%SAVESTAMP% "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\XMLLiveProdResults"

xcopy "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Screenshot LIVE PROD9.5.0 (310)" "Y:\Media Backup\Mobile Apps\LIVE\VERIFICATION" /e /i
xcopy "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Screenshot LIVE PROD9.5.0 (310)" "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Backup" /e /i
rmdir /s /q "C:\Scripts\Veri 9.5\VodaiOSPRODLocal\VodaiOS\bin\Debug\Screenshot LIVE PROD9.5.0 (310)"