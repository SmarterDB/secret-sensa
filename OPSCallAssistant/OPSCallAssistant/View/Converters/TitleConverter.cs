using System;
using System.Globalization;
using System.Windows.Data;

namespace OPSCallAssistant.View.Converters
{
    class TitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = (string)value;

            return string.Format("Ozeki call assistant - {0}", name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
