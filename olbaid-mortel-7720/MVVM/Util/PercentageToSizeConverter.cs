using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace olbaid_mortel_7720.MVVM.Utils
{
  /// <summary>
  ///   A Converter to convert a percentage to a size to be used in XAML.
  /// </summary>
  public class PercentageToSizeConverter : IMultiValueConverter
  {
    /// <summary>
    ///   Convert a percentage to a size.
    /// </summary>
    /// <param name="values">
    ///   values[0] = percentage
    ///   values[1] = size of the parent
    /// </param>
    /// <returns>a size</returns>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values[0] is IConvertible && values[1] is IConvertible)
      {
        double percentage = System.Convert.ToDouble(values[1]) / 100;
        if (percentage <= 0) return 0.0;
        double fullSize = System.Convert.ToDouble(values[0]);
        return fullSize * percentage;
      }

      return 100;
    }

    /// <summary>
    ///   Convert a size to a percentage.
    /// </summary>
    /// <exception cref="NotImplementedException">Otherwise there would be a missing value to perform a calculation.</exception>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException("This converter can only be used OneWay.");
    }
  }
}