using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestPlatform.DAL.Entities
{
    class Photos
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        
    }
}
