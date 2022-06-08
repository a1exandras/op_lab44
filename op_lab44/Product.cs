using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace op_lab44
{
    public class Product
    {
        public int ID_Magazine { get; set; }
        public int ID_Producer { get; set; }
        public int ID_Editor { get; set; }

        public string Name { 
            get
            {
                DataAccess db = new DataAccess();
                Magazine mag = db.GetMagazines().Find(x => x.ID_Magazine == ID_Magazine);
                return mag.Name;
            }
        }
    }
}
