using System;
using System.Globalization;
using Xamarin.Forms;

namespace ChallengeMk2.Converters
{
    public class NullStringConverter : IValueConverter
    {
        readonly string noData = "N/A";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sourceString = (value ?? "").ToString();

            if (string.IsNullOrWhiteSpace(sourceString))
            {
                return noData;
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{nameof(NullStringConverter)} cannot be used on TWO-WAY bindings.");
        }
    }
}
