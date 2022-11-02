using System;
using System.Globalization;
using System.Windows.Data;

namespace olbaid_mortel_7720.MVVN.Utils;

public class PercentageToSizeConverter : IValueConverter
{
   
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double percentage = System.Convert.ToDouble(value) / 100;
        double fullSize = System.Convert.ToDouble(parameter);
        return fullSize * percentage;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double size = System.Convert.ToDouble(value);
        double fullSize = System.Convert.ToDouble(parameter);
        return size / fullSize * 100;
    }
}