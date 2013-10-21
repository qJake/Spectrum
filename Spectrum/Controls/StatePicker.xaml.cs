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

namespace Spectrum
{
    /// <summary>
    /// Interaction logic for StatePicker.xaml
    /// </summary>
    public partial class StatePicker : UserControl
    {
        public event Action<object, LightStateBuilder> StatePicked;

        public LightStateBuilder SelectedState { get; protected set; }

        private Style SelectorActive;
        private Style SelectorInactive;

        public StatePicker()
        {
            InitializeComponent();

            SelectedState = null;

            SelectorActive = FindResource("Text-Selector-Active") as Style;
            SelectorInactive = FindResource("Text-Selector-Inactive") as Style;

            SetMode(PickerState.RGB);
        }

        private void SelectorRGB_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition((IInputElement)sender);

            var hue = MathEx.TranslateValue((int)mousePos.X, 0, (int)((FrameworkElement)sender).ActualWidth, 0, 255);
            var sat = 255 - MathEx.TranslateValue((int)mousePos.Y, 0, (int)((FrameworkElement)sender).ActualHeight, 0, 255);

            HSBColor color = new HSBColor(hue, sat, 255);
            BriTop.Color = new Color() { A = 255, R = color.Color.R, G = color.Color.G, B = color.Color.B };

            var lsb = new LightStateBuilder()
                          .TurnOn()
                          .Hue((ushort)(color.H * 255))
                          .Saturation((byte)color.S)
                          .Brightness((byte)color.B);

            OnStatePicked(lsb);
        }

        private void SelectorCT_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition((IInputElement)sender);

            var visualct = MathEx.TranslateValue((int)mousePos.Y, 0, (int)((FrameworkElement)sender).ActualHeight, 2000, 11500);
            var ct = MathEx.TranslateValue((int)mousePos.Y, 0, (int)((FrameworkElement)sender).ActualHeight, 137, 500, true);

            HSBColor color = new HSBColor(visualct);
            color.B = 255;
            BriTop.Color = new Color() { A = 255, R = color.Color.R, G = color.Color.G, B = color.Color.B };

            var lsb = new LightStateBuilder()
                          .TurnOn()
                          .ColorTemperature((ushort)ct);

            OnStatePicked(lsb);
        }

        // TODO: Change this to an Enum or something like that
        private void SetMode(PickerState state)
        {
            switch (state)
            {
                case PickerState.RGB:
                    SelectorCT.Visibility = Visibility.Collapsed;
                    SelectorRGB.Visibility = Visibility.Visible;
                    Brightness.Visibility = Visibility.Visible;

                    OptionRGB.Style = SelectorActive;
                    OptionCT.Style = SelectorInactive;
                    OptionOff.Style = SelectorInactive;
                    break;

                case PickerState.CT:
                    SelectorCT.Visibility = Visibility.Visible;
                    SelectorRGB.Visibility = Visibility.Collapsed;
                    Brightness.Visibility = Visibility.Visible;

                    OptionRGB.Style = SelectorInactive;
                    OptionCT.Style = SelectorActive;
                    OptionOff.Style = SelectorInactive;
                    break;

                case PickerState.Off:
                    SelectorCT.Visibility = Visibility.Collapsed;
                    SelectorRGB.Visibility = Visibility.Collapsed;
                    Brightness.Visibility = Visibility.Collapsed;

                    OptionRGB.Style = SelectorInactive;
                    OptionCT.Style = SelectorInactive;
                    OptionOff.Style = SelectorActive;
                    break;
            }
        }

        private void OptionRGB_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetMode(PickerState.RGB);
        }

        private void OptionCT_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetMode(PickerState.CT);
        }

        private void OptionOff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetMode(PickerState.Off);
            OnStatePicked(new LightStateBuilder().TurnOff());
        }

        private void OnStatePicked(LightStateBuilder lsb)
        {
            SelectedState = lsb;
            if (StatePicked != null)
            {
                StatePicked(this, lsb);
            }
        }
    }
}
