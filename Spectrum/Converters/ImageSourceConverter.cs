using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Spectrum
{
    /// <summary>
    /// A type converter for visibility and boolean values.
    /// </summary>
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                BitmapImage img = new BitmapImage();

                string path = value.ToString();

                img.BeginInit();
                img.UriSource = new Uri("pack://application:,,,/" + path, UriKind.Absolute);
                img.EndInit();

                return img;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
