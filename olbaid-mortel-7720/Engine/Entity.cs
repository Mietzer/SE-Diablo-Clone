﻿using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace olbaid_mortel_7720.Engine
{
  public abstract class Entity : NotifyObject
  {
    #region Properties

    #region Movement
    private int xCoord;
    public int XCoord
    {
      get { return xCoord; }
      private set
      {
        if (value == xCoord) return;
        xCoord = value;
        OnPropertyChanged(nameof(XCoord));
      }
    }

    private int yCoord;
    public int YCoord
    {
      get { return yCoord; }
      private set
      {
        if (value == yCoord) return;
        yCoord = value;
        OnPropertyChanged(nameof(YCoord));
      }
    }

    public int Height { get; private set; }
    public int Width { get; private set; }
    public Direction Direction { get; set; }
    public bool IsMoving { get; protected set; }

    private int stepLength;
    public int StepLength
    {
      get { return stepLength; }
      private set
      {
        stepLength = value;
        OnPropertyChanged(nameof(stepLength));
      }
    }
    #endregion Movement

    public ObservableCollection<Bullet> Bullets;

    private Rect hitbox;
    public Rect Hitbox
    {
      get { return hitbox; }
      set
      {
        if (value == hitbox) return;
        hitbox = value;
        OnPropertyChanged(nameof(Hitbox));
      }
    }

    private BitmapImage image;
    public BitmapImage Image
    {
      get { return image; }
      set
      {
        if (value == image) return;
        image = value;
        OnPropertyChanged(nameof(Image));
      }
    }

    #endregion Properties

    private int viewRange;
    public int ViewRange
    {
      get { return viewRange; }
      set
      {
        viewRange = value;
        OnPropertyChanged(nameof(ViewRange));
      }
    }

    protected Entity(int x, int y, int height, int width, int stepLength)
    {
      this.xCoord = x;
      this.yCoord = y;

      this.Height = height;
      this.Width = width;
      this.Direction = Direction.Down;
      IsMoving = false;
      this.stepLength = stepLength;
      this.hitbox = new Rect(XCoord, YCoord, Width, Height);
      Bullets = new();
      PropertyChanged += Entity_PropertyChanged;
    }

    #region Methods
    private void Entity_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(Hitbox)) return;
      RefreshHitbox();
    }
    public virtual void RefreshHitbox()
    {
      this.Hitbox = new Rect(XCoord, YCoord, Width, Height);
    }

    protected void MoveLeft()
    {
      Direction = Direction.Left;
      if (XCoord - StepLength >= GlobalVariables.MinX)
        XCoord -= StepLength;
    }
    protected void MoveRight()
    {
      Direction = Direction.Right;
      if (XCoord + StepLength + Width <= GlobalVariables.MaxX)
        XCoord += StepLength;
    }
    protected void MoveUp()
    {
      Direction = Direction.Up;
      if (YCoord - StepLength >= GlobalVariables.MinY)
        YCoord -= StepLength;
    }
    protected void MoveDown()
    {
      Direction = Direction.Down;
      if (YCoord + StepLength + Height <= GlobalVariables.MaxY)
        YCoord += StepLength;
    }

    /// <summary>
    /// Defines a action happening when entity is stopping a movement
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public abstract void StopMovement(object? sender, EventArgs e);

    #endregion Methods

  }
}
