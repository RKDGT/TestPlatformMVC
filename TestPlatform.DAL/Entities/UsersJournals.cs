using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL.Entities
{
    [Table("UsersJournals")]
    public class UsersJournals
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int JournalId { get; set; }
        public Journal Journal { get; set; }
    }
}
