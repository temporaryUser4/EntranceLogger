using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EntranceLogger
{
    public class VisitorViewModel : BindableBase
    {
        private Visit linkedVisit;
        private Visitor visitor;
        private ConfirmExitWindow confirm = null;
        public int Id { get { return visitor.Id; } set { visitor.Id = value; } }
        public string Name { get { return visitor.Name; } set { visitor.Name = value; } }
        public string Surname { get { return visitor.Surname; } set { visitor.Surname = value; } }
        public string Patronymic { get { return visitor.Patronymic; } set { visitor.Patronymic = value; } }
        public int DocumentTypeId { get { return visitor.DocumentTypeId; } set { visitor.DocumentTypeId = value; } }
        public string DocumentSeries { get { return visitor.DocumentSeries; } set { visitor.DocumentSeries = value; } }
        public int DocumentNumber { get { return visitor.DocumentNumber; } set { visitor.DocumentNumber = value; } }
        public virtual DocumentType DocumentType { get { return visitor.DocumentType; } set { visitor.DocumentType = value; } }
        public virtual ICollection<Visit> Visits { get { return visitor.Visits; } set { visitor.Visits = value; } }
        public string DocumentTypeTitle { get { return DocumentType.Title; } }
        public ICommand ExitButtonCommand { get; set; }
        public Action<VisitorViewModel> SaveChangesCommand { get; set; }

        public VisitorViewModel(Visitor visitor, Visit visit)
        {
            this.visitor = visitor;
            linkedVisit = visit;
            ExitButtonCommand = new RelayCommand(OnExitButtonCommand);
        }

        private void OnExitButtonCommand(object arguemnt)
        {
            if (confirm == null)
                confirm = new ConfirmExitWindow();
            if (confirm.ShowDialog() == true)
            {
                linkedVisit.ExitDate = DateTime.Now;
                var vm = (ConfirmExitViewModel)(confirm.DataContext);
                if (vm.Remark != null)
                    linkedVisit.Remark = vm.Remark;
                SaveChangesCommand?.Invoke(this);
            }
            else
            {

            }
            confirm = null;
        }

    }
}
