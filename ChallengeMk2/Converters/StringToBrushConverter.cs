using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChallengeMk2.Converters
{
    class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hash = value.GetHashCode();
            var r = (hash & 0xFF0000) >> 16;
            var g = (hash & 0x00FF00) >> 8;
            var b = hash & 0x0000FF;

            var myColor = Color.FromRgb(r, g, b);
            return new SolidColorBrush(myColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException($"{nameof(NullStringConverter)} cannot be used on TWO-WAY bindings.");
    }
}
