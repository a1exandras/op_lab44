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
    /// Interaction logic for ConsignmentView.xaml
    /// </summary>
    public partial class ConsignmentView : UserControl
    {
        List<Consignment> consignments = new List<Consignment>();
        List<Magazine> magazines = new List<Magazine>();
        List<Department> departments = new List<Department>();
        bool isLoading = false;
        string searchString = "";
        public ConsignmentView()
        {
            InitializeComponent();

            DataAccess db = new DataAccess();
            magazines = db.GetMagazines();
            departments = db.GetDepartments();

            magazineNewBox.ItemsSource = magazines;
            departmentBox.ItemsSource = departments;
            departmentNewBox.ItemsSource = departments;
            magazineNewBox.DisplayMemberPath = "Name";
            departmentBox.DisplayMemberPath = "Number";
            departmentNewBox.DisplayMemberPath = "Number";

            UpdateConsignmentLst();
        }

        private void ListBoxConsignments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading) return;

            Consignment cons = (Consignment)ListBoxConsignments.SelectedItem;
            Magazine mag = magazines.Find(x => x.ID_Magazine == cons.ID_Magazine);
            Department dep = departments.Find(x => x.ID_Department == cons.ID_Department);

            magazineBox.Text = mag.Name;
            departmentBox.Text = dep.Number;
            numberBox.Text = cons.Required.ToString();

            numberBox.IsReadOnly = false;
            departmentBox.IsEnabled = true;
        }

        private void clrButt_Click(object sender, RoutedEventArgs e)
        {
            isLoading = true;
            searchBar.Text = "";
            searchString = "";
            UpdateConsignmentLst();
            isLoading = false;
        }

        private void searchButt_Click(object sender, RoutedEventArgs e)
        {
            SearchInList();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxConsignments.SelectedItem == null)
                return;
            if (numberBox.Text == "")
                return;
            if (!int.TryParse(numberBox.Text, out _))
                return;
            isLoading = true;
            Consignment cons = (Consignment)ListBoxConsignments.SelectedItem;
            Department dep = (Department)departmentBox.SelectedItem;
            DataAccess db = new DataAccess();
            db.UpdateConsignment(cons.ID_Consignment, cons.ID_Magazine, dep.ID_Department, int.Parse(numberBox.Text));
            UpdateConsignmentLst();
            isLoading = false;
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxConsignments.SelectedItem == null)
                return;
            isLoading = true;
            Consignment cons = (Consignment)ListBoxConsignments.SelectedItem;
            DataAccess db = new DataAccess();
            db.DeleteConsignment(cons.ID_Consignment);
            UpdateConsignmentLst();
            isLoading = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (magazineNewBox.SelectedItem == null)
                return;
            if (departmentNewBox.SelectedItem == null)
                return;
            if (numberNewBox.Text == "")
                return;
            if (!int.TryParse(numberNewBox.Text, out _))
                return;
            isLoading = true;
            int newID = consignments.Max(x => x.ID_Consignment) + 1;
            Department dep = (Department)departmentNewBox.SelectedItem;
            Magazine mag = (Magazine)magazineNewBox.SelectedItem;
            DataAccess db = new DataAccess();
            db.InsertConsignment(newID, mag.ID_Magazine, dep.ID_Department, int.Parse(numberNewBox.Text));
            UpdateConsignmentLst();
            isLoading = false;

            magazineNewBox.SelectedItem = null;
            departmentNewBox.SelectedItem = null;
            numberNewBox.Text = "";
        }

        private void UpdateConsignmentLst()
        {
            DataAccess db = new DataAccess();
            consignments = db.GetConsignments();
            consignments = consignments.OrderBy(x => x.ID_Department).ThenBy(x => x.ID_Magazine).ToList();
            if (searchString == "")
            {
                ListBoxConsignments.ItemsSource = consignments;
                ListBoxConsignments.DisplayMemberPath = "FullInfo";
            }
            else
            {
                SearchInList();
            }

            disableControls();
            searchBar.Text = searchString;
        }

        private void SearchInList()
        {
            isLoading = true;
            searchString = searchBar.Text;
            List<Consignment> filteredCons = new List<Consignment>();
            foreach (Consignment cons in consignments)
            {
                if (cons.FullInfo.Contains(searchString))
                    filteredCons.Add(cons);
            }
            ListBoxConsignments.ItemsSource = filteredCons;
            //ListBoxConsignments.DisplayMemberPath = "FullInfo";
            if (!filteredCons.Contains((Consignment)ListBoxConsignments.SelectedItem))
                disableControls();
            isLoading = false;
        }

        private void disableControls()
        {
            magazineBox.Text = "";
            numberBox.Text = "";
            numberBox.IsReadOnly = true;
            departmentBox.SelectedItem = null;
            departmentBox.IsEnabled = false;
        }

        private void magazineNewBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
