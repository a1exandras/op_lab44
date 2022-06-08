using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace op_lab44
{
    public class Editor
    {
        public int ID_Editor { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sec_Name { get; set; }

        public string FullInfo
        {
            get { return $"{Surname}, {Name}"; }
        }

    }
}
