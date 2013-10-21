using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpHue;

namespace Spectrum
{
    /// <summary>
    /// A type converter for visibility and boolean values.
    /// </summary></Border>
    public class LSBColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is LightStateBuilder))
            {
                throw new InvalidOperationException();
            }

            var lsb = value as LightStateBuilder;
            var tempLight = new Light();
            tempLight.RefreshState(lsb.GetJson());
            var c = tempLight.State.Color;
            return new Color() { A = 255, R = c.R, G = c.G, B = c.B };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
