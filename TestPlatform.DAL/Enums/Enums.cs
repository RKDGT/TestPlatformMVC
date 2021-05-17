using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatform.DAL.Enums
{
    public enum SignedCoursesStatus
    {
        Comleted,
        InProgress,
        NotStarted
    }
    public enum Roles
    {
        SuperAdmin,
        Admin,
        Lector,
        Mentor,
        Student
    }
    public enum QuestionType
    {
        Point,
        Flags,
        Custom
    }
}
