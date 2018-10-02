using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace EntranceLogger
{
    public class MainWindowViewModel : BindableBase
    {
        public ICommand AddVisitorCommand { get; set; }
        public ObservableCollection<VisitorViewModel> ActiveVisitors { get; set; }

        private EntranceLoggerEntities entities = new EntranceLoggerEntities();

        public MainWindowViewModel()
        {
            ActiveVisitors = new ObservableCollection<VisitorViewModel>();
            AddVisitorCommand = new RelayCommand(OnAddVisitorCommand);
            LoadActiveVisitors();
        }

        private void LoadActiveVisitors()
        {
            var visits = entities.Visits;
            var visitors = entities.Visitors;
            foreach (var item in visits)
            {
                if (item.ExitDate == null)
                {
                    ActiveVisitors.Add(new VisitorViewModel(item.Visitor, item) { SaveChangesCommand = OnSaveChangesCommand });
                }
            }
        }

        private void OnSaveChangesCommand(VisitorViewModel deleted)
        {
            entities.SaveChanges();
            ActiveVisitors.Remove(deleted);
        }

        private void OnAddVisitorCommand(object argument)
        {
            CreationVisitorWindow w = new CreationVisitorWindow(entities);
            if (w.ShowDialog() == true)
            {
                var m = (CreationVisitorViewModel)w.DataContext;
                Visitor vis = null;
                foreach (var item in entities.Visitors)
                    if (item.Name == m.Name && item.Surname == m.Surname
                        && item.Patronymic == m.Patronymic)
                        vis = item;
                int vid = 0;
                if (vis == null)
                {
                    var docty = GetDocumentType(m.DocumentType);
                    if (docty == null)
                    {
                        docty = new DocumentType
                        {
                            Title = m.DocumentType
                        };
                        entities.DocumentTypes.Add(docty);
                        // 
                    }
                    vis = new Visitor
                    {
                        Name = m.Name,
                        Surname = m.Surname,
                        Patronymic = m.Patronymic,
                        DocumentNumber = Int32.Parse(m.DocumentNumber),
                        DocumentSeries = m.DocumentSeries,
                        DocumentTypeId = docty.Id,
                        DocumentType = docty
                    };
                    entities.Visitors.Add(vis);
                }
                else
                {
                    vid = vis.Id;
                }
                Visit visit = new Visit
                {
                    Visitor = vis,
                    VisitTarget = m.VisitTarget,
                    EntranceDate = DateTime.Now,
                    ExitDate = null,
                    Employee = m.SelectedEmployee.Employee,
                    EmployeeId = m.SelectedEmployee.Employee.Id
                };
                ActiveVisitors.Add(new VisitorViewModel(vis, visit));
                entities.Visits.Add(visit);
                entities.SaveChanges();
            }
        }

        private DocumentType GetDocumentType(string doc)
        {
            foreach (var item in entities.DocumentTypes)
                if (doc == item.Title)
                    return item;
            return null;
        }
    }
}
