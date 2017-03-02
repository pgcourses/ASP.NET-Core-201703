using Day1.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day1.API.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<CourseModel> GetCourses();
    }
}
