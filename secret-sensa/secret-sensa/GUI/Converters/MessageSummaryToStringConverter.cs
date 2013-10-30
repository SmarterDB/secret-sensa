using System;
using System.Windows.Data;
using Ozeki.VoIP;

namespace secret_sensa.GUI.Converters
{
    /// <summary>
    /// Converts an IPhoneLine object to a string.
    /// </summary>
    class MessageSummaryToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IPhoneLine line = value as IPhoneLine;
            if (line == null)
                return "No lines selected";

            if (line.MessageSummary == null)
                return "No message info";

            return "View messages";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
