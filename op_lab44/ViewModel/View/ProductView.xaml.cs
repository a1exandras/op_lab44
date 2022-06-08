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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        List<Product> products = new List<Product>();
        List<Magazine> magazines = new List<Magazine>();
        List<Producer> producers = new List<Producer>();
        List<Editor> editors = new List<Editor>();
        bool isLoading = false;
        public ProductView()
        {
            InitializeComponent();

            DataAccess db = new DataAccess();
            magazines = db.GetMagazines();
            producers = db.GetProducers();
            editors = db.GetEditors();

            editorBox.ItemsSource = editors;
            editorBox.DisplayMemberPath = "FullInfo";
            producerBox.ItemsSource = producers;
            producerBox.DisplayMemberPath = "Post_Index";

            editorNewBox.ItemsSource = editors;
            editorNewBox.DisplayMemberPath = "FullInfo";
            producerNewBox.ItemsSource = producers;
            producerNewBox.DisplayMemberPath = "Post_Index";

            UpdateProducts();
        }

        private void ListBoxProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading) return;

            DataAccess db = new DataAccess();
            Product product = (Product)ListBoxProducts.SelectedItem;
            Magazine mag = magazines.Find(x => x.ID_Magazine == product.ID_Magazine);
            Producer pr = producers.Find(x => x.ID_Producer == product.ID_Producer);
            Editor ed = editors.Find(x => x.ID_Editor == product.ID_Editor);

            magazineBox.Text = mag.Name;
            producerBox.Text = pr.Post_Index;
            editorBox.Text = ed.FullInfo;

            enableControls();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxProducts.SelectedItem == null)
                return;
            isLoading = true;
            Product pr = (Product)ListBoxProducts.SelectedItem;
            Producer pro = (Producer)producerBox.SelectedItem;
            Editor ed = (Editor)editorBox.SelectedItem;
            DataAccess db = new DataAccess();
            db.UpdateProduct(pr.ID_Magazine, pro.ID_Producer, ed.ID_Editor);
            UpdateProducts();
            isLoading = false;
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxProducts.SelectedItem == null)
                return;
            isLoading = true;
            Product pr = (Product)ListBoxProducts.SelectedItem;
            DataAccess db = new DataAccess();
            db.DeleteProduct(pr.ID_Magazine);
            UpdateProducts();
            isLoading = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(magazineNewBox.SelectedItem == null)
            {
                return;
            }
            if (producerNewBox.SelectedItem == null)
            {
                return;
            }
            if (editorNewBox.SelectedItem == null)
            {
                return;
            }
            Magazine mag = (Magazine)magazineNewBox.SelectedItem;
            Producer pro = (Producer)producerNewBox.SelectedItem;
            Editor ed = (Editor)editorNewBox.SelectedItem;
            DataAccess db = new DataAccess();
            db.InsertProduct(mag.ID_Magazine, pro.ID_Producer, ed.ID_Editor);
            UpdateProducts();

            producerNewBox.SelectedItem = null;
            editorNewBox.SelectedItem = null;
            producerNewBox.IsEnabled = false;
            editorNewBox.IsEnabled = false;
        }

        private void UpdateProducts()
        {
            DataAccess db = new DataAccess();
            products = db.GetProducts();
            ListBoxProducts.ItemsSource = products;
            ListBoxProducts.DisplayMemberPath = "Name";

            List<Magazine> spareMags = new List<Magazine>();
            foreach (Magazine mag in magazines)
            {
                if (!products.Exists(x => x.ID_Magazine == mag.ID_Magazine))
                    spareMags.Add(mag);
            }
            magazineNewBox.ItemsSource = spareMags;
            magazineNewBox.DisplayMemberPath = "Name";
            disableControls();
        }

        private void magazineNewBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            producerNewBox.IsEnabled = true;
            editorNewBox.IsEnabled = true;
        }

        private void enableControls()
        {
            producerBox.IsEnabled = true;
            editorBox.IsEnabled = true;
        }
        private void disableControls()
        {
            magazineBox.Text = "";
            producerBox.SelectedItem = null;
            editorBox.SelectedItem = null;
            producerBox.IsEnabled = false;
            editorBox.IsEnabled = false;
        }
    }
}
