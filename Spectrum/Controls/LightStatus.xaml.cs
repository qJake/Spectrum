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
        public event Action<object> LightSelected;

        private Light _Light;
        public Light Light
        {
            get
            {
                return _Light;
            }
            protected set
            {
                _Light = value;
                _Light.StateChanged += StateChanged;
            }
        }

        public LightStatus(Light l)
        {
            InitializeComponent();

            Light = l;
            DataContext = Light;

            // Set on/off state
            if (!Light.State.IsReachable)
            {
                UnreachableDot.Visibility = Visibility.Visible;
            }
            else
            {
                if (Light.State.IsOn)
                {
                    var color = Light.State.Color;
                    OnDot.Visibility = Visibility.Visible;
                    OnDot.Fill = new SolidColorBrush(new Color() { A = 255, R = color.R, G = color.G, B = color.B });
                }
                else
                {
                    OffDot.Visibility = Visibility.Visible;
                }
            }
        }

        private void Selected_Checked(object sender, RoutedEventArgs e)
        {
            if (LightSelected != null)
            {
                LightSelected(this);
            }
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Selected.IsChecked = !Selected.IsChecked;
        }

        public void StateChanged(object sender, LightState state)
        {
            var c = state.Color;
            OnDot.Fill = new SolidColorBrush(new Color() { A = 255, R = c.R, G = c.G, B = c.B });
        }
    }
}
