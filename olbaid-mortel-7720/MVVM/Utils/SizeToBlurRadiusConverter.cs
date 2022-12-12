using System;
using System.Diagnostics;
using System.Windows.Data;

namespace olbaid_mortel_7720.MVVM.Utils
{
  /// <summary>
  ///   A Converter to convert a size to a blur radius to be used in XAML.
  /// </summary>
  public class SizeToBlurRadiusConverter : IValueConverter
  {
    /// <summary>
    ///   Convert a size to a blur radius.
    /// </summary>
    /// <param name="value">the size to be converted</param>
    /// <param name="parameter">the divider to be used</param>
    /// <returns>a blur radius</returns>
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      double size = System.Convert.ToDouble(value);
      double param = parameter as double? ?? 50;
      return size / param;
    }

    /// <summary>
    ///   Convert a blur radius to a size.
    /// </summary>
    /// <exception>We will never need that. (At least my opinion as a fortune teller)</exception>
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException("Not needed for conversion!");
    }
  }
}