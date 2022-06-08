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
    /// Interaction logic for PrinterView.xaml
    /// </summary>
    public partial class PrinterView : UserControl
    {
        List<Printer> ListPrinters = new List<Printer>();
        bool isLoading = false;
        public PrinterView()
        {
            InitializeComponent();

            UpdatePrinters();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameNewBox.Text == "")
            {
                errorText.Text = "Enter Name!";
                return;
            }
            if (addressNewBox.Text == "")
            {
                errorText.Text = "Enter Address!";
                return;
            }
            int index = ListPrinters.Last().ID_Printer + 1;
            DataAccess db = new DataAccess();
            db.InsertPrinter(index, nameNewBox.Text, addressNewBox.Text, (bool)closedNewBox.IsChecked);
            UpdatePrinters();
            nameNewBox.Text = "";
            addressNewBox.Text = "";
            closedNewBox.IsChecked = false;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxPrinters.SelectedItem == null)
                return;
            if (nameBox.Text == "")
            {
                errorText.Text = "Enter Name!";
                return;
            }
            if (addressBox.Text == "")
            {
                errorText.Text = "Enter Address!";
                return;
            }
            isLoading = true;
            DataAccess db = new DataAccess();
            Printer pr = (Printer)ListBoxPrinters.SelectedItem;
            db.UpdatePrinter(pr.ID_Printer, nameBox.Text, addressBox.Text, (bool)closedBox.IsChecked);
            UpdatePrinters();
            isLoading = false;
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxPrinters.SelectedItem == null)
                return;
            isLoading = true;
            DataAccess db = new DataAccess();
            Printer pr = (Printer)ListBoxPrinters.SelectedItem;
            db.DeletePrinter(pr.ID_Printer);
            UpdatePrinters();
            isLoading = false;
        }

        private void ListBoxPrinters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading)
                return;
            enableControls();
            Printer pr = (Printer)ListBoxPrinters.SelectedItem;
            nameBox.Text = pr.Name;
            addressBox.Text = pr.Address;
            closedBox.IsChecked = pr.Is_Closed;
        }

        private void UpdatePrinters()
        {
            DataAccess db = new DataAccess();
            ListPrinters = db.GetPrinters();
            ListBoxPrinters.ItemsSource = ListPrinters;
            ListBoxPrinters.DisplayMemberPath = "FullInfo";
            disableControls();
        }

        private void enableControls()
        {
            nameBox.IsReadOnly = false;
            addressBox.IsReadOnly = false;
            closedBox.IsEnabled = true;
        }

        private void disableControls()
        {
            nameBox.Text = "";
            addressBox.Text = "";
            closedBox.IsChecked = false;
            nameBox.IsReadOnly = true;
            addressBox.IsReadOnly = true;
            closedBox.IsEnabled = false;
        }
    }
}
