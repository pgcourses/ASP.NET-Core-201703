using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FamilyPhotos
{

    /// <summary>
    /// MVC: Model-View-Controller 
    /// 
    /// Model: az adatok előállítása, rögzítése, feldolgozása
    /// View: a felhasználói felület: WebAlkalmazás esetén HTML oldal
    /// Controller: Azért felel, hogy a felhasználói felületről megérkező 
    ///             kérést kiszolgáljuk. Ide érkezik a kérés, és ő vezérli 
    ///             az egyes tevékenységeket mindaddig, amíg a válasz ki 
    ///             nem megy a felhasználóhoz.
    /// 
    /// Állapotok kezelése: a HTTP állapot mentes protokoll
    ///             cookie: adatcsomag, amit a szerver elhelyez a böngészőn
    /// 1.  Session változó használata (és az ebből leágazó megoldások)
    ///     Figyelem: 4.6-ig nem javasolt a használata: http://johnculviner.com/asp-net-concurrent-ajax-requests-and-session-state-blocking/
    ///     Az ASP.NET Core megosztottan kezeli ezeket az adatokat, így a korábbi teljesítményproblémák megszűntek.
    ///        A párhuzamos kérések azonban felülírhatják egymás adatait.
    /// 
    /// 2.  Az átmeneti adatok perzisztens tárolása (pl. SQL szerveren)
    /// 
    /// 
    /// 
    /// </summary>

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseUrls("http://*:1000")
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
