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
    /// Interaction logic for ManufactureView.xaml
    /// </summary>
    public partial class ManufactureView : UserControl
    {
        List<Magazine> magazines = new List<Magazine>();
        List<Printer> printers = new List<Printer>();
        List<Manufacture> manufactures = new List<Manufacture>();
        bool isLoading = false;
        string searchString = "";
        public ManufactureView()
        {
            InitializeComponent();

            DataAccess db = new DataAccess();
            magazines = db.GetMagazines();
            printers = db.GetPrinters();

            magazineNewBox.ItemsSource = magazines;
            printerNewBox.ItemsSource = printers;
            printerBox.ItemsSource=printers;
            magazineNewBox.DisplayMemberPath = "Name";
            printerNewBox.DisplayMemberPath = "FullInfo";
            printerBox.DisplayMemberPath = "FullInfo";

            UpdateManufacturesList();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (numberBox.Text == "")
                return;
            if (!int.TryParse(numberBox.Text, out _))
                return;
            isLoading = true;
            Manufacture man = (Manufacture)ListBoxManufactures.SelectedItem;
            Printer printer = (Printer)printerBox.SelectedItem;
            DataAccess db = new DataAccess();
            db.UpdateManufacture(man.ID_Manufacture, man.ID_Magazine, printer.ID_Printer, int.Parse(numberBox.Text));
            UpdateManufacturesList();
            isLoading = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (magazineNewBox.SelectedItem == null)
                return;
            if (printerNewBox.SelectedItem == null)
                return;
            if (numberNewBox.Text == "")
                return;
            if (!int.TryParse(numberNewBox.Text, out _))
                return;
            isLoading = true;
            int newID = manufactures.Max(x => x.ID_Manufacture) + 1;
            Printer printer = (Printer)printerNewBox.SelectedItem;
            Magazine mag = (Magazine)magazineNewBox.SelectedItem;
            DataAccess db = new DataAccess();
            db.InsertManufacture(newID, mag.ID_Magazine, printer.ID_Printer, int.Parse(numberNewBox.Text));
            UpdateManufacturesList();
            isLoading = false;

            magazineNewBox.SelectedItem = null;
            printerNewBox.SelectedItem = null;
            numberNewBox.Text = "";

        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxManufactures.SelectedItem == null) return;
            isLoading = true;
            Manufacture man = (Manufacture)ListBoxManufactures.SelectedItem;
            DataAccess db = new DataAccess();
            db.DeleteManufacture(man.ID_Manufacture);
            UpdateManufacturesList();
            isLoading = false;
        }

        private void magazineNewBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdateManufacturesList()
        {
            DataAccess db = new DataAccess();
            manufactures = db.GetManufacture();
            manufactures = manufactures.OrderBy(x => x.ID_Magazine).ThenBy(x => x.ID_Printer).ToList();
            if (searchString == "")
            {
                ListBoxManufactures.ItemsSource = manufactures;
                ListBoxManufactures.DisplayMemberPath = "FullInfo";
            }
            else
            {
                searchInList();
            }

            disableCOntrols();
            searchBar.Text = searchString;
        }

        private void ListBoxManufactures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading) return;

            Manufacture man = (Manufacture)ListBoxManufactures.SelectedItem;
            Magazine mag = magazines.Find(x => x.ID_Magazine == man.ID_Magazine);
            Printer pri = printers.Find(x => x.ID_Printer == man.ID_Printer);

            magazineBox.Text = mag.Name;
            printerBox.Text = pri.FullInfo;
            numberBox.Text = man.Copies.ToString();

            numberBox.IsReadOnly = false;
            printerBox.IsEnabled = true;
        }

        private void searchButt_Click(object sender, RoutedEventArgs e)
        {
            searchInList();
        }

        private void disableCOntrols()
        {
            magazineBox.Text = "";
            numberBox.Text = "";
            numberBox.IsReadOnly = true;
            printerBox.SelectedItem = null;
            printerBox.IsEnabled = false;
        }

        private void clrButt_Click(object sender, RoutedEventArgs e)
        {
            isLoading = true;
            searchBar.Text = "";
            searchString = "";
            UpdateManufacturesList();
            isLoading = false;
        }

        private void searchInList()
        {
            isLoading = true;
            searchString = searchBar.Text;
            List<Manufacture> filteredMan = new List<Manufacture>();
            foreach (Manufacture man in manufactures)
            {
                if (man.FullInfo.Contains(searchString))
                    filteredMan.Add(man);
            }
            ListBoxManufactures.ItemsSource = filteredMan;
            ListBoxManufactures.DisplayMemberPath = "FullInfo";
            if (!filteredMan.Contains((Manufacture)ListBoxManufactures.SelectedItem))
                disableCOntrols();
            isLoading = false;
        }
    }
}