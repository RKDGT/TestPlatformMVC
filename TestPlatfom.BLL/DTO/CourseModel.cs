using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatfom.BLL.DTO
{
    public class CourseModel
    {
        public string Id { get; set; }
     
        public string NameOfCourse { get; set; }
        
        public string DescriptionOfCourse { get; set; }
        
        public List<TheoryModel> AdditionalInform { get; set; }
    }
}
