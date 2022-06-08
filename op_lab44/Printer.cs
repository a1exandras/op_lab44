using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace op_lab44
{
    public class Printer
    {
        public int ID_Printer { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Is_Closed { get; set; }
        public string FullInfo
        {
            get { return "["+ (Is_Closed ? "Closed" : "Open") + $"], {Name}"; }
        }
    }
}
