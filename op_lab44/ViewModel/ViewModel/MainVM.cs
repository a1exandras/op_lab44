using System;
using op_lab44.Core;

namespace op_lab44.ViewModel.ViewModel
{
    internal class MainVM : CurrentObj
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand AppendViewCommand { get; set; }
        public RelayCommand EditorsViewCommand { get; set; }
        public RelayCommand PrintersViewCommand { get; set; }
        public RelayCommand ProducersViewCommand { get; set; }
        public RelayCommand DepartmentsViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand ManufactureViewCommand { get; set; }
        public RelayCommand ConsignmentViewCommand { get; set; }
        public RelayCommand PersonalTaskViewCommand { get; set; }
        public RelayCommand ReportViewCommand { get; set; }
        public HomeVM homevm { get; set; }
        public AppendVM appendvm { get; set; }
        public EditorsVM editorvm { get; set; }
        public PrinterVM printervm { get; set; }
        public ProducerVM producervm { get; set; }
        public DepertmentVM departmentvm { get; set; }
        public ProductVM productvm { get; set; }
        public ManufactureVM manufacturevm { get; set; }
        public ConsignmentVM consignmentvm { get; set; }
        public PersonalTaskVM personaltaskvm { get; set; }
        public ReportVM reportvm { get; set; }

        private Object _currentView;

        public Object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainVM()
        {
            homevm = new HomeVM();
            appendvm = new AppendVM();
            editorvm = new EditorsVM();
            printervm = new PrinterVM();
            producervm = new ProducerVM();
            departmentvm = new DepertmentVM();
            productvm = new ProductVM();
            manufacturevm = new ManufactureVM();
            consignmentvm = new ConsignmentVM();
            personaltaskvm = new PersonalTaskVM();
            reportvm = new ReportVM();

            CurrentView = homevm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = homevm;
            });

            AppendViewCommand = new RelayCommand(o =>
            {
                CurrentView = appendvm;
            });

            EditorsViewCommand = new RelayCommand(o =>
            {
                CurrentView = editorvm;
            });

            PrintersViewCommand = new RelayCommand(o =>
            {
                CurrentView = printervm;
            });

            ProducersViewCommand = new RelayCommand(o =>
            {
                CurrentView = producervm;
            });

            DepartmentsViewCommand = new RelayCommand(o =>
            {
                CurrentView = departmentvm;
            });

            ProductsViewCommand = new RelayCommand(o =>
            {
                CurrentView = productvm;
            });

            ManufactureViewCommand = new RelayCommand(o =>
            {
                CurrentView = manufacturevm;
            });

            ConsignmentViewCommand = new RelayCommand(o =>
            {
                CurrentView = consignmentvm;
            });

            PersonalTaskViewCommand = new RelayCommand(o =>
            {
                CurrentView = personaltaskvm;
            });

            ReportViewCommand = new RelayCommand(o =>
            {
                CurrentView = reportvm;
            });
        }
    }
}
