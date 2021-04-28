
# BankApp feladat 

Egy bank mukodeset szimulaltam, szamlak nyitasa, szamlak kozotti tranzakcio, kezpenz ki/be fizetes, 2 lepeses azonositas, stb. Ha uj regisztracio torenik akkor lenne megerosito email, de most ez a lehetoseg ki van kapcsolva. A 2f autentikacioval mobil app-al tudunk bejelentkezni. Google vagy Microsoft App-ot erdemes hasznalni, mindketto van Androidra es IOS-re. Tovabba uj regisztracional letre jon ket szamla, normal es megtakaritasi. Az egyszeruseg kedveert HUF penznemet hasznaltam.

## Telepites
**SqlLocalDb**-t hasznaltam a fejleszteshez innen letoltheto: [download from here](https://download.microsoft.com/download/7/c/1/7c14e92e-bdcb-4f89-b7cf-93543e7112d1/SqlLocalDB.msi)

Van egy Connection string-unk, ami igy alakul:
>      "ConnectionStrings": { "DefaultConnection": "Server=(localdb)\\HVGDb;Database=HvgBank;Trusted_Connection=True;MultipleActiveResultSets=true" }


Ertelemszeruen ha van a gepeden, MSSQL, vagy MSSQLLocalDB az is tokeletes a celra. 
Ha **nem** telepited a SqlLocalDb akkor termeszetesen **nem** kell megkrealni a HvgDb-t.
>SqlLocalDb create "HvgDb"

>SqlLocalDb start "HvgDb"

MSSQL eseten igy alakul a connection:
>      "ConnectionStrings": { "DefaultConnection": "Server=.;Database=HvgBank;Trusted_Connection=True;MultipleActiveResultSets=true" }

*dotnet run* utan a migraciok lefutnak es lesz egy elo adatbazisunk.



***
**Build majd futtatas** (BankSystem.Web project)
>dotnet build

>dotnet run

Maga a BankSystem.Web egy konzol app, *dotnet run* utan a https://localhost:5001/ vagy a http://localhost:5000/ cimen tudjuk elerni.
Termeszetesen Visual Studioban is tudod futtatni ha kivalasztod a BanksSytem.Web-et majd a zold play gombnal is a BankSystem.Web-et kell kivalasztani.

## Teszt Account
Van egy Nagy Zoltan nevezetu urgenk, akinek a felhasznalo neve
| Felhasznalo nev                 | Jelszo 
|-----------------	|----------
| test@hvg.com         | Admin1!

*Belepes utan erdemes 2db szamlat csinalni es azon tesztelni*



**Hasznalt technologiak:**
C#, ASP.NET Core, ASP.NET Core MVC, ASP.NET Core Web API, Entity Framework Core, AJAX, HTML, CSS, Bootstrap
