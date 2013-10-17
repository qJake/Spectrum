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
using SharpHue;

namespace Spectrum.Controls
{
    /// <summary>
    /// Interaction logic for LightStatus.xaml
    /// </summary>
    public partial class LightStatus : UserControl
    {
        protected Light light { get; set; }

        public LightStatus(Light l)
        {
            InitializeComponent();

            light = l;

            DataContext = light;
        }
    }
}
