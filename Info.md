# Név és jelszó alapú azonosítás (Authentikáció: a felhasználó beazonosítása)

Felhasználó --> Név     --> ASP.NET Core alkalmazás (Identity) --> Hash algoritmus --> Név         --> EntityFramework --> DB
                Jelszó                                             (Egyirányú)         Jelszó hash 

Hash algoritmus azt garantálja, hogy
Azonos bemeneti adathoz azonos kimeneti hasht ad
Jól szórja a kimenetet, vagyis a bemenet kis változása a végeredményt nagyon megváltoztatja
A hash algoritmus végeredményébõl nem lehet a bemenetet visszafejteni

# Csoport alapú jogosultságkezelés

A bejelentkezett felhasználó jogait a szerver a saját titkos kulcsával titkosítja, és beállít a végeredménnyel egy authentikációs
cookie-t. Ezt a cookie-t a böngészõ minden kérés mellé elküldi, a szerver visszaalakítja, így kiderül, hogy a felhasználó milyen 
jogosultságcsoportban van.


## Készítsünk egy vázlatot a csoportok kezelésére
## Készítsünk egy vázlatot arra, hogy a felhasználóinkat csoporthoz rendeljük.

Authorizáció: az azonosított felhasználó jogainak ellenõrzése egy adott erõforrás használatához

Egy új szint:

1. Felhasználó (név+jelszó)
2. Felhasználó->csoportok tagja lehet
3. Egy _policy_ menet közben megmondja, hogy az adott felhasználóra milyen szabályok vonatkoznak
   a policy hivatkozhat csoporttagságra IS.
4. Az erõforrást policy-vel védjük

# A WebAPI védelme: authentikáció és Authorizáció
A böngészõ/html cookie megoldást is lehet szimulálni, de elég mazerás, és a világ ehelyett Bearer token-t 
használja különbözõ formában. A korábbi ASP.NET-ben (5-ös verzióig) néhány kapcsolóval ez bekapcsolható volt.
A ASP.NET Core-ban viszont egy külsõ megoldást kell integrálni:
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/community

Amit ezen a tanfolyamon megnézünk 
https://github.com/aspnet-contrib/AspNet.Security.OpenIdConnect.Server

## Authentikáció




              +------------------+                                   +--------------------+
              | Kliens           |   Bejelentkezés: név+jelszó       |                    |
              |------------------|  +------------------------------> |--------------------|
              |                  |                                   |                    |
              |                  |     sikeres bejelentkezés: token  |                    |
              |                  |  <-------------------------------+|                    |
              |                  |                                   |                    |
              +------------------+    Minden kérésre + token         +--------------------+
                                    +------------------------------->


## lépésk
### Token Request Validation
A kérés valódiságát ellenõrzi, és csak akkor engedi tovább a folyamatot, ha a kérést rendben van (valid)
### Handle Token Request
A kérés tartalmi feldolgozása: név+jelszó ellenõrzése, claim identity->ticket->token legyártása

Ha ez jól lefut, akkor a kérésünkre kapott válasz (pl:)

{
    "scope": "openid",
    "token_type": "Bearer",
    "access_token": "CfDJ8ITYHYiWz...",
    "expires_in": 86400,
    "id_token": "eyJhbGciOiJub..."
}

Az access tokenre van szükségünk a továbbiakhoz.

## Authorizáció

A korábbiakhoz hasonlóan, a webapi kontrollereken is Authorize attributumokkal szeretnénk
a jogosultságokat beállítani.

