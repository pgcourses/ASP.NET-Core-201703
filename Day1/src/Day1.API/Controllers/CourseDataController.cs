using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Day1.API.Controllers
{
    //[Route("api/courses")] //itt is meg tudjuk a route címet adni
    [Route("api/[controller]")]
    public class CourseDataController : Controller
    {
        //Egy http kérés a következők egyike:
        //GET/POST/PUT/PATCH/DELETE stb.

        //[HttpGet("api/courses")]
        [HttpGet] //ha a controlleren megadtuk az elérést, akkor itt már csak a http methodot kell megadni.
        public IActionResult GetCourses()
        {
            return new JsonResult(new List<object>
            {
                new { id=1, Name="Certified Information Systems Security Pro - CISSP", Info="Épp most ért véget" },
                new { id=2, Name="Borkollégium mesterkurzus: Tokaj Y Generáció", Info="Épp most ért véget" },
                new { id=3, Name="Titkosítási ABC", Info="Épp most ért véget" },
                new { id=4, Name="Windows Stack High Availability", Info="Épp most ért véget" },
                new { id=5, Name="Xamarin-fejlesztés mobileszközökre", Info="Folyamatban..." },
                new { id=6, Name="Vállalkozóiskola", Info="Ma kezdődik (14:00)" },
                new { id=7, Name="ASP.NET Core szerveroldali fejlesztés", Info="Ma kezdődik (13:00)" }
            });
        }
    }
}
