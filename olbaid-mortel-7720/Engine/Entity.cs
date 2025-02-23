﻿using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Enemies;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Generic;
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
      internal set
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
      internal set
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
      protected set
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

    public List<Barrier> Barriers { get; private set; }
    #endregion Properties

    protected Entity(int x, int y, int height, int width, int stepLength, MapViewModel mapModel)
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

      if (mapModel == null) Barriers = new();
      else Barriers = mapModel.Barriers;
    }

    ~Entity() { }

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
      if (VerifyNoCollision())
        XCoord -= StepLength;
    }
    protected void MoveRight()
    {
      Direction = Direction.Right;
      if (VerifyNoCollision())
        XCoord += StepLength;
    }
    protected void MoveUp()
    {
      Direction = Direction.Up;
      if (VerifyNoCollision())
        YCoord -= StepLength;
    }
    protected void MoveDown()
    {
      Direction = Direction.Down;
      if (VerifyNoCollision())
        YCoord += StepLength;
    }

    /// <summary>
    /// Defines a action happening when entity is stopping a movement
    /// </summary>
    /// <param name="e"></param>
    public abstract void StopMovement(EventArgs e);

    /// <summary>
    /// Checks if there is a collision with a barrier
    /// </summary>
    /// <returns></returns>
    protected bool VerifyNoCollision()
    {
      Rect testHitbox = new Rect(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);
      switch (Direction)
      {
        case Direction.Left:
          testHitbox.X -= StepLength;
          break;
        case Direction.Right:
          testHitbox.X += StepLength;
          break;
        case Direction.Up:
          testHitbox.Y -= StepLength;
          break;
        case Direction.Down:
          testHitbox.Y += StepLength;
          break;
      }
      List<Barrier> intersections = Barriers.FindAll(barrier => barrier.Hitbox.IntersectsWith(testHitbox));
      if (this is EnemyBoss)
      {
        intersections.RemoveAll(barrier => barrier.Type == Barrier.BarrierType.Hole);
      }
      return intersections.Count == 0;
    }

    protected void Dispose()
    {
      //Barriers?.Clear();
      //Barriers = null;
      PropertyChanged -= Entity_PropertyChanged;
    }
    #endregion Methods

  }
}
