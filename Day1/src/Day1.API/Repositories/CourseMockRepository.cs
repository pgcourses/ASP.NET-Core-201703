using Day1.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day1.API.Repositories
{
    public class CourseMockRepository : ICourseRepository
    {
        public IEnumerable<CourseModel> GetCourses()
        {
            return new List<CourseModel>
            {
                new CourseModel { id=1, Name="Certified Information Systems Security Pro - CISSP", Info="Épp most ért véget" },
                new CourseModel { id=2, Name="Borkollégium mesterkurzus: Tokaj Y Generáció", Info="Épp most ért véget" },
                new CourseModel { id=3, Name="Titkosítási ABC", Info="Épp most ért véget" },
                new CourseModel { id=4, Name="Windows Stack High Availability", Info="Épp most ért véget" },
                new CourseModel { id=5, Name="Xamarin-fejlesztés mobileszközökre", Info="Folyamatban..." },
                new CourseModel { id=6, Name="Vállalkozóiskola", Info="Ma kezdődik (14:00)" },
                new CourseModel { id=7, Name="ASP.NET Core szerveroldali fejlesztés", Info="Ma kezdődik (13:00)" }
            };
        }
    }
}
