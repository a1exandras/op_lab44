using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace op_lab44.ViewModel.View
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        DataAccess db = new DataAccess();
        List<Manufacture> manufactures = new List<Manufacture>();
        public ReportView()
        {
            InitializeComponent();
            List<Printer> printers = db.GetPrinters();
            selectBox.ItemsSource = printers;
            selectBox.DisplayMemberPath = "Name";

            manufactures = db.GetManufacture();
        }

        private void selectBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Printer pr = (Printer)selectBox.SelectedItem;
            List<Manufacture> filteredMan = manufactures.Where(x => x.ID_Printer == pr.ID_Printer).OrderBy(x => x.ID_Magazine).ToList();

            List<int> indxes = new List<int>();
            for (int i = 0; i < filteredMan.Count - 1; i++)
            {
                if (filteredMan[i].ID_Magazine == filteredMan[i + 1].ID_Magazine)
                {
                    filteredMan[i + 1].Copies += filteredMan[i].Copies;
                    indxes.Insert(0, i);
                }
            }
            foreach(int i in indxes)
            {
                filteredMan.RemoveAt(i);
            }

            printersListBox.ItemsSource = filteredMan;
            printersListBox.DisplayMemberPath = "FullInfo1";

            int sum = filteredMan.Sum(x => x.Copies);
            nunmberBox.Text = sum.ToString();
        }
    }
}
