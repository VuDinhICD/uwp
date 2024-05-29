using System;
using Windows.UI.Xaml.Data;

namespace universal_windows_platform_cs.Helpers
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var d = value as DateTime?;
            if (!d.HasValue) return value;
            return d.Value.ToString("yyyy/MM/dd");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
