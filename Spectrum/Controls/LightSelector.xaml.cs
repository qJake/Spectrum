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

namespace Spectrum
{
    /// <summary>
    /// Interaction logic for LightSelector.xaml
    /// </summary>
    public partial class LightSelector : UserControl
    {
        private LightCollection lights { get; set; }

        public List<Light> SelectedLights { get; protected set; }

        public event Action<object, List<Light>> SelectedLightsChanged;

        public LightSelector()
        {
            InitializeComponent();
            SelectedLights = new List<Light>();
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            foreach (LightStatus ctl in LightContainer.Children)
            {
                ctl.LightSelected -= LightSelected;
            }

            if (lights == null)
            {
                lights = new LightCollection();
            }
            else
            {
                lights.Refresh();
            }

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
            SelectedLights.Clear();

            foreach (LightStatus ctl in LightContainer.Children)
            {
                if (ctl.Selected.IsChecked.HasValue && ctl.Selected.IsChecked.Value)
                {
                    SelectedLights.Add(ctl.Light);
                }
            }

            if (SelectedLightsChanged != null)
            {
                SelectedLightsChanged(this, SelectedLights);
            }
        }
    }
}
