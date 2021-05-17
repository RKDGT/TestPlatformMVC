using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Enums;
using TestPlatform.DAL.Identity;
namespace TestPlatform.DAL.Entities
{
    [Table("CoursesInfoTheoriesTestsQuestions")]
    public class Questions
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public QuestionType QuestionType { get; set; }
        [Required]
        public int TestId { get; set; }
        public Tests Test { get; set; }

        public List<QuestionAdditional> QuestionAdditionals { get; set; }
        public List<QuestionsAnswers> QuestionsAnswers { get; set; }
    }
}
