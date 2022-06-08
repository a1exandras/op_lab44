using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace op_lab44
{
    public class Manufacture
    {
        public int ID_Manufacture { get; set; }
        public int ID_Magazine { get; set; }
        public int ID_Printer { get; set; }
        public int Copies { get; set; }

        public string FullInfo
        {
            get
            {
                DataAccess db = new DataAccess();
                Magazine mag = db.GetMagazines().Find(x => x.ID_Magazine == ID_Magazine);
                Printer pri = db.GetPrinters().Find(x => x.ID_Printer == ID_Printer);
                return $"[{Copies}] : {mag.Name} - {pri.Name}";
            }
        }

        public string FullInfo1
        {
            get
            {
                DataAccess db = new DataAccess();
                Magazine mag = db.GetMagazines().Find(x => x.ID_Magazine == ID_Magazine);
                return $"{mag.Name} : [{Copies}]";
            }
        }
    }
}
