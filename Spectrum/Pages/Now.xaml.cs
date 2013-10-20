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

        public Now()
        {
            InitializeComponent();
            lights = new LightCollection();
            Refresh();
        }

        public void Refresh()
        {
            lights.Refresh();

            LightContainer.Children.Clear();

            foreach (var light in lights)
            {
                LightContainer.Children.Add(new LightStatus(light));
            }
        }

        private void Saturation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition((IInputElement)sender);

            var hue = MathEx.TranslateValue((int)mousePos.X, 0, (int)((FrameworkElement)sender).ActualWidth, 0, 255);
            var sat = 255 - MathEx.TranslateValue((int)mousePos.Y, 0, (int)((FrameworkElement)sender).ActualHeight, 0, 255);

            HSBColor color = new HSBColor(hue, sat, 255);
            BriTop.Color = new Color() { A = 255, R = color.Color.R, G = color.Color.G, B = color.Color.B };

            new LightStateBuilder()
                .For(lights[5])
                .Hue((ushort)(color.H * 255))
                .Saturation((byte)color.S)
                .Brightness((byte)color.B)
                .TurnOn()
                .Apply();

            Refresh();
        }

        private void SelectorCT_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition((IInputElement)sender);

            var visualct = MathEx.TranslateValue((int)mousePos.Y, 0, (int)((FrameworkElement)sender).ActualHeight, 2000, 11500);
            var ct = MathEx.TranslateValue((int)mousePos.Y, 0, (int)((FrameworkElement)sender).ActualHeight, 137, 500, true);

            HSBColor color = new HSBColor(visualct);
            color.B = 255;
            BriTop.Color = new Color() { A = 255, R = color.Color.R, G = color.Color.G, B = color.Color.B };

            new LightStateBuilder()
                .For(lights[5])
                .ColorTemperature((ushort)ct)
                .TurnOn()
                .Apply();

            Refresh();
        }
    }
}
