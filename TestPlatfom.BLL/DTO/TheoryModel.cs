using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatfom.BLL.DTO
{
    public class TheoryModel
    {
        public string Id { get; set; }
        public string NemeOfCourse { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public IFormFileCollection Files { get; set; }
        public List<string> FileNames { get; set; }
    }
}
