using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPlatform.DAL.Entities
{
    [Table("CoursesInfoTheories")]
    public class Theory
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }
        public string Details { get; set; }
        [Required]
        public int CourseInfoId { get; set; }
        public CourseInfo CoursesInfo { get; set; }

        public List<Tests> Tests { get; set; }
    }
}
