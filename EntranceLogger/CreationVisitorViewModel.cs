using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace EntranceLogger
{
    public class CreationVisitorViewModel : BindableBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string DocumentType { get; set; }
        public string DocumentSeries { get; set; }
        public string DocumentNumber { get; set; }
        public string StatusText { get; set; }
        public string VisitTarget { get; set; }
        public EmployeeViewModel SelectedEmployee { get; set; } = null;
        public SolidColorBrush StatusColor { get; set; } = Brushes.Green;
        public List<EmployeeViewModel> Employees { get; set; }
        public ICommand AddCommand { get; set; }
        public Action CloseAction { get; set; }
        private EntranceLoggerEntities source;

        public CreationVisitorViewModel(EntranceLoggerEntities dataSource)
        {
            source = dataSource;
            Employees = new List<EmployeeViewModel>();
            foreach (var item in dataSource.Employees)
            {
                if (item.IsActive)
                {
                    Employees.Add(new EmployeeViewModel(item));
                }
            }
            AddCommand = new RelayCommand(OnAddCommand);
        }

        private bool Validate()
        {
            if (String.IsNullOrEmpty(Name) ||
                String.IsNullOrEmpty(Surname) ||
                String.IsNullOrEmpty(Patronymic) ||
                String.IsNullOrEmpty(DocumentType) ||
                String.IsNullOrEmpty(DocumentSeries) ||
                String.IsNullOrEmpty(DocumentNumber) ||
                SelectedEmployee == null)
                return false;
            return true;
        }

        private void OnAddCommand(object arg)
        {
            if (Validate())
                CloseAction();
            else
                System.Windows.MessageBox.Show("Введите все необходимые данные");
        }

    }
}
