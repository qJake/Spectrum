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

            HSBColor hsb;
            switch (light.State.CurrentColorMode.ToLower())
            {
                case "ct":
                    hsb = new HSBColor(MathEx.TranslateValue(light.State.ColorTemperature, 137, 500, 2000, 11500, true));
                    break;
                default:
                    hsb = new HSBColor(l.State.Hue / 255, l.State.Saturation, l.State.Brightness);
                    break;
            }

            var color = hsb.Color;
            OnDot.Fill = new SolidColorBrush(new Color() { A = 255, R = color.R, G = color.G, B = color.B });
        }
    }
}
