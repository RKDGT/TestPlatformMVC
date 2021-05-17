using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPlatform.BLL.Services;
using TestPlatform.BLL.DTO;
using TestPlatfom.BLL.Logic;
using TestPlatfom.BLL.DTO;

namespace TestPlatformMVC.Controllers
{
    public class SuperAdminController : Controller
    {
        private readonly ISuperAdminLogic _logic;

        public SuperAdminController(
            ISuperAdminLogic logic
            )
        {
            _logic = logic;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users(List<UserModel> users)
        {
            if (users.Count == 0)
            {
                users = await _logic.GetUsers();
            }
            return View(users);
        }
        public IActionResult Courses()
        {
            List<CoursesModel> coursesModels = _logic.GetCourses();
            return View(coursesModels);
        }
        public IActionResult CreateCourses()
        {
            ViewData["Lectors"] = _logic.GetUsersByRole("Lector").Result.Select(res => res.UserName).ToList();
            return View(new CoursesModel());
        }
        public IActionResult CreateNewCourse(CoursesModel coursesModel)
        {
            _logic.CreateCourses(coursesModel);
            return RedirectToAction("CreateCourses");
        }
        public IActionResult Up()
        {
            return View();
        }
        
        public async Task<IActionResult> RegisterUser(SignUpAdminModel singUpAdminModel)
        {
            await _logic.Up(singUpAdminModel);
            return RedirectToAction("Up");
        }

        [HttpPost]
        public IActionResult UpdateUsers(List<UserModel> singUpAdminModel)
        {
            _logic.UpdateUsers(singUpAdminModel);
            return RedirectToAction("Users", singUpAdminModel);
        }

        public IActionResult Course()
        {
            var courseName = Request.Query.Keys.FirstOrDefault();
            var course = _logic.GetCourse(courseName);
            
            return View(course);
        }
        

        public IActionResult CreateTestForCourse()
        {
            return View();
        }
        

        [HttpPost]
        public IActionResult PostTestForCourse(PostTestForCourse test)
        {
            _logic.PostTest(test);
            return Json(string.Empty);
        }


        [HttpPost]
        public IActionResult PostTheoryForCourse(TheoryModel theory)
        {
            theory.Files = HttpContext.Request.Form.Files;
            return Json(_logic.PostTheoryForCourse(theory));
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile()
        {
            DownloadFiles file = await _logic.DownLoadFileOfCourse(Request.Query.Keys.FirstOrDefault());
            return File(file.mem, file.myme, file.path);
        }


    }
}
