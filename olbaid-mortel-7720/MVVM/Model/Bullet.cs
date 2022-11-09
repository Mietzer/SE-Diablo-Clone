using olbaid_mortel_7720.Helper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.Model
{
  public class Bullet : NotifyObject
  {
    #region Properties
    public Rectangle Rectangle { get; set; }
    public Vector Direction { get; set; }
    public Rect Hitbox { get; set; }
    #endregion Properties
    
    public Bullet(int height, int width, Vector direction, Brush brush, string name)
    {
      Rectangle = new Rectangle();
      Rectangle.Height = height;
      Rectangle.Width = width;
      Rectangle.Fill = brush;
      Rectangle.Name = name;
      Direction = direction;
      Hitbox = new Rect(0, 0, width, height);
    }

    #region Methods
    public void Show(Canvas canvas, double x, double y)
    {
      // Adjust the hitbox
      Hitbox = new Rect(x, y, Rectangle.Width, Rectangle.Height);
      
      //Add to View
      canvas.Children.Add(Rectangle);
      
      // Apply the rotation
      double angle = Math.Atan2(Direction.Y, Direction.X) * 180 / Math.PI;
      Rectangle.RenderTransform = new RotateTransform(angle, Rectangle.Width / 2, Rectangle.Height / 2);

      //Set Position
      Canvas.SetLeft(Rectangle, x + Direction.X * 50);
      Canvas.SetTop(Rectangle, y + Direction.Y * 50);
    }
    
    public void Move(double velocity)
    {
      // Move the bullet
      Canvas.SetLeft(Rectangle, Canvas.GetLeft(Rectangle) + Direction.X * velocity);
      Canvas.SetTop(Rectangle, Canvas.GetTop(Rectangle) + Direction.Y * velocity);
      
      // Adjust the hitbox
      Hitbox = new Rect(Canvas.GetLeft(Rectangle), Canvas.GetTop(Rectangle), Rectangle.Width, Rectangle.Height);
    }
    #endregion Methods
  }
}
