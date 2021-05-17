using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Identity;

namespace TestPlatform.DAL.Entities
{
    [Table("CoursesInfoTheoriesTestsQuestionsAnswers")]
    public class Answers
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
        public List<QuestionsAnswers> QuestionsAnswers { get; set; }
    }
}
