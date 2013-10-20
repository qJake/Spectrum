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

        private Style SelectorActive;
        private Style SelectorInactive;

        public Now()
        {
            InitializeComponent();
            lights = new LightCollection();
            Refresh();

            SelectorActive = FindResource("Text-Selector-Active") as Style;
            SelectorInactive = FindResource("Text-Selector-Inactive") as Style;

            SetMode(true);
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

        private void SelectorRGB_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition((IInputElement)sender);

            var hue = MathEx.TranslateValue((int)mousePos.X, 0, (int)((FrameworkElement)sender).ActualWidth, 0, 255);
            var sat = 255 - MathEx.TranslateValue((int)mousePos.Y, 0, (int)((FrameworkElement)sender).ActualHeight, 0, 255);

            HSBColor color = new HSBColor(hue, sat, 255);
            BriTop.Color = new Color() { A = 255, R = color.Color.R, G = color.Color.G, B = color.Color.B };

            new LightStateBuilder()
                .For(selectedLights)
                .Hue((ushort)(color.H * 255))
                .Saturation((byte)color.S)
                .Brightness((byte)color.B)
                .TurnOn()
                .Apply();
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
                .For(selectedLights)
                .ColorTemperature((ushort)ct)
                .TurnOn()
                .Apply();
        }

        // TODO: Change this to an Enum or something like that
        private void SetMode(bool isRgb)
        {
            if (isRgb)
            {
                SelectorCT.Visibility = Visibility.Collapsed;
                SelectorRGB.Visibility = Visibility.Visible;

                OptionRGB.Style = SelectorActive;
                OptionCT.Style = SelectorInactive;
            }
            else
            {
                SelectorCT.Visibility = Visibility.Visible;
                SelectorRGB.Visibility = Visibility.Collapsed;

                OptionRGB.Style = SelectorInactive;
                OptionCT.Style = SelectorActive;
            }
        }

        private void OptionRGB_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetMode(true);
        }

        private void OptionCT_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetMode(false);
        }
    }
}
