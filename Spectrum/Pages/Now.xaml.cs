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
        private LightCollection lights { get; set; }

        private List<Light> selectedLights = new List<Light>();

        public Now()
        {
            InitializeComponent();
            lights = new LightCollection();

            StatePicker.StatePicked += (_, lsb) =>
            {
                foreach (var l in selectedLights)
                {
                    l.SetState(lsb);
                }
            };

            Refresh();
        }

        public void Refresh()
        {
            foreach (LightStatus ctl in LightContainer.Children)
            {
                ctl.LightSelected -= LightSelected;
            }

            lights.Refresh();

            LightContainer.Children.Clear();

            foreach (var light in lights)
            {
                var status = new LightStatus(light);
                status.LightSelected += LightSelected;
                LightContainer.Children.Add(status);
            }
        }

        private void LightSelected(object sender)
        {
            selectedLights.Clear();

            foreach (LightStatus ctl in LightContainer.Children)
            {
                if (ctl.Selected.IsChecked.HasValue && ctl.Selected.IsChecked.Value)
                {
                    selectedLights.Add(ctl.Light);
                }
            }
        }
    }
}
