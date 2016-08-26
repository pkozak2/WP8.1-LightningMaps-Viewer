LightningMaps Viewer
========================

W Repozytorium istniej¹ dwa projekty:
Blitzortung Viewer UWP - Dla systemu Windows 10 Mobile
Blitzortung Viewer - Dla systemu Windows Phone 8.1

Kod dla systemu Windows 10 Mobile siê nie kompiluje?
--------------

W klatalogu Blitzortung Viewer UWP/Utils/ nale¿y utworzyæ now¹ klasê Keys.cs o poni¿szej strukturze:

>public static string MapServiceToken = "*BingMapServiceToken*";
>public static string ws_server = "*WebocketServer*";
>
>public static string historyRegion1 = "*API URL*";
>public static string historyRegion2 = "*API URL*";
>public static string historyRegion3 = "*API URL*";
>public static string historyRegion4 = "*API URL*";
>public static string historyRegion5 = "*API URL*";
>public static string historyRegion6 = "*API URL*";
>
>public static string message = "*Message to Server (JSON)*";