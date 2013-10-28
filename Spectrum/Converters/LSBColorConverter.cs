using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpHue;

namespace Spectrum
{
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
            if (tempLight.State.IsOn)
            {
                return new SolidColorBrush(new Color() { A = 255, R = c.R, G = c.G, B = c.B });
            }
            else
            {
                return new SolidColorBrush(Colors.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    public class LSBBorderColorConverter : IValueConverter
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
            if (tempLight.State.IsOn)
            {
                return new SolidColorBrush(Colors.Black);
            }
            else
            {
                return new SolidColorBrush(Colors.LightGray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
