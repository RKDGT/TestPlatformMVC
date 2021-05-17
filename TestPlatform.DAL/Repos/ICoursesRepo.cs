using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlatform.DAL.Entities;

namespace TestPlatform.DAL.Repos
{
    public interface ICoursesRepo
    {
        IQueryable<Courses> GetCourses();
        void Create(Courses item);
    }
}
