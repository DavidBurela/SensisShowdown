using System;
using SensisShowdown.Models;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace SensisShowdown.Helpers.Converters
{
    class ResultColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((value == null) || (value is bool == false))
                return null;

            var isResult1 = (bool)value;
            if(isResult1)
                return new SolidColorBrush(Colors.Red);
            else
                return new SolidColorBrush(Colors.Blue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
