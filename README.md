LightningMaps Viewer
========================

W Repozytorium istnieją dwa projekty:
Blitzortung Viewer UWP - Dla systemu Windows 10 Mobile
Blitzortung Viewer - Dla systemu Windows Phone 8.1

Kod dla systemu Windows 10 Mobile się nie kompiluje?
--------------

W katalogu **Blitzortung Viewer UWP/Utils/** należy utworzyć nową klasę **Keys.cs** o poniższej strukturze:

>public static string MapServiceToken = "*BingMapServiceToken*";
>
>public static string ws_server = "*WebocketServer*";
>
>public static string historyRegion1 = "*API URL*";
>
>public static string historyRegion2 = "*API URL*";
>
>public static string historyRegion3 = "*API URL*";
>
>public static string historyRegion4 = "*API URL*";
>
>public static string historyRegion5 = "*API URL*";
>
>public static string historyRegion6 = "*API URL*";
>
>public static string message = "*Message to Server (JSON)*";


*Dane wrażliwe zostały usunięte, gdyż nie mogę ich udostępnić publicznie.*