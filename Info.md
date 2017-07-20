# N�v �s jelsz� alap� azonos�t�s (Authentik�ci�: a felhaszn�l� beazonos�t�sa)

Felhaszn�l� --> N�v     --> ASP.NET Core alkalmaz�s (Identity) --> Hash algoritmus --> N�v         --> EntityFramework --> DB
                Jelsz�                                             (Egyir�ny�)         Jelsz� hash 

Hash algoritmus azt garant�lja, hogy
Azonos bemeneti adathoz azonos kimeneti hasht ad
J�l sz�rja a kimenetet, vagyis a bemenet kis v�ltoz�sa a v�geredm�nyt nagyon megv�ltoztatja
A hash algoritmus v�geredm�ny�b�l nem lehet a bemenetet visszafejteni

# Csoport alap� jogosults�gkezel�s

A bejelentkezett felhaszn�l� jogait a szerver a saj�t titkos kulcs�val titkos�tja, �s be�ll�t a v�geredm�nnyel egy authentik�ci�s
cookie-t. Ezt a cookie-t a b�ng�sz� minden k�r�s mell� elk�ldi, a szerver visszaalak�tja, �gy kider�l, hogy a felhaszn�l� milyen 
jogosults�gcsoportban van.


## K�sz�ts�nk egy v�zlatot a csoportok kezel�s�re
## K�sz�ts�nk egy v�zlatot arra, hogy a felhaszn�l�inkat csoporthoz rendelj�k.

Authoriz�ci�: az azonos�tott felhaszn�l� jogainak ellen�rz�se egy adott er�forr�s haszn�lat�hoz

Egy �j szint:

1. Felhaszn�l� (n�v+jelsz�)
2. Felhaszn�l�->csoportok tagja lehet
3. Egy _policy_ menet k�zben megmondja, hogy az adott felhaszn�l�ra milyen szab�lyok vonatkoznak
   a policy hivatkozhat csoporttags�gra IS.
4. Az er�forr�st policy-vel v�dj�k

# A WebAPI v�delme: authentik�ci� �s Authoriz�ci�
A b�ng�sz�/html cookie megold�st is lehet szimul�lni, de el�g mazer�s, �s a vil�g ehelyett Bearer token-t 
haszn�lja k�l�nb�z� form�ban. A kor�bbi ASP.NET-ben (5-�s verzi�ig) n�h�ny kapcsol�val ez bekapcsolhat� volt.
A ASP.NET Core-ban viszont egy k�ls� megold�st kell integr�lni:
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/community

Amit ezen a tanfolyamon megn�z�nk 
https://github.com/aspnet-contrib/AspNet.Security.OpenIdConnect.Server

## Authentik�ci�




              +------------------+                                   +--------------------+
              | Kliens           |   Bejelentkez�s: n�v+jelsz�       |                    |
              |------------------|  +------------------------------> |--------------------|
              |                  |                                   |                    |
              |                  |     sikeres bejelentkez�s: token  |                    |
              |                  |  <-------------------------------+|                    |
              |                  |                                   |                    |
              +------------------+    Minden k�r�sre + token         +--------------------+
                                    +------------------------------->


## l�p�sk
### Token Request Validation
A k�r�s val�dis�g�t ellen�rzi, �s csak akkor engedi tov�bb a folyamatot, ha a k�r�st rendben van (valid)
### Handle Token Request
A k�r�s tartalmi feldolgoz�sa: n�v+jelsz� ellen�rz�se, claim identity->ticket->token legy�rt�sa

Ha ez j�l lefut, akkor a k�r�s�nkre kapott v�lasz (pl:)

{
    "scope": "openid",
    "token_type": "Bearer",
    "access_token": "CfDJ8ITYHYiWz...",
    "expires_in": 86400,
	"refresh_token": "CfDJ8ITYHYiW......"
    "id_token": "eyJhbGciOiJub..."
}

Ahhoz, hogy a bejelentkezett felhaszn�l� k�r�s�t a szerver fogadni �s feldolgozni tudja, az access_token-t haszn�ljuk

M�ghozz, minden k�r�sre kell tenni egy Authorization header v�ltoz�t, aminek az �rt�ke:

Bearer CfDJ8ITYHYiWzedEmvGkmA-S....

Teh�t, a Bearer sz�veg, sz�k�z, majd az access_token �rt�ke. 

Ha lej�rt a token�nk �rv�nyess�ge, akkor pedig a 
grant_type=refresh_token �s refresh_token=CfDJ8ITYHYiW......

param�terekkel tudunk k�rni �j access tokent, teh�t an�lk�l, hogy nevet �s jelsz�t k�ne �jra k�ldeni.

Az access tokenre van sz�ks�g�nk a tov�bbiakhoz.
Ebben a tokenben szerepeltethetj�k a csoporttags�got is, ekkor a policy-ben tudunk ilyet is k�rdezni.

## Authoriz�ci�

A kor�bbiakhoz hasonl�an, a webapi kontrollereken is Authorize attributumokkal szeretn�nk
a jogosults�gokat be�ll�tani.

Mivel bele�rtuk a tokenbe a role-okat, �gy erre k�sz�nk policy-t