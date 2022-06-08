using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace op_lab44
{
    public class DataAccess
    {
        //MAGAZINES
        public List<Magazine> GetMagazines()
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                return connection.Query<Magazine>("dbo.Magazines_FullInfo").ToList();
            }
        }
        public void AddMagazine(int id_magazine, string name, int price)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Magazine> magazines = new List<Magazine>();
                magazines.Add(new Magazine { ID_Magazine = id_magazine, Name = name, Price = price });

                connection.Execute("dbo.Magazines_Insert @ID_Magazine, @Name, @Price", magazines);
            }
        }

        public void DeleteMagazine(int id_magazine)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                connection.Execute("dbo.Magazines_Delete @ID_Magazine", new { ID_Magazine = id_magazine });
            }
        }

        public void UpdateMagazine(int id_magazine, string name, int price)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Magazine> magazines = new List<Magazine>();
                magazines.Add(new Magazine { ID_Magazine = id_magazine, Name = name, Price = price });

                connection.Execute("dbo.Magazines_Update @ID_Magazine, @Name, @Price", magazines);
            }
        }

        //EDITORS
        public List<Editor> GetEditors()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                return connection.Query<Editor>("dbo.Editors_FullInfo").ToList();
            }
        }

        public void DeleteEditor(int id_editor)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                connection.Execute("dbo.Editors_Delete @ID_Editor", new { ID_Editor = id_editor });
            }
        }

        public void UpdateEditor(int id_editor, string name, string surname, string sec_name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Editor> editors = new List<Editor>();
                editors.Add(new Editor { ID_Editor = id_editor, Name = name, Surname = surname, Sec_Name = sec_name });

                connection.Execute("dbo.Editors_Update @ID_Editor, @Name, @Surname, @Sec_Name", editors);
            }
        }

        public void InsertEditor(int id_editor, string name, string surname, string sec_name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Editor> editors = new List<Editor>();
                editors.Add(new Editor { ID_Editor = id_editor, Name = name, Surname = surname, Sec_Name = sec_name });

                connection.Execute("dbo.Editors_Insert @ID_Editor, @Name, @Surname, @Sec_Name", editors);
            }
        }

        //PRINTERS
        public List<Printer> GetPrinters()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                return connection.Query<Printer>("dbo.Printers_FullInfo").ToList();
            }
        }

        public void DeletePrinter(int id_printer)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                connection.Execute("dbo.Printers_Delete @ID_Printer", new { ID_Printer = id_printer });
            }
        }

        public void UpdatePrinter(int id_printer, string name, string address, bool is_closed)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Printer> printers = new List<Printer>();
                printers.Add(new Printer { ID_Printer = id_printer, Name = name, Address = address, Is_Closed = is_closed});

                connection.Execute("dbo.Printers_Update @ID_Printer, @Name, @Address, @Is_Closed", printers);
            }
        }

        public void InsertPrinter(int id_printer, string name, string address, bool is_closed)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Printer> printers = new List<Printer>();
                printers.Add(new Printer { ID_Printer = id_printer, Name = name, Address = address, Is_Closed = is_closed});

                connection.Execute("dbo.Printers_Insert @ID_Printer, @Name, @Address, @Is_Closed", printers);
            }
        }

        //PRODUCERS
        public List<Producer> GetProducers()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                return connection.Query<Producer>("dbo.Producers_FullInfo").ToList();
            }
        }

        public void DeleteProducer(int id_producer)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                connection.Execute("dbo.Producers_Delete @ID_Producer", new { ID_Producer = id_producer });    
            }
        }

        public void UpdateProducer(int id_producer, string post_index)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Producer> producers = new List<Producer>();
                producers.Add(new Producer { ID_Producer = id_producer, Post_Index = post_index });

                connection.Execute("dbo.Producers_Update @ID_Producer, @Post_Index", producers);
            }
        }

        public void InsertProducer(int id_producer, string post_index)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Producer> producers = new List<Producer>();
                producers.Add(new Producer { ID_Producer = id_producer, Post_Index = post_index});

                connection.Execute("dbo.Producers_Insert @ID_Producer, @Post_Index", producers);
            }
        }

        //DEPARTMENTS

        public List<Department> GetDepartments()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                return connection.Query<Department>("dbo.Departments_FullInfo").ToList();
            }
        }

        public void DeleteDepartment(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                connection.Execute("dbo.Departments_Delete @ID_Department", new { ID_Department = id });
            }
        }

        public void UpdateDepartment(int id, string number, string address)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Department> departments = new List<Department>();
                departments.Add(new Department { ID_Department = id, Number = number, Address = address });

                connection.Execute("dbo.Departments_Update @ID_Department, @Number, @Address", departments);
            }
        }

        public void InsertDepartment(int id, string number, string address)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Department> departments = new List<Department>();
                departments.Add(new Department { ID_Department = id, Number = number, Address = address });

                connection.Execute("dbo.Departments_Insert @ID_Department, @Number, @Address", departments);
            }
        }

        //PRODUCTS
        public List<Product> GetProducts()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                return connection.Query<Product>("dbo.Product_FullInfo").ToList();
            }
        }

        public void DeleteProduct(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                connection.Execute("dbo.Product_Delete @ID_Magazine", new { ID_Magazine = id });
            }
        }

        public void UpdateProduct(int id, int id_p, int id_e)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Product> products = new List<Product>();
                products.Add(new Product { ID_Magazine = id, ID_Producer = id_p, ID_Editor = id_e });

                connection.Execute("dbo.Products_Update @ID_Magazine, @ID_Producer, @ID_Editor", products);
            }
        }

        public void InsertProduct(int id, int id_p, int id_e)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Product> products = new List<Product>();
                products.Add(new Product { ID_Magazine = id, ID_Producer = id_p, ID_Editor = id_e });

                connection.Execute("dbo.Products_Insert @ID_Magazine, @ID_Producer, @ID_Editor", products);
            }
        }

        //MANUFACTURING
        public List<Manufacture> GetManufacture()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                return connection.Query<Manufacture>("dbo.Manufacture_FullInfo").ToList();
            }
        }

        public void DeleteManufacture(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                connection.Execute("dbo.Manufacture_Delete @ID_Manufacture", new { ID_Manufacture = id });
            }
        }

        public void UpdateManufacture(int id, int id_m, int id_p, int copies)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Manufacture> manufactures = new List<Manufacture>();
                manufactures.Add(new Manufacture { ID_Manufacture = id, ID_Magazine = id_m, ID_Printer = id_p, Copies = copies });

                connection.Execute("dbo.Manufacture_Update @ID_Manufacture, @ID_Magazine, @ID_Printer, @Copies", manufactures);
            }
        }

        public void InsertManufacture(int id, int id_m, int id_p, int copies)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Manufacture> manufactures = new List<Manufacture>();
                manufactures.Add(new Manufacture { ID_Manufacture = id, ID_Magazine = id_m, ID_Printer = id_p, Copies = copies });

                connection.Execute("dbo.Manufacture_Insert @ID_Manufacture, @ID_Magazine, @ID_Printer, @Copies", manufactures);
            }
        }

        //CONSIGNMENTS

        public List<Consignment> GetConsignments()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                return connection.Query<Consignment>("dbo.Consignment_FullInfo").ToList();
            }
        }

        public void DeleteConsignment(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                connection.Execute("dbo.Consignment_Delete @ID_Consignment", new { ID_Consignment = id });
            }
        }
        public void UpdateConsignment(int id, int id_m, int id_d, int req)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Consignment> consignments = new List<Consignment>();
                consignments.Add(new Consignment { ID_Consignment = id, ID_Magazine = id_m, ID_Department = id_d, Required = req });

                connection.Execute("dbo.Consignment_Update @ID_Consignment, @ID_Magazine, @ID_Department, @Required", consignments);
            }
        }
        public void InsertConsignment(int id, int id_m, int id_d, int req)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TestDB")))
            {
                List<Consignment> consignments = new List<Consignment>();
                consignments.Add(new Consignment { ID_Consignment = id, ID_Magazine = id_m, ID_Department = id_d, Required = req });

                connection.Execute("dbo.Consignment_Insert @ID_Consignment, @ID_Magazine, @ID_Department, @Required", consignments);
            }
        }
    }
}
