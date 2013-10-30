using System;
using System.Windows.Data;
using Ozeki.VoIP;

namespace secret_sensa.GUI.Converters
{
    class MessageSummaryToBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IPhoneLine line = value as IPhoneLine;
            if (line == null)
                return false;

            if (line.MessageSummary == null)
                return false;

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
