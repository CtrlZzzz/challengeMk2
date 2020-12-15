using System;
using System.Globalization;
using Xamarin.Forms;

namespace ChallengeMk2.Converters
{
    class FirstCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Substring(0, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException($"{nameof(NullStringConverter)} cannot be used on TWO-WAY bindings.");
    }
}
