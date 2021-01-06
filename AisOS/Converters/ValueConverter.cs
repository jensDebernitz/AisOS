using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;

namespace AisOS.Converters
{
    class ValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }
    }

    //public class TextInputToVisibilityConverter : IMultiValueConverter
    //{
    //    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        // Always test MultiValueConverter inputs for non-null
    //        // (to avoid crash bugs for views in the designer)
    //        if (values[0] is bool && values[1] is bool)
    //        {
    //            bool hasText = !(bool)values[0];
    //            bool hasFocus = (bool)values[1];
                
    //            if (hasFocus || hasText)
    //                return Visibility.Collapsed;
    //        }

    //        return Visibility.Visible;
    //    }


    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
