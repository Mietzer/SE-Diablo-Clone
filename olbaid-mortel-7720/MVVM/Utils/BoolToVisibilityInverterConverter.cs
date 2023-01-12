using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace olbaid_mortel_7720.MVVM.Utils
{
  /// <summary>
  ///   A Converter to convert a bool to Visibility
  /// </summary>
  public class BoolToVisibilityInverterConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if ((bool)value)
        return Visibility.Collapsed;
      else
        return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException("This converter can only be used OneWay.");
    }
  }
}