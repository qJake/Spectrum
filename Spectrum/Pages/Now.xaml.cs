using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using SharpHue;
using Spectrum.Controls;

namespace Spectrum.Pages
{
    /// <summary>
    /// Interaction logic for Now.xaml
    /// </summary>
    public partial class Now : UserControl
    {
        public Now()
        {
            InitializeComponent();
            StatePicker.StatePicked += (_, lsb) =>
            {
                foreach (var l in LightSelector.SelectedLights)
                {
                    l.SetState(lsb);
                }
            };
        }
    }
}
