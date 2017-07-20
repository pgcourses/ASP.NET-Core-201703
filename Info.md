# N�v �s jelsz� alap� azonos�t�s (Authentik�ci�: a felhaszn�l� beazonos�t�sa)

Felhaszn�l� --> N�v     --> ASP.NET Core alkalmaz�s (Identity) --> Hash algoritmus --> N�v         --> EntityFramework --> DB
                Jelsz�                                             (Egyir�ny�)         Jelsz� hash 

Hash algoritmus azt garant�lja, hogy
Azonos bemeneti adathoz azonos kimeneti hasht ad
J�l sz�rja a kimenetet, vagyis a bemenet kis v�ltoz�sa a v�geredm�nyt nagyon megv�ltoztatja
A hash algoritmus v�geredm�ny�b�l nem lehet a bemenetet visszafejteni

# Csoport alap� jogosults�gkezel�s
## K�sz�ts�nk egy v�zlatot a csoportok kezel�s�re
## K�sz�ts�nk egy v�zlatot arra, hogy a felhaszn�l�inkat csoporthoz rendelj�k.

Authoriz�ci�: az azonos�tott felhaszn�l� jogainak ellen�rz�se egy adott er�forr�s haszn�lat�hoz

Egy �j szint:

1. Felhaszn�l� (n�v+jelsz�)
2. Felhaszn�l�->csoportok tagja lehet
3. Egy _policy_ menet k�zben megmondja, hogy az adott felhaszn�l�ra milyen szab�lyok vonatkoznak
   a policy hivatkozhat csoporttags�gra IS.
4. Az er�forr�st policy-vel v�dj�k

# WebHook feldolgoz�s
## Github webhook fogad�sa

1. ngrok ind�t�sa: 

ngrok http 59167 -host-header="localhost:59167"



