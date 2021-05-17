using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestPlatform.DAL.Identity;
namespace TestPlatform.DAL.Entities
{
    [Table("QuestionsAnswers")]
    public class QuestionsAnswers
    {
        public int Id { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public Questions Question { get; set; }
        [Required]
        public int AnswerId { get; set; }
        public Answers Answer { get; set; }
        public List<JournalSubJournal> JournalResForTest { get; set; }

    }
}
