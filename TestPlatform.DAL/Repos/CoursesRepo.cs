using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlatform.DAL.Entities;

namespace TestPlatform.DAL.Repos
{
    public class CoursesRepo : ICoursesRepo
    {
        public TestPlatformDBContext _context { get; set; }

        public CoursesRepo(
            TestPlatformDBContext context
            )
        {
            _context = context;
        }

        public IQueryable<Courses> GetCourses() 
        {
            var courses = _context.Courses.Select(res => res);
            return courses;
        }
        public void Create(Courses item)
        {
            _context.Courses.Add(item);
            _context.SaveChanges();
        }
    }
}
