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
using Spectrum.Windows;

namespace Spectrum.Pages
{
    /// <summary>
    /// Interaction logic for Automate.xaml
    /// </summary>
    public partial class Automate : UserControl
    {
        public List<AutomationAction> Actions;

        public Automate()
        {
            InitializeComponent();
        }

        private void AddTrigger_Click(object sender, RoutedEventArgs e)
        {
            var definitionWindow = new AddAutomationDefinition();
            var res = definitionWindow.ShowDialog();

            if (res.HasValue && res.Value)
            {
                // Load automation definition properties
            }
        }
    }
}
