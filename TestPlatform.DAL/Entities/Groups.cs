using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL.Entities
{
    [Table("Groups")]
    public class Groups
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string  Name { get; set; }

        [Required]
        public string MentorId { get; set; }
        public User Mentor { get; set; }
        
        public List<GroupsCourses> GroupsCourses { get; set; }
        public List<UsersGroups> UsersGroups { get; set; }
    }
}
