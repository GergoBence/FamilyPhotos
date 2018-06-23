using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FamilyPhotos
{
    /// <summary>
    ///     MVC: Model-View-Controller
    ///
    ///    Model: adatok előállítása rögzítése, feldolgozása
    ///    View: a fejlhasználói felület: most a html
    ///    COntroller: a felhasználói felületről megérkező kérést kiszolgáljuk.
    ///            Ide érkezik a kérés és ő vezérli az egyes tevékenységeket mindaddig
    ///            amíg a válasz ki nem megy a felhasználóhoz
    ///
    ///    állapot kezelése: a HTTP állapot mentes protokoll
    ///    Cookie: adatcsomag amit a szerver elhelyez a böngészőn
    ///     1. Session változó használata
    ///      
    ///     2. Az átmeneti adatok perzisztens tárolása pl sql szerveren
    /// 
    /// -----------------------------------------------------------------------------------------
    /// 
    /// I. Modellek szerepe az MVC alkalmazásban:
    /// 
    /// 1. Megjelenítés során (Szerver ---> böngésző)
    ///     
    ///    -A modell-be összegyűjtjük azokat az információkat, amiket meg szeretnénk jeleníteni. Ezt végzi a kontroller.
    ///    -Az összegyűjtött adatok a modell-be zűrva kerülnek a nézet generátorhoz, amelyik generélja a html-t, közben
    ///     felhasználja a model adatokat.
    ///     
    /// 2. Az adatbevitel során (Böngésző---> Szerver)
    /// 
    ///    -A böngésző információt küld a szerver felé.
    ///         -query string
    ///         -header
    ///         -HTML Form
    ///     Egységes ezekben hogy név/érték párokban van szervezve az információ
    ///     
    ///    -A szerver megkapja ezt az információt
    ///    -A ModelBinder 
    ///       -pédányosít a controller/action paraméternek megefelő típusokat
    ///       -Megpróbálja összerendelni a várakozásainkat a kapott kulcs/érték halmaz egyes elemeivel
    ///         -primitív típusok esetén a paraméter neve
    ///         -DTO (Data transfer Object) esetén a property-k neve az összerendelés alapja
    ///     
    ///       -adatbevitel validációja: ezt  ModelBinder elvégzi a megadott validationAttribute-ok alapján,
    ///        az eredményt a ControllerBase.ModelState osztály tartalmazza
    ///        
    ///    - Ha a modellünk érvénytelen , akkor visszaadjuk browsernek az új oldalt és a hibainformációkat
    ///     
    ///    - Ha érvényes akkor a modellt feldolgozzuk (elmentjük, módosítjuk amit kell, stb.)
    ///     
    /// rendes: 
    ///    megjelenítés <---- (ViewModel) <---- (transzformáció(robotmunka)) <---- (model) <----(repository) <----(adattárolás)
    ///    transzformáció neve automapper
    /// 
    ///     automapper üzembehelyezéséhez kell:
    ///         -profile, a konstruktorban van a transzformációs definíció
    ///         -ezt be kell tölteni
    /// </summary>




    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseUrls("http://*:1000")
                .UseStartup<Startup>();
    }
}
