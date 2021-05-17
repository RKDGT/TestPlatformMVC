using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Enums;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL.Entities
{
    [Table("QuestionAdditional")]
    public class QuestionAdditional
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Info { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public Questions Questions { get; set; }
    }
}
