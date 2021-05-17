using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatform.DAL.Entities
{
    public class TheoryFiles
    {
        public int Id { get; set; }
        public string filePath { get; set; }
        public int TheoryId{ get; set; }
        public Theory Theory{ get; set; }

    }
}
