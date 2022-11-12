using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        yCoord = value;
        OnPropertyChanged(nameof(YCoord));
      }
    }

    public int XCoordMax { get; private set; }
    public int YCoordMax { get; private set; }
    public int XCoordMin { get; private set; }
    public int YCoordMin { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }
    public Direction Direction { get; private set; }
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
      set { hitbox = value; }
    }
    
    private BitmapImage image;
    public BitmapImage Image
    {
      get { return image; }
      set
      {
        image = value;
        OnPropertyChanged(nameof(Image));
      }
    }

    #endregion Properties

    public Entity(int x, int y, int xMin, int yMin, int xMax, int yMax, int height, int width, int stepLength)
    {
      this.xCoord = x;
      this.yCoord = y;
      this.XCoordMin = xMin;
      this.YCoordMin = yMin;
      this.XCoordMax = xMax;
      this.YCoordMax = yMax;
      this.Height = height;
      this.Width = width;
      this.Direction = Direction.Down;
      IsMoving = false;
      this.stepLength = stepLength;
      this.hitbox = new Rect(XCoord, YCoord, Width, Height);
      Bullets = new();
      PropertyChanged += Entity_PropertyChanged;
      Stop(null, null);
    }

    #region Methods
    private void Entity_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      RefreshHitbox();
    }
    private void RefreshHitbox()
    {
      this.hitbox = new Rect(XCoord, YCoord, Width, Height);
    }

    protected void MoveLeft()
    {
      Direction = Direction.Left;
      if (XCoord - StepLength >= XCoordMin)
        XCoord -= StepLength;
    }
    protected void MoveRight()
    {
      Direction = Direction.Right;
      if (XCoord + StepLength + Width <= XCoordMax)
        XCoord += StepLength;
    }
    protected void MoveUp()
    {
      Direction = Direction.Up;
      if (YCoord - StepLength >= YCoordMin)
        YCoord -= StepLength;
    }
    protected void MoveDown()
    {
      Direction = Direction.Down;
      if (YCoord + StepLength + Height <= YCoordMax)
        YCoord += StepLength;
    }
    
    public abstract void Stop(object sender, KeyEventArgs e);

    #endregion Methods

  }
}
