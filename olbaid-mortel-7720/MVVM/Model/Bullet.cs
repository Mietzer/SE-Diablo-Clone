﻿using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
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

    private bool hasHit;
    private Vector vector;

    public bool HasHit
    {
      get { return hasHit; }
      set
      {
        hasHit = value;
        Rectangle.Height = 0;
        Rectangle.Width = 0;
      }
    }

    #endregion Properties

    #region Constructor
    public Bullet(Vector direction, int height, int width, Brush brush, string name)
    {
      Rectangle = new Rectangle();
      Rectangle.Height = height;
      Rectangle.Width = width;
      Rectangle.Fill = brush;
      Rectangle.Name = name;
      Direction = direction;
      Hitbox = new Rect(0, 0, width, height);
    }
    public Bullet(Vector vector, Munition munition) : this(vector, munition.Height, munition.Width, munition.BulletImage, munition.Name)
    { }
    #endregion Constructor

    #region Methods
    /// <summary>
    /// Shows the bullet on the canvas
    /// </summary>
    /// <param name="canvas">Canvas to be shown on</param>
    /// <param name="x">X Coordinate</param>
    /// <param name="y">Y Coordinate</param>
    public void Show(Canvas canvas, double x, double y)
    {
      //Add to View
      canvas.Children.Add(Rectangle);

      // Apply the rotation
      double angle = Math.Atan2(Direction.Y, Direction.X) * 180 / Math.PI;
      Rectangle.RenderTransform = new RotateTransform(angle, Rectangle.Width / 2, Rectangle.Height / 2);

      //Set Position
      Canvas.SetLeft(Rectangle, x);
      Canvas.SetTop(Rectangle, y);

      // Adjust the hitbox
      Hitbox = new Rect(Canvas.GetLeft(Rectangle), Canvas.GetTop(Rectangle), Rectangle.Width, Rectangle.Height);
    }

    /// <summary>
    /// Moving the bullet on the canvas
    /// </summary>
    /// <param name="velocity">Velocity to move with</param>
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
