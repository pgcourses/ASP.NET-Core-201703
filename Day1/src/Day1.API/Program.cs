using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

/// <summary>
/// .NET Core
/// 
/// - ASP.NET hozzá van kötve az IIS-hez.
/// - Az IIS hozzá van kötve a windows-hoz
/// - Ezért, az új ASP.NET = ASP.NET Core kell, hogy többféle webkiszolgálón képes legyen futni.
/// 
/// - ASP.NET Core célja: multiplatformos, jól paraméterezhető webes alkalmazásokhoz fejlesztő környezet
/// - Beérkező kéréseket feldolgozzuk, előállítsuk a választ, és kiküldjük a kérőhöz a végeredményt.
///   (Request pipeline)
///   
/// - Beépített HTTP szerver: Kestrel
/// 
/// - MVC: Model-View-Controller
/// 
/// 
/// </summary>

namespace Day1.API
{
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
