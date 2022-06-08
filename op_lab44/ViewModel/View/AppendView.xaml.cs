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
    /// Interaction logic for AppendView.xaml
    /// </summary>
    public partial class AppendView : UserControl
    {
        List<Magazine> ListMagazines = new List<Magazine>();
        bool isLoading = false;
        public AppendView()
        {
            InitializeComponent();

            UpdateListMagazines();
            
        }

        private void ListBoxMagazines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading) return;
            Magazine mag = (Magazine)ListBoxMagazines.SelectedItem;
            nameBox.Text = mag.Name;
            priceBox.Text = mag.Price.ToString();
            enableControls();
        }   

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxMagazines.SelectedItem == null)
                return;
            isLoading = true;
            DataAccess db = new DataAccess();
            Magazine mag = (Magazine)ListBoxMagazines.SelectedItem;
            db.DeleteMagazine(mag.ID_Magazine);
            nameBox.Text = "";
            priceBox.Text = "";    
            UpdateListMagazines();
            isLoading = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameNewBox.Text == "")
            {
                newErrorText.Text = "Enter Name!";
                return;
            }
            if (priceNewBox.Text == "")
            {
                newErrorText.Text = "Enter Price!";
                return;
            }
            if(!int.TryParse(priceNewBox.Text, out _))
            {
                newErrorText.Text = "Enter numbers!";
                return;
            }
            int index = ListMagazines.Last().ID_Magazine + 1;
            DataAccess db = new DataAccess();
            db.AddMagazine(index, nameNewBox.Text, Int32.Parse(priceNewBox.Text));
            UpdateListMagazines();
            nameNewBox.Text = "";
            priceNewBox.Text = "";
            newErrorText.Text = "";
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "")
            {
                errorText.Text = "Enter Name!";
                return;
            }
            if (priceBox.Text == "")
            {
                errorText.Text = "Enter Price!";
                return;
            }
            if (!int.TryParse(priceBox.Text, out _))
            {
                errorText.Text = "Enter numbers!";
                return;
            }
            isLoading = true;
            Magazine mag = (Magazine)ListBoxMagazines.SelectedItem;
            DataAccess db = new DataAccess();
            db.UpdateMagazine(mag.ID_Magazine, nameBox.Text, Int32.Parse(priceBox.Text));
            UpdateListMagazines();
            isLoading = false;
        }

        private void UpdateListMagazines()
        {
            DataAccess db = new DataAccess();
            ListMagazines = db.GetMagazines();
            ListBoxMagazines.ItemsSource = ListMagazines;
            ListBoxMagazines.DisplayMemberPath = "Name";
            disableControls();
        }

        private void enableControls()
        {
            nameBox.IsReadOnly= false;
            priceBox.IsReadOnly= false;
        }

        private void disableControls()
        {
            nameBox.Text = "";
            priceBox.Text = "";
            nameBox.IsReadOnly = true;
            priceBox.IsReadOnly = true;
        }
    }
}
