using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace olbaid_mortel_7720.MVVM.Viewmodel
{
  public class MapViewModel : NotifyObject
  {
    #region Properties

    public List<Rectangle> Rectangles;
    public Grid MyGrid;
    private Map map;

    #endregion Properties

    public MapViewModel(Grid mygrid, Map map)
    {
      Rectangles = new List<Rectangle>();
      this.map = map;
      MyGrid = mygrid;
      RenderMap();
      CreatObjects();
    }

    #region Methods
    public void RenderMap()
    {
      List<MapObject> rednermap = map.Load();
      BitmapImage tilesetImage = ImageImporter.Import(ImageCategory.TILESETS, "Level1.png");

      //Randering the Map 
      for (int i = 0; i < rednermap.Count; i++)
      {
        Rectangles.Add(new Rectangle());

        Rectangles[i].Width = 2000;
        Rectangles[i].Height = 2000;

        ImageBrush myImageBrush = new ImageBrush();
        myImageBrush.ImageSource = tilesetImage;
        myImageBrush.ViewboxUnits = BrushMappingMode.Absolute;
        myImageBrush.Stretch = Stretch.None;
        myImageBrush.TileMode = TileMode.None;
        myImageBrush.AlignmentX = AlignmentX.Left;
        myImageBrush.AlignmentY = AlignmentY.Top;

        double x = -rednermap[i].Graphic.Imagex;
        double y = -rednermap[i].Graphic.Imagey;
        myImageBrush.Transform = new MatrixTransform(0.75d, 0.0d, 0.0d, 0.75d, x, y); //m11=default 1   m12 m21 m22=default 1 0.75 da 0.25 Skalierungfaktor rausrechnen x y 
        Rectangles[i].Fill = myImageBrush;

        Grid.SetColumn(Rectangles[i], rednermap[i].Graphic.Index % map.MapWidth);
        Grid.SetRow(Rectangles[i], (rednermap[i].Graphic.Index - (rednermap[i].Graphic.Index % map.MapWidth)) / map.MapWidth);

        MyGrid.Children.Add(Rectangles[i]);
      }
    }

    public void CreatObjects()
    {
      List<SpawnObject> rednerobjects = map.LoadObjects();

      for (int i = 0; i < rednerobjects.Count; i++)
      {
        Grid.SetColumn(rednerobjects[i].Hitbox, CalculateGridPosition(rednerobjects[i].X));
        Grid.SetRow(rednerobjects[i].Hitbox, CalculateGridPosition(rednerobjects[i].Y));
        if (rednerobjects[i].Hitbox.Width > 32)
          Grid.SetColumnSpan(rednerobjects[i].Hitbox, CalculateGridSpan(rednerobjects[i].Hitbox.Width));
        if (rednerobjects[i].Hitbox.Height > 32)
          Grid.SetRowSpan(rednerobjects[i].Hitbox, CalculateGridSpan(rednerobjects[i].Hitbox.Height));
        MyGrid.Children.Add(rednerobjects[i].Hitbox);
      }

    }

    public int CalculateGridPosition(float wert)
    {
      int iwert = Convert.ToInt32(wert / 32);
      if (0 < (wert / 32) - iwert)
        return (iwert);
      return iwert - 1;
    }
    public int CalculateGridSpan(double wert)
    {
      int iwert = Convert.ToInt32(wert / 32);
      if (0 < (wert / 32) - iwert)
        return iwert + 1;
      return iwert;
    }

    #endregion Methods


  }

}
