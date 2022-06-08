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
    /// Interaction logic for ProducerView.xaml
    /// </summary>
    public partial class ProducerView : UserControl
    {
        List<Producer> ListProducers = new List<Producer>();
        bool isLoading = false;
        public ProducerView()
        {
            InitializeComponent();

            UpdateProducers();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxProducers.SelectedItem == null)
                return;
            if (indexBox.Text == "")
            {
                errorText.Text = "Enter Index!";
                return;
            }    
            isLoading = true;
            Producer pr = (Producer)ListBoxProducers.SelectedItem;
            DataAccess db = new DataAccess();
            db.UpdateProducer(pr.ID_Producer, indexBox.Text);
            UpdateProducers();
            isLoading = false;
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxProducers.SelectedItem == null)
                return;
            isLoading = true;
            Producer pr = (Producer)ListBoxProducers.SelectedItem;
            DataAccess db = new DataAccess();
            db.DeleteProducer(pr.ID_Producer);
            UpdateProducers();
            isLoading = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (indexNewBox.Text == "")
            {
                newErrorText.Text = "Enter Index!";
                return;
            }
            int newID = ListProducers.Last().ID_Producer + 1;
            DataAccess db = new DataAccess();
            db.InsertProducer(newID, indexNewBox.Text);
            UpdateProducers();
            indexNewBox.Text = "";
        }

        private void UpdateProducers()
        {
            DataAccess db = new DataAccess();
            ListProducers = db.GetProducers();
            ListBoxProducers.ItemsSource = ListProducers;
            ListBoxProducers.DisplayMemberPath = "Post_Index";
            indexBox.IsReadOnly = true;
            indexBox.Text = "";
        }
        private void ListBoxProducers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading) return;
            indexBox.IsReadOnly = false;
            Producer pr = (Producer)ListBoxProducers.SelectedItem;
            indexBox.Text = pr.Post_Index;
        }
    }
}
