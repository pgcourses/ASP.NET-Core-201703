using Day1.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day1.API.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _repository;

        public CourseController(ICourseRepository repository)
        {
            if (repository == null)
            { throw new ArgumentNullException(nameof(repository)); }

            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetCourses());
        }
    }
}
