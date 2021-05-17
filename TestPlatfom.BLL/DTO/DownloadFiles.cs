using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatfom.BLL.DTO
{
    public class DownloadFiles
    {
        public MemoryStream mem { get; set; }
        public string myme { get; set; }
        public string path { get; set; }
    }
}
