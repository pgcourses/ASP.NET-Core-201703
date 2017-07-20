# Név és jelszó alapú azonosítás (Authentikáció: a felhasználó beazonosítása)

Felhasználó --> Név     --> ASP.NET Core alkalmazás (Identity) --> Hash algoritmus --> Név         --> EntityFramework --> DB
                Jelszó                                             (Egyirányú)         Jelszó hash 

Hash algoritmus azt garantálja, hogy
Azonos bemeneti adathoz azonos kimeneti hasht ad
Jól szórja a kimenetet, vagyis a bemenet kis változása a végeredményt nagyon megváltoztatja
A hash algoritmus végeredményébõl nem lehet a bemenetet visszafejteni

# Csoport alapú jogosultságkezelés
## Készítsünk egy vázlatot a csoportok kezelésére
## Készítsünk egy vázlatot arra, hogy a felhasználóinkat csoporthoz rendeljük.

Authorizáció: az azonosított felhasználó jogainak ellenõrzése egy adott erõforrás használatához

Egy új szint:

1. Felhasználó (név+jelszó)
2. Felhasználó->csoportok tagja lehet
3. Egy _policy_ menet közben megmondja, hogy az adott felhasználóra milyen szabályok vonatkoznak
   a policy hivatkozhat csoporttagságra IS.
4. Az erõforrást policy-vel védjük

# WebHook feldolgozás
## Github webhook fogadása

1. ngrok indítása: 

ngrok http 59167 -host-header="localhost:59167"



