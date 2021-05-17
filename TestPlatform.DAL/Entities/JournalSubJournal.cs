using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPlatform.DAL.Entities
{
    [Table("JournalSubJournal")]
    public class JournalSubJournal
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int JournalId { get; set; }
        public Journal Journal { get; set; }
        [Required]
        public int QuestionAnswersId { get; set; }
        public QuestionsAnswers QuestionsAnswers { get; set; }
    }
}
