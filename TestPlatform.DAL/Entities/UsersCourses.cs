using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Enums;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL.Entities
{
    [Table("UsersCourses")]
    public class UsersCourses
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Courses Course { get; set; }
        [Required]
        public SignedCoursesStatus SignedCoursesStatus { get; set; }
    }
}
