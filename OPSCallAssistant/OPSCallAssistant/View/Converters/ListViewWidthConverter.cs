using System;
using System.Globalization;
using System.Windows.Data;

namespace OPSCallAssistant.View.Converters
{
    class ListViewWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Double) value) - 30;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
