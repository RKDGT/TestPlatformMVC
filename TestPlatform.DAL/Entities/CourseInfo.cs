using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL.Entities
{   
    [Table("CoursesInfo")]
    public class CourseInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string MainCourseImgPath { get; set; }
        public List<Theory> Theories { get; set; }
    }
}
