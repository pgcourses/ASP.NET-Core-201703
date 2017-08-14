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

# Docker
https://www.netacademia.hu/Tanfolyam/dockerbemutato-ingyenes-docker-bemutato
https://www.netacademia.hu/Tanfolyam/docker-docker

Megoldja a "saj�t g�pemen m�k�dik" probl�m�j�t.
T�rol� (container): szabv�nyos�tott csomagol�s: az alkalmaz�s �llom�nyai, k�rnyezeti b�ll�t�sai �s a f�gg�s�geinek a szabv�nyos�tott csomagja.
R�teg (Layer): az egyes t�rol�k r�tegenk�nt egym�sra �p�lnek, a legfels�t tudjuk m�dos�tani, minden m�s r�teg csak olvashat�.
Image: a buildelt docker container.
Nyilv�ntart�s (Registry): a Docker k�zponti nyilv�ntart�sa az el�rhet� images-kr�l

0. Windows al� telep�thet� innen:
https://store.docker.com/editions/community/docker-ce-desktop-windows

Alkalmaz�s f�gg�s�g: MSSQL szerver
Szerencs�re a Microsoft kiadta m�r az SQL-t linux al� is, �gy egyszer� Linux cont�nerben el�rhet�:

ha nem adunk nevet az SQL kont�nernek, akkor a docker ad neki minden l�trehoz�skor m�s �s m�s nevet.
Mivel a k�l�nb�z� kont�nerekb�l a n�vvel hivatkozunk egy m�sik kont�nerre, ez csak nehez�ten� az �let�nket.
(docker run -e ACCEPT_EULA=Y -e SA_PASSWORD=B1zt0ns�g -p 1433:1433 -d microsoft/mssql-server-linux)

Ez�rt n�vvel elnevezz�k:
1. docker run -e ACCEPT_EULA=Y -e SA_PASSWORD=B1zt0ns�g -p 1433:1433 --name sqlonlinuxondocker -d microsoft/mssql-server-linux

2. a dotnet publish-hoz sz�ks�g van a kliens oldali eszk�z�kere (pl. bower), ez�rt a konzolban ezt kell futtatni:

set Path=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio 14.0\Web\External;

majd a konzolban:

dotnet publish -o publish

3. A docker csomag k�sz�t�s�hez sz�ks�g van egy le�r�ra, ez minden esetben a Dockerfile (nagybet�vel, kiterjeszt�s n�lk�l.)

Ez most �gy n�z ki:

FROM microsoft/dotnet:1.0-runtime
WORKDIR /src/FamilyPhotosWithIdentity
COPY publish .
ENTRYPOINT ["dotnet", "FamilyPhotosWithIdentity.dll"]

Ez ut�n a konzolban elind�thatjuk a docker buildet:

docker build -t familyphotoswithidentity .

Figyelem, a pont kell a sor v�g�re!!!

4. Miel�tt az alkalmaz�sunkat futtatn�nk, ki kell adni parancssorb�l a 

dotnet ef database update 

parancsot az adatb�zis gener�l�s�hoz. Azonban ez a parancs a appsettings.json-b�l veszi a param�tereit, �gy oda az 
adatb�zis el�r�s�t ideiglenesen �t kell �rni erre:

"DefaultConnection": "Server=localhost;Database=FamilyPhotosWithIdentityDB;User Id=sa;Password=B1zt0ns�g;MultipleActiveResultSets=true"

hiszen az SQL szervert tartalmaz� docker container a localhoston szolg�ltat a windwos g�pen amin vagyunk.

Azonban, ha kont�nerb�l futtatjuk, akkor viszont a szerver neve nem localhost (ennek nincs �lrtelme a docker kont�neren BEL�L, hanem
az SQL kont�nern ek a neve: sqlonlinuxondocker

5. Ha van adatb�zisunk, akkor tudjuk futtatni a m�sik (alkalmaz�s) kont�ner�nket, �sszelinkelve az SQL szerverrel:

docker run -it -p 1000:1000 --link sqlonlinuxondocker familyphotoswithidentity

FIGYELEM:

A docker file-ban m�g meg kell oldani az adatb�zis automatikus l�trehoz�s�t, ehhez 
biztosan dotnet-sdk kell, mert abban van dotnet ef parancs.

Illetve, futtatni kell linuxon a dotnet ef-et, ezt val�sz�n�leg bash script-b�l kell.

https://waterlan.home.xs4all.nl/dos2unix.html#DOS2UNIX


#https://gist.github.com/remarkablemark/aacf14c29b3f01d6900d13137b21db3a#file-dockerfile

# replace shell with bash so we can source files
RUN rm /bin/sh && ln -s /bin/bash /bin/sh

# update the repository sources list
# and install dependencies
RUN apt-get update \
    && apt-get install -y curl \
    && apt-get -y autoclean

# nvm environment variables
ENV NVM_DIR /usr/local/nvm
ENV NODE_VERSION 4.4.7

# install nvm
# https://github.com/creationix/nvm#install-script
RUN curl --silent -o- https://raw.githubusercontent.com/creationix/nvm/v0.31.2/install.sh | bash

# install node and npm
RUN source $NVM_DIR/nvm.sh \
    && nvm install $NODE_VERSION \
    && nvm alias default $NODE_VERSION \
    && nvm use default

# add node and npm to path so the commands are available
ENV NODE_PATH $NVM_DIR/v$NODE_VERSION/lib/node_modules
ENV PATH $NVM_DIR/versions/node/v$NODE_VERSION/bin:$PATH

#https://github.com/aspnet/aspnet-docker/blob/master/1.0/jessie/sdk/Dockerfile


https://hahoangv.wordpress.com/2016/08/05/docker-for-net-developers-net-core-ef-core-and-postresql-in-docker/
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            }
