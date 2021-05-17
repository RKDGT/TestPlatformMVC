using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatfom.BLL.DTO
{
    public class CoursesModel
    {
        public string? Description { get; set; }
        public IFormFile Img { get; set; }
        public string ImgPath { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
