using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.Model
{
  public class Bullet : NotifyObject
  {
    #region Properties
    public Rectangle Rectangle { get; set; }
    public Vector Direction { get; set; }
    #endregion Properties
    public Bullet(int height, int width, Vector direction, Brush brush, string name)
    {
      Rectangle = new Rectangle();
      Rectangle.Height = height;
      Rectangle.Width = width;
      Rectangle.Fill = brush;
      Rectangle.Name = name;
      Direction = direction;
    }


  }
}
