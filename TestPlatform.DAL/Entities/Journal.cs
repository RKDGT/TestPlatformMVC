using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPlatform.DAL.Entities
{
    [Table("Journals")]
    public class Journal
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId  { get; set; }

        public Courses Course { get; set; }
        public List<JournalSubJournal> JournalSubJournals { get; set; }
        public List<UsersJournals> UserJournal { get; set; }
    }
}