using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EntranceLogger
{
    public class ConfirmExitViewModel : BindableBase
    {
        private ConfirmExitWindow view;
        public ICommand ConfirmButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public string Remark { get; set; }
        
        public ConfirmExitViewModel(ConfirmExitWindow view)
        {
            this.view = view;
            ConfirmButtonCommand = new RelayCommand(OnConfirmButtonCommand);
            CancelButtonCommand = new RelayCommand(OnCancelButtonCommand);
        }

        private void OnConfirmButtonCommand(object arg)
        {
            view.DialogResult = true;
            view.Close();
        }

        private void OnCancelButtonCommand(object arg)
        {
            view.DialogResult = false;
            view.Close();
        }
    }
}
