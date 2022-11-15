using olbaid_mortel_7720.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
/*
        <Rectangle Width="auto" Height="auto" RenderTransformOrigin="0,0" Grid.Column="0" Grid.Row="0" Grid.ColumnSpan="20" Grid.RowSpan="15" >
            <Rectangle.Fill>
                <ImageBrush ImageSource="pack://application:,,,/Images/Tilesets/Level1.png" AlignmentX="Left" AlignmentY="top" Stretch="None" TileMode="None" ViewboxUnits="Absolute" RenderOptions.BitmapScalingMode="HighQuality">
                    <ImageBrush.Transform>
                        <ScaleTransform ScaleX="0.75" ScaleY="0.75" />
                    </ImageBrush.Transform>
                </ImageBrush>
            </Rectangle.Fill>
        </Rectangle>
        <Rectangle Width="2000" Height="32" RenderTransformOrigin="0,0" Grid.Column="1" Grid.Row="0" >
            <Rectangle.Fill>
                <ImageBrush ImageSource="pack://application:,,,/Images/Tilesets/Level1.png" AlignmentX="Left" AlignmentY="top" Stretch="None" TileMode="None" ViewboxUnits="Absolute" >
                    <ImageBrush.Transform>
                        <TranslateTransform X="-206" Y="0" />
                    </ImageBrush.Transform>
                </ImageBrush>
            </Rectangle.Fill>
        </Rectangle>

        

 */

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für MapView.xaml
  /// </summary>
  //public string[] Test { get; set; }
  public partial class MapView : UserControl
  {
    Rectangle[] rectangles;
    public MapView()
    {

      // This will get the current WORKING directory (i.e. \bin\Debug)
      string workingDirectory = Environment.CurrentDirectory;
      // or: Directory.GetCurrentDirectory() gives the same result

      // This will get the current PROJECT directory
      string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
      InitializeComponent();

      Map Level1 = new Map($"{projectDirectory}/Levels/Level1_test.tmx", $"{projectDirectory}/Levels/Level1.tsx");
      //Map Level1 = new Map("/Levels/Level1_test.tmx", "/Levels/Level1.tsx");
      var rednermap = Level1.Load();


      /*
      rectangles = new Rectangle[] { this.Tiled00, this.Tiled01, this.Tiled02, this.Tiled03, this.Tiled04, this.Tiled05, this.Tiled06, this.Tiled07, this.Tiled08, this.Tiled09, this.Tiled10, this.Tiled11, this.Tiled12, this.Tiled13, this.Tiled14, this.Tiled15, this.Tiled16, this.Tiled17, this.Tiled18, this.Tiled19, this.Tiled20,
      this.Tiled21, this.Tiled22, this.Tiled23, this.Tiled24, this.Tiled25, this.Tiled26, this.Tiled27, this.Tiled28, this.Tiled29, this.Tiled30, this.Tiled31, this.Tiled32, this.Tiled33, this.Tiled34, this.Tiled35, this.Tiled36, this.Tiled37, this.Tiled38, this.Tiled39, this.Tiled40,
      this.Tiled41, this.Tiled42, this.Tiled43, this.Tiled44, this.Tiled45, this.Tiled46, this.Tiled47, this.Tiled48, this.Tiled49, this.Tiled50, this.Tiled51, this.Tiled52, this.Tiled53, this.Tiled54, this.Tiled55, this.Tiled56, this.Tiled57, this.Tiled58, this.Tiled59, this.Tiled60,
      this.Tiled61, this.Tiled62, this.Tiled63, this.Tiled64, this.Tiled65, this.Tiled66, this.Tiled67, this.Tiled68, this.Tiled69, this.Tiled70, this.Tiled71, this.Tiled72, this.Tiled73, this.Tiled74, this.Tiled75, this.Tiled76, this.Tiled77, this.Tiled78, this.Tiled79, this.Tiled80,
      this.Tiled81, this.Tiled82, this.Tiled83, this.Tiled84, this.Tiled85, this.Tiled86, this.Tiled87, this.Tiled88, this.Tiled89, this.Tiled90, this.Tiled91, this.Tiled92, this.Tiled93, this.Tiled94, this.Tiled95, this.Tiled96, this.Tiled97, this.Tiled98, this.Tiled99, this.Tiled100
      };
      for (int t = 0; t < 4; t++)
      {
        int r = 0;
        int i = 0;
        while (i < 100)
        {
          for (int c = 0; c < 10 && i < 100; c++)
          {

            if (rednermap[t, i] != null)
            {
              int h = i;
              if (t == 2)
                h = 100;

              //int width = rednermap[t, i].width + rednermap[t, i].x;
              //int height = rednermap[t, i].height;
              //rectangles[i].Width = width;
              //rectangles[i].Height = height;
              rectangles[h].Width = 2000;
              rectangles[h].Height = 2000;

              ImageBrush myImageBrush = new ImageBrush();
              myImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Tilesets/Level1_old.png", UriKind.Absolute));
              myImageBrush.ViewboxUnits = BrushMappingMode.Absolute;
              myImageBrush.Stretch = Stretch.None;
              myImageBrush.TileMode = TileMode.None;
              myImageBrush.AlignmentX = AlignmentX.Left;
              myImageBrush.AlignmentY = AlignmentY.Top;


              double x = -rednermap[t, i].x;
              double y = -rednermap[t, i].y;
              myImageBrush.Transform = new MatrixTransform(0.75d, 0.0d, 0.0d, 0.75d, x, y); //m11=default 1   m12 m21 m22=default 1 0.75 da 0.25 Skalierungfaktor rausrechnen x y 
              //myImageBrush.Transform = new MatrixTransform(0.75d, 0.0d, 0.0d, 0.75d, -128d, 0d); //m11 m12 m21 m22 x y 

              //Test für erstellung von Level 
              /*
              if (t == 0)
                rectangles[i].Fill = Brushes.Blue;
              else if (t == 1)
                rectangles[i].Fill = Brushes.Black;
              else if (t == 2)
                rectangles[i].Fill = Brushes.Brown;
              else if (t == 3)
                rectangles[i].Fill = Brushes.Orange;
              else
                rectangles[i].Fill = Brushes.Yellow;
              /

              rectangles[h].Fill = myImageBrush;

              Grid.SetColumn(rectangles[h], c);
              //Grid.SetColumnSpan(rectangles[i], 20);
              Grid.SetRow(rectangles[h], r);
              //Grid.SetRowSpan(rectangles[i], 20);


            }
            i++;
          }
          r++;

        }

      }
      */

      //
      List<Rectangle> rectangles = new List<Rectangle>();
      int t = 0;
      for (int i = 0; i < 5; i++)
      {
        rectangles.Add(new Rectangle());
        rectangles[i].Width = 2000;
        rectangles[i].Height = 2000;

        ImageBrush myImageBrush = new ImageBrush();
        myImageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Tilesets/Level1_old.png", UriKind.Absolute));
        myImageBrush.ViewboxUnits = BrushMappingMode.Absolute;
        myImageBrush.Stretch = Stretch.None;
        myImageBrush.TileMode = TileMode.None;
        myImageBrush.AlignmentX = AlignmentX.Left;
        myImageBrush.AlignmentY = AlignmentY.Top;
        double x = -rednermap[t, i].x;
        double y = -rednermap[t, i].y;
        myImageBrush.Transform = new MatrixTransform(0.75d, 0.0d, 0.0d, 0.75d, x, y); //m11=default 1   m12 m21 m22=default 1 0.75 da 0.25 Skalierungfaktor rausrechnen x y 
        rectangles[i].Fill = myImageBrush;

        Grid.SetRow(rectangles[i], i + 3);
        Grid.SetColumn(rectangles[i], 0);
        myGrid.Children.Add(rectangles[i]);

      }

      //


      // Create a brush and set the source to the tileset image


    }
  }
}
