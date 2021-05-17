using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestPlatfom.BLL.DTO;
using TestPlatform.BLL.DTO;
using TestPlatform.DAL.Identity;

namespace TestPlatfom.BLL.Logic
{
    public interface ISuperAdminLogic
    {
        Task<List<UserModel>> GetUsers();
        void UpdateUsers(List<UserModel> userModels);
        Task Up(SignUpAdminModel signUp);
        Task<IList<User>> GetUsersByRole(string role);
        void CreateCourses(CoursesModel coursesModel);
        List<CoursesModel> GetCourses();
        CourseModel GetCourse(string course);
        bool PostTheoryForCourse(TheoryModel theoryModel);
        Task<DownloadFiles> DownLoadFileOfCourse(string pathOfDownloads);
        void PostTest(PostTestForCourse postTestForCourse);
    }
}
