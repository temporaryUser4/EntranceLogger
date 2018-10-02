using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EntranceLogger
{
    public partial class CreationVisitorWindow : Window
    {
        private static readonly Regex numericRegex = new Regex("[0-9]+");
        private EntranceLoggerEntities source;

        public CreationVisitorWindow(EntranceLoggerEntities dataSource)
        {
            InitializeComponent();
            DataContext = new CreationVisitorViewModel(dataSource) { CloseAction = OnClose };
            source = dataSource;
        }

        private static bool IsNumericAllowed(string text)
        {
            return numericRegex.IsMatch(text);
        }

        private void NumericPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumericAllowed(e.Text);
        }

        private void OnClose()
        {
            DialogResult = true;
            Close();
        }
    }
}
