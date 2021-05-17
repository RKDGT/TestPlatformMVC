using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Enums;
namespace TestPlatform.DAL.Entities
{
    [Table("GroupsCourses")]
    public class GroupsCourses
    {
        public int Id { get; set; }
        [Required]
        public int GroupId { get; set; }
        public Groups Group { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Courses Course { get; set; }
        [Required]
        public SignedCoursesStatus SignedCoursesStatus { get; set; }
    }
}
