using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace op_lab44
{
    public class Consignment
    {
        public int ID_Consignment { get; set; }
        public int ID_Magazine { get; set; }
        public int ID_Department { get; set; }
        public int Required { get; set; }

        public string FullInfo
        {
            get
            {
                DataAccess db = new DataAccess();
                Magazine mag = db.GetMagazines().Find(x => x.ID_Magazine == ID_Magazine);
                Department dep = db.GetDepartments().Find(x => x.ID_Department == ID_Department);
                return $"[{Required}] : {dep.Number} - {mag.Name}";
            }
        }
        public string MaxPrice
        {
            get
            {
                DataAccess db = new DataAccess();
                Magazine mag = db.GetMagazines().Find(x => x.ID_Magazine == ID_Magazine);
                Department dep = db.GetDepartments().Find(x => x.ID_Department == ID_Department);
                return $"{mag.Name} ({dep.Number}) - {mag.Price}";
            }
        }
        public int Price
        {
            get
            {
                DataAccess db = new DataAccess();
                Magazine mag = db.GetMagazines().Find(x => x.ID_Magazine == ID_Magazine);
                return mag.Price;
            }
        }
    }
}
