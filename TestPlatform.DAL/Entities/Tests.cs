using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL.Entities
{
    [Table("CoursesInfoTheoriesTests")]
    public class Tests
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }
        public string Description { get; set; }
        [Required]
        public int TheoriesId { get; set; }
        public Theory Theories { get; set; }
        public List<Questions> Questions { get; set; }
    }
}
