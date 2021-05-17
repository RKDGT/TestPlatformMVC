using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Entities;

namespace TestPlatform.DAL.Identity
{ 
    [Table("Users")]
    public class User: IdentityUser
    {
        [Required]
        [PersonalData]
        public string Name { get; set; }

        [Required]
        [PersonalData]
        public string SurName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public List<UsersJournals> UserJournal { get; set; }
        public List<UsersGroups> UserGroup { get; set; }
        public List<UsersCourses> UserCourse { get; set; }

    }
}
