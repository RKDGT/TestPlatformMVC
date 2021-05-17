using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL.Entities
{
    [Table("Courses")]
    public class Courses
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CourseInfoId { get; set; }
        public CourseInfo CourseInfo { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfFinish { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public List<GroupsCourses> GroupsCourses { get; set; }
        public List<UsersCourses> User { get; set; }
        public List<Journal> Journal { get; set; }
    }
}