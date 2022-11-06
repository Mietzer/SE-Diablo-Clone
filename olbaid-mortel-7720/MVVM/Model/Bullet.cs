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

      //Rotate Shot by its vector
      double angle = Math.Atan2(direction.Y , direction.X) * 180/Math.PI;
      RotateTransform rotation =  new RotateTransform(angle);
      rotation.CenterX = width/2;
      rotation.CenterY = height/2;
      Rectangle.RenderTransform = rotation;
    }
  }
}
