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
    /// Interaction logic for DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : UserControl
    {
        List<Department> ListDepartments = new List<Department>();
        bool isLoading = false;
        public DepartmentView()
        {
            InitializeComponent();

            UpdateDepartments();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxDepartments.SelectedItem == null)
                return;
            if (numberBox.Text == "")
            {
                errorText.Text = "Input Idef. Number";
                return;
            }
            if (addressBox.Text == "")
            {
                errorText.Text = "Input Address";
                return;
            }
            isLoading = true;
            Department dep = (Department)ListBoxDepartments.SelectedItem;
            DataAccess db = new DataAccess();
            db.UpdateDepartment(dep.ID_Department, numberBox.Text, addressBox.Text);
            UpdateDepartments();
            isLoading = false;
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxDepartments.SelectedItem == null)
                return;
            isLoading = true;
            Department dep = (Department)ListBoxDepartments.SelectedItem;
            DataAccess db = new DataAccess();
            db.DeleteDepartment(dep.ID_Department);
            UpdateDepartments();
            isLoading = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (numberNewBox.Text == "")
            {
                newErrorText.Text = "Input Idef. Number";
                return;
            }
            if (addressNewBox.Text == "")
            {
                newErrorText.Text = "Input Address";
                return;
            }
            int newID = ListDepartments.Last().ID_Department + 1;
            DataAccess db = new DataAccess();
            db.InsertDepartment(newID, numberNewBox.Text, addressNewBox.Text);
            UpdateDepartments();
            numberNewBox.Text = "";
            addressNewBox.Text = "";
        }

        private void ListBoxDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading) return;
            enableControls();
            Department dep = (Department)ListBoxDepartments.SelectedItem;
            numberBox.Text = dep.Number;
            addressBox.Text = dep.Address;
        }

        private void UpdateDepartments()
        {
            DataAccess db = new DataAccess();
            ListDepartments = db.GetDepartments();
            ListBoxDepartments.ItemsSource = ListDepartments;
            ListBoxDepartments.DisplayMemberPath = "Number";
            disableControls();
        }

        private void enableControls()
        {
            numberBox.IsReadOnly = false;
            addressBox.IsReadOnly = false;
        }

        private void disableControls()
        {
            numberBox.IsReadOnly = true;
            addressBox.IsReadOnly = true;
            numberBox.Text = "";
            addressBox.Text = "";   
        }
    }
}
