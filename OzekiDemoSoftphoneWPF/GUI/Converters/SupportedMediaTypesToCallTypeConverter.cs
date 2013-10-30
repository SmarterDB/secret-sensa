using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Ozeki.VoIP.Media;

namespace OzekiDemoSoftphoneWPF.GUI.Converters
{
    class SupportedMediaTypesToCallTypeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable<VoIPMediaType> types = value as IEnumerable<VoIPMediaType>;

            if (types.Contains(VoIPMediaType.Audio) && types.Contains(VoIPMediaType.Video))
                return CallType.AudioVideo;

            if (!types.Contains(VoIPMediaType.Audio) && types.Contains(VoIPMediaType.Video))
                return CallType.Video;
            
            return CallType.Audio;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion

    }
}
