using olbaid_mortel_7720.Engine;
using System;
using System.Globalization;

namespace olbaid_mortel_7720.MVVM.Utils
{
  internal class EntityToPointMultiConverter : System.Windows.Data.IMultiValueConverter
  {

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      Entity enitity = (Entity)values[0];
      if (enitity != null)
        return new System.Windows.Point(enitity.XCoord + enitity.Width / 2, enitity.YCoord + enitity.Height / 2);
      else
        return new System.Windows.Point(0, 0);

    }


    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
