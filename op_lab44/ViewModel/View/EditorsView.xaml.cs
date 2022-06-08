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
    /// Interaction logic for EditorsView.xaml
    /// </summary>
    public partial class EditorsView : UserControl
    {
        bool isLoading = false;
        List<Editor> ListEditors = new List<Editor>();
        public EditorsView()
        {
            InitializeComponent();

            UpdateListEditors();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxEditors.SelectedItem == null)
                return;
            if (nameBox.Text == "")
            {
                errorText.Text = "Enter Name!";
                return;
            }
            if (surnameBox.Text == "")
            {
                errorText.Text = "Enter Surname!";
                return;
            }
            isLoading = true;
            DataAccess db = new DataAccess();
            Editor ed = (Editor)ListBoxEditors.SelectedItem;
            db.UpdateEditor(ed.ID_Editor, nameBox.Text, surnameBox.Text, secNameBox.Text);
            UpdateListEditors();
            isLoading = false;
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxEditors.SelectedItem == null) 
                return;
            isLoading = true;
            DataAccess db = new DataAccess();
            Editor ed = (Editor)ListBoxEditors.SelectedItem;
            db.DeleteEditor(ed.ID_Editor);
            UpdateListEditors();
            isLoading = false;
            nameBox.Text = "";
            surnameBox.Text = "";
            secNameBox.Text = "";
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameNewBox.Text == "")
            {
                errorText.Text = "Enter Name!";
                return;
            }
            if (surnameNewBox.Text == "")
            {
                errorText.Text = "Enter Surname!";
                return;
            }
            int index = ListEditors.Last().ID_Editor + 1;
            DataAccess db = new DataAccess();
            db.InsertEditor(index, nameNewBox.Text, surnameNewBox.Text, secNameNewBox.Text);
            UpdateListEditors();
            nameNewBox.Text = "";
            surnameNewBox.Text = "";
            secNameNewBox.Text = "";
        }

        private void ListBoxEditors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading) return;
            Editor ed = (Editor)ListBoxEditors.SelectedItem;
            nameBox.Text = ed.Name;
            surnameBox.Text = ed.Surname;
            secNameBox.Text = ed.Sec_Name;
            enableControls();
        }

        private void UpdateListEditors()
        {
            DataAccess db = new DataAccess();
            ListEditors = db.GetEditors();
            ListBoxEditors.ItemsSource = ListEditors;
            ListBoxEditors.DisplayMemberPath = "FullInfo";
            disableControls();
        }

        private void enableControls()
        {
            nameBox.IsReadOnly = false;
            surnameBox.IsReadOnly = false;
            secNameBox.IsReadOnly = false;
        }

        private void disableControls()
        {
            nameBox.Text = "";
            surnameBox.Text = "";
            secNameBox.Text = "";
            nameBox.IsReadOnly = true;
            surnameBox.IsReadOnly = true;
            secNameBox.IsReadOnly = true;
        }
    }
}
