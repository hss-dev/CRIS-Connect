using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRISConnectDemo
{
    class Command
    {
        public string command { get; set; }
        public List<string> accession { get; set; }
        public string examCode { get; set; }
        public string nhs { get; set; }
        public string chi { get; set; }
    }

}
