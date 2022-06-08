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
    /// Interaction logic for PersonalTaskView.xaml
    /// </summary>
    public partial class PersonalTaskView : UserControl
    {
        DataAccess db = new DataAccess();
        public PersonalTaskView()
        {
            InitializeComponent();
        }

        private void optionsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            inputBox.Text = null;
            searchBox.SelectedItem = null;
            resultListBox.ItemsSource = null;
            searchBox.IsEnabled = false;
            inputBox.IsEnabled = false;
            searchButton.IsEnabled = false;
            if (optionsBox.SelectedIndex == 0)
            {
                List<Printer> printers = db.GetPrinters();
                searchBox.ItemsSource = printers;
                searchBox.DisplayMemberPath = "FullInfo";
                searchBox.IsEnabled = true;
            }
            else if(optionsBox.SelectedIndex == 1)
            {
                inputBox.IsEnabled = true;
                searchButton.IsEnabled = true;
            }
            else if (optionsBox.SelectedIndex == 2)
            {
                inputBox.IsEnabled = true;
                searchButton.IsEnabled = true;
            }
        }

        private void searchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (optionsBox.SelectedIndex == 0)
            {
                resultListBox.ItemsSource = null;
                Printer pri = (Printer)searchBox.SelectedItem;
                List<Manufacture> manufactures = db.GetManufacture();
                manufactures = manufactures.Where(x => x.ID_Printer == pri.ID_Printer).OrderBy(x => x.Copies).ToList();
                if (manufactures.Count == 0) return;
                List<Product> products = db.GetProducts();
                Product prod = products.Find(x => x.ID_Magazine == manufactures.First().ID_Magazine);
                if (prod == null) return;
                List<Editor> editors = db.GetEditors();
                editors = editors.Where(x => x.ID_Editor == prod.ID_Editor).ToList();
                resultListBox.ItemsSource = editors;
                resultListBox.DisplayMemberPath = "FullInfo";
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (optionsBox.SelectedIndex == 1)
            {
                if (int.TryParse(inputBox.Text, out _))
                {
                    List<Consignment> consignments = db.GetConsignments();
                    List<Magazine> magazines = db.GetMagazines();
                    List<int> magIDs = magazines.Where(x => x.Price >= int.Parse(inputBox.Text)).Select(x => x.ID_Magazine).ToList();
                    List<Consignment> filteredCons = new List<Consignment>();
                    foreach(Consignment cons in consignments)
                    {
                        if (magIDs.Contains(cons.ID_Magazine))
                            filteredCons.Add(cons);
                    }

                    filteredCons = filteredCons.OrderByDescending(x => x.Price).ToList();
                    resultListBox.ItemsSource = filteredCons;
                    resultListBox.DisplayMemberPath = "MaxPrice";
                }
            }
            if (optionsBox.SelectedIndex == 2)
            {
                if (int.TryParse(inputBox.Text, out _))
                {
                    List<Consignment> consignments = db.GetConsignments();
                    consignments = consignments.Where(x => x.Required < int.Parse(inputBox.Text)).OrderByDescending(x => x.Required).ToList();

                    resultListBox.ItemsSource = consignments;
                    resultListBox.DisplayMemberPath = "FullInfo";
                }
            }
        }
    }
}
