using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day1.API.Repositories;

namespace Day1.API.Controllers
{
    [Route("api/courses")] //itt is meg tudjuk a route címet adni
    //[Route("api/[controller]")] //Így pedig a controller nevét használja a "Controller postfix nélkül"
    public class CourseDataController : Controller
    {
        private readonly ICourseRepository _repository;

        public CourseDataController(ICourseRepository courseRepository)
        {
            if (courseRepository == null)
            { throw new ArgumentNullException(nameof(courseRepository)); }

            _repository = courseRepository;
        }

        //Egy http kérés a következők egyike:
        //GET/POST/PUT/PATCH/DELETE stb.

        //[HttpGet("api/courses")]
        //[HttpGet] //ha a controlleren megadtuk az elérést, akkor itt már csak a http methodot kell megadni.
        public IActionResult GetCourses()
        {
            //Ez egy rossz megoldás, hiszen a környezetünket saját magunk állítjuk elő
            //ezzel a függőségünket ide láncoljuk. Függünk a CourseMockRepository-tól
            //var repo = new CourseMockRepository();
            //return new JsonResult(repo.GetCourses());

            //Ehelyett jobb megoldás, ha a függőséget kívülről kapjuk: DependencyInjection
            return new JsonResult(_repository.GetCourses());

        }

        //[HttpPost]
        //public IActionResult AddCourse(/*CourseInputModel model*/)
        //{
        //    return new JsonResult(new { Name = "Ez a postba jött" });
        //}


        //Ha több azonos metódust akarunk címezni routinggal, akkor mindegyik action-re kell attributum
        //[HttpGet("api/courses/{id}")]
        //public IActionResult GetCourse(int id)
        //{
        //    return new JsonResult(new { Name = $"Ez a get-be érkezett: {id}" });
        //}

    }
}
