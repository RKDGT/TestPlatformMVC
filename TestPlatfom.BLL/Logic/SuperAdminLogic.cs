using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestPlatfom.BLL.DTO;
using TestPlatform.BLL.DTO;
using TestPlatform.DAL.Entities;
using TestPlatform.DAL.Enums;
using TestPlatform.DAL.Identity;
using TestPlatform.DAL.Repos;

namespace TestPlatfom.BLL.Logic
{
    public class SuperAdminLogic : ISuperAdminLogic
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly ICoursesRepo _coursesRepo;
        private readonly IGenericRepository<CourseInfo> _coursesInfoRepo;
        private readonly IGenericRepository<Theory> _coursesTheory;
        private readonly IGenericRepository<TheoryFiles> _coursesTheoryFiles;
        private readonly IGenericRepository<Tests> _tests;
        private readonly IGenericRepository<Questions> _question;
        private readonly IGenericRepository<Answers> _answers;
        private readonly IGenericRepository<QuestionsAnswers> _questionAnswers;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnviroment;

        [Obsolete]
        public SuperAdminLogic(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IGenericRepository<User> userRepo,
            IGenericRepository<CourseInfo> coursesInfoRepo,
            ICoursesRepo coursesRepo,
            IHostingEnvironment hostingEnviroment,
            IGenericRepository<Theory> coursesTheory,
            IGenericRepository<TheoryFiles> coursesTheoryFiles,
            IGenericRepository<Tests> tests,
            IGenericRepository<Questions> question,
            IGenericRepository<Answers> answers,
            IGenericRepository<QuestionsAnswers> questionAnswers
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _coursesRepo = coursesRepo;
            _hostingEnviroment = hostingEnviroment;
            _coursesInfoRepo = coursesInfoRepo;
            _coursesTheory = coursesTheory;
            _coursesTheoryFiles = coursesTheoryFiles;
            _tests = tests;
            _question = question;
            _answers = answers;
            _questionAnswers = questionAnswers;
        }
        public async Task Out()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<bool> In(SignInModel signIn)
        {
            var res = await _signInManager.PasswordSignInAsync(signIn.Login, signIn.Password, signIn.Remember, false);
            return res.Succeeded;
        }
        public async Task<List<UserModel>> GetUsers()
        {
            List<UserModel> usersFormated = new List<UserModel>();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                usersFormated.Add(new UserModel
                {
                    Name = user.Name,
                    SurName = user.SurName,
                    Fullname = $"{user.Name} {user.SurName}",
                    UserName = user.UserName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                });
            }
            return usersFormated;
        }
        public async Task<IList<User>> GetUsersByRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync("Lector");
            return users;
        }
        public List<CoursesModel> GetCourses()
        {
            List<CoursesModel> coursesFormated = new List<CoursesModel>();
            var courses = _coursesRepo.GetCourses().ToList();
            foreach (var course in courses)
            {
                var additionalInfo = _coursesInfoRepo.Get().Where(res => res.Id == course.Id).Select(res => new { res.Description, res.MainCourseImgPath }).FirstOrDefault();
                coursesFormated.Add(new CoursesModel
                {
                    Name = course.Name,
                    Description = additionalInfo.Description,
                    ImgPath = additionalInfo.MainCourseImgPath,
                    Owner = _userManager.FindByIdAsync(course.OwnerId).Result.UserName,
                    IsActive = course.IsActive,
                    IsPublic = course.IsPublic,
                    Start = course.DateOfStart,
                    Finish = course.DateOfFinish
                });
            }
            return coursesFormated;
        }
        public void UpdateUsers(List<UserModel> userModels)
        {
            userModels.ForEach(user =>
            {
                var userToChange = _userManager.FindByEmailAsync(user.Email).Result;
                var oldrole = _userManager.GetRolesAsync(userToChange).Result;
                userToChange.Name = user.Name;
                userToChange.SurName = user.SurName;
                userToChange.UserName = user.UserName;
                userToChange.Email = user.Email;
                userToChange.IsActive = user.IsActive;
                _userManager.UpdateAsync(userToChange).Wait();
                _userManager.RemoveFromRoleAsync(userToChange, oldrole.FirstOrDefault()).Wait();
                _userManager.AddToRoleAsync(userToChange, user.Role).Wait();
            });

        }

        [Obsolete]
        public void CreateCourses(CoursesModel coursesModel)
        {
            var ownerId = _userManager.FindByNameAsync(coursesModel.Owner).Result;

            string uniqueFileName = null;
            if (coursesModel.Img != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnviroment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + coursesModel.Img.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                coursesModel.Img.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            if (coursesModel.Name != null && ownerId.Id != null)
            {
                _coursesInfoRepo.Create(new CourseInfo
                {
                    Description = coursesModel.Description,
                    MainCourseImgPath = uniqueFileName
                });
                _coursesRepo.Create(new Courses
                {
                    Name = coursesModel.Name,
                    OwnerId = ownerId.Id,
                    IsPublic = coursesModel.IsPublic,
                    IsActive = coursesModel.IsActive,
                    DateOfFinish = coursesModel.Start,
                    DateOfStart = coursesModel.Finish,
                    CourseInfoId = _coursesInfoRepo.Get().Count()
                });
            }
        }
        public async Task Up(SignUpAdminModel signUp)
        {
            var addNewUser = new User
            {
                Name = signUp.Name,
                SurName = signUp.SurName,
                Email = signUp.Email,
                UserName = signUp.Login,
                EmailConfirmed = true,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(addNewUser, signUp.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(addNewUser, signUp.Role);
                await _signInManager.SignInAsync(addNewUser, isPersistent: false);
            }
        }

        public CourseModel GetCourse(string course)
        {
            course = Regex.Replace(course, Regex.Escape("%20"), " ");
            var courseInfo = _coursesRepo.GetCourses().Where(res => res.Name == course).FirstOrDefault();
            var detailForCourse = _coursesInfoRepo.Get().Where(res => res.Id == courseInfo.CourseInfoId).FirstOrDefault();
            var theoryForCourse = _coursesTheory.Get().Where(res => res.CourseInfoId == courseInfo.CourseInfoId).ToList();
            CourseModel theories = new CourseModel
            {
                NameOfCourse = course,
                DescriptionOfCourse = detailForCourse.Description,
                AdditionalInform = new List<TheoryModel>()
            };

            foreach (var item in theoryForCourse)
            {
                var filesForCourse = _coursesTheoryFiles.Get().Where(res => res.TheoryId == item.Id).Select(res => res.filePath).ToList();
                theories.AdditionalInform.Add(new TheoryModel { Topic = item.Topic, Description = item.Details, FileNames = filesForCourse, Id = item.Id.ToString() });
            }


            return theories;
        }

        private string CreateLinkFromCustom(string entering)
        {
            MatchCollection links = Regex.Matches(entering, Regex.Escape("[--") + "(.*?)" + Regex.Escape("--]"));
            foreach (Match link in links)
            {
                var toReplace = link.Groups[1].Value.Split("|");
                entering = entering.Replace(link.Groups[0].Value, $"<a href='{toReplace[0]}'>{toReplace[1]}</a>");
            }
            return entering;
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt","text/plain"},
                {".pdf","application/pdf"},
                {".zip","application/zip"},
                {".rar","application/x-rar-compressed"},
                {".doc","application/vnd.ms-word"},
                {".docx","application/vnd.ms-word"},
                {".xls","application/vnd.ms-excel"},
                {".xlsx","application/vnd.openxmlformats-officedocument,spreadsheetml.sheet"},
                {".png","image/png"},
                {".jpg","image/jpeg"},
                {".jpeg","image/jpeg"},
                {".gif","image/gif"},
                {".csv","text/csv"},
                {".srt","text/plain"}
            };
        }

        [Obsolete]
        public async Task<DownloadFiles> DownLoadFileOfCourse(string pathOfDownloads)
        {
            pathOfDownloads = Path.Combine(_hostingEnviroment.WebRootPath, Path.Combine("theorys", Regex.Replace(Regex.Replace(pathOfDownloads, Regex.Escape("%20"), " "), Regex.Escape("?"), "\\")));

            var memory = new MemoryStream();
            using (var stream = new FileStream(pathOfDownloads, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return new DownloadFiles { mem = memory, myme = GetMimeTypes()[Path.GetExtension(pathOfDownloads).ToLowerInvariant()], path = Path.GetFileName(pathOfDownloads) };
        }

        [Obsolete]
        public bool PostTheoryForCourse(TheoryModel theoryModel)
        {
            if (theoryModel.NemeOfCourse != null && theoryModel.Topic != null)
            {
                theoryModel.NemeOfCourse = Regex.Replace(theoryModel.NemeOfCourse, Regex.Escape("%20"), " ");
                theoryModel.Topic = CreateLinkFromCustom(theoryModel.Topic);
                if (theoryModel.Description != null)
                {
                    theoryModel.Description = CreateLinkFromCustom(theoryModel.Description);
                }

                var currentCourseInfoId = _coursesRepo.GetCourses().Where(res => res.Name == theoryModel.NemeOfCourse).Select(res => res.CourseInfoId).FirstOrDefault();
                _coursesTheory.Create(new Theory
                {
                    Topic = theoryModel.Topic,
                    Details = theoryModel.Description,
                    CourseInfoId = currentCourseInfoId
                });

                var idOfTheory = _coursesTheory.Get().Where(res => res.CourseInfoId == currentCourseInfoId && res.Topic == theoryModel.Topic && res.Details == theoryModel.Description).Select(res => res.Id).FirstOrDefault();


                var wwwroot = Path.Combine(Path.Combine(Path.Combine(_hostingEnviroment.WebRootPath, "theorys"), theoryModel.NemeOfCourse), theoryModel.Topic);
                if (!Directory.Exists(wwwroot))
                {
                    Directory.CreateDirectory(wwwroot);
                }
                foreach (var item in theoryModel.Files)
                {
                    if (item != null)
                    {
                        Task task = new Task(() =>
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                            string filePath = Path.Combine(wwwroot, uniqueFileName);
                            item.CopyTo(new FileStream(filePath, FileMode.Create));
                            _coursesTheoryFiles.Create(new TheoryFiles
                            {
                                filePath = uniqueFileName,
                                TheoryId = idOfTheory
                            });
                        });
                        task.Start();
                    }
                }
                return true;
            }
            return false;
        }

        public void PostTest(PostTestForCourse postTestForCourse)
        {
            var mainInfo = postTestForCourse.AdditionalInfo;
            if (postTestForCourse.Topic != null)
            {
                _tests.Create(new Tests
                {
                    Topic = postTestForCourse.Topic,
                    Description = postTestForCourse.Description,
                    TheoriesId = Convert.ToInt32(mainInfo)
                });
                var idTest = _tests.Get().Where(res => res.Topic == postTestForCourse.Topic).Select(res => res.Id).FirstOrDefault();
                var questions = postTestForCourse.AnswersForTests.Split("---end---");
                foreach (var question in questions)
                {
                    var questionTypeAnswers = question.Split("---main---");
                    var typeQuestion = questionTypeAnswers[0].Split("|");
                    if (typeQuestion.Length == 2)
                    {
                        if (typeQuestion[0] != null && typeQuestion[1] != null)
                        {
                            var type = (QuestionType)Enum.Parse(typeof(QuestionType), typeQuestion[0]);
                            _question.Create(new Questions
                            {
                                Topic = typeQuestion[1],
                                QuestionType = type,
                                TestId = idTest
                            });
                            var questionId = _question.Get().Where(res => res.QuestionType == type && res.TestId == idTest && res.Topic == typeQuestion[1]).Select(res => res.Id).FirstOrDefault();
                            foreach (var answers in questionTypeAnswers[1].Split("|n|"))
                            {
                                if (typeQuestion[0] == "Flags" || typeQuestion[0] == "Point")
                                {
                                    var pair = answers.Split("|");
                                    if (pair[1] != null && pair[0] != null)
                                    {
                                        var correct = bool.Parse(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pair[0].ToLower()));
                                        int? answerId = _answers.Get().Where(res => res.Topic == pair[1] && res.IsCorrect == correct).Select(res => res.Id).FirstOrDefault();
                                        if (answerId == null)
                                        {
                                            _answers.Create(new Answers
                                            {
                                                Topic = pair[1],
                                                IsCorrect = correct,
                                            });
                                            answerId = _answers.Get().Where(res => res.Topic == pair[1] && res.IsCorrect == correct).Select(res => res.Id).FirstOrDefault();
                                        }
                                        int? qaIsCreated = _questionAnswers.Get().Where(res => res.AnswerId == answerId && res.QuestionId == questionId).Select(res => res.Id).FirstOrDefault();
                                        if (qaIsCreated == null && answerId != null)
                                        {
                                            _questionAnswers.Create(new QuestionsAnswers
                                            {
                                                AnswerId = answerId ?? -1,
                                                QuestionId = questionId,
                                            });
                                        }
                                    }
                                }
                                if (typeQuestion[0] == "Custom")
                                {
                                    if (answers != null)
                                    {
                                        int? answerId = _answers.Get().Where(res => res.Topic == answers).Select(res => res.Id).FirstOrDefault();
                                        if (answerId == null)
                                        {
                                            _answers.Create(new Answers
                                            {
                                                Topic = answers,
                                                IsCorrect = true,
                                            });
                                            answerId = _answers.Get().Where(res => res.Topic == answers).Select(res => res.Id).FirstOrDefault();
                                        }
                                        _questionAnswers.Create(new QuestionsAnswers
                                        {
                                            AnswerId = answerId ?? -1,
                                            QuestionId = questionId,
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

