using olbaid_mortel_7720.MVVM.Model;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

      var rednermap = Level1.Load();


      rectangles = new Rectangle[] { this.Tiled01, this.Tiled02, this.Tiled03, this.Tiled04, this.Tiled05, this.Tiled06, this.Tiled07, this.Tiled08, this.Tiled09, this.Tiled10, this.Tiled11, this.Tiled12, this.Tiled13, this.Tiled14, this.Tiled15, this.Tiled16, this.Tiled17, this.Tiled18, this.Tiled19, this.Tiled20,
      this.Tiled21, this.Tiled22, this.Tiled23, this.Tiled24, this.Tiled25, this.Tiled26, this.Tiled27, this.Tiled28, this.Tiled29, this.Tiled30, this.Tiled31, this.Tiled32, this.Tiled33, this.Tiled34, this.Tiled35, this.Tiled36, this.Tiled37, this.Tiled38, this.Tiled39, this.Tiled30,
      this.Tiled41, this.Tiled42, this.Tiled43, this.Tiled44, this.Tiled45, this.Tiled46, this.Tiled47, this.Tiled48, this.Tiled49, this.Tiled50, this.Tiled51, this.Tiled52, this.Tiled53, this.Tiled54, this.Tiled55, this.Tiled56, this.Tiled57, this.Tiled58, this.Tiled59, this.Tiled60,
      this.Tiled61, this.Tiled62, this.Tiled63, this.Tiled64, this.Tiled65, this.Tiled66, this.Tiled67, this.Tiled68, this.Tiled69, this.Tiled70, this.Tiled71, this.Tiled72, this.Tiled73, this.Tiled74, this.Tiled75, this.Tiled76, this.Tiled77, this.Tiled78, this.Tiled79, this.Tiled80,
      this.Tiled81, this.Tiled82, this.Tiled83, this.Tiled84, this.Tiled85, this.Tiled86, this.Tiled87, this.Tiled88, this.Tiled89, this.Tiled90, this.Tiled91, this.Tiled92, this.Tiled93, this.Tiled94, this.Tiled95, this.Tiled96, this.Tiled97, this.Tiled98, this.Tiled99, this.Tiled100
      };
      for (int t = 0; t < 5; t++)
      {
        int r = 0;
        int i = 0;
        while (i < 100)
        {
          r++;
          for (int c = 0; c < 10 || i < 100; c++)
          {
            if (rednermap[t, i] != null)
            {
              //rectangles[i].Width = rednermap[t, i].width + rednermap[t, i].x;
              //rectangles[i].Height = rednermap[t, i].height + rednermap[t, i].y;
              rectangles[i].Width = 206;
              rectangles[i].Height = 64;

              ImageBrush myImageBrush = new ImageBrush();
              myImageBrush.ImageSource = new BitmapImage(new Uri("C:\\Users\\Konsti\\OneDrive - Technische Hochschule Nürnberg Georg Simon Ohm\\Documents\\Studium\\Semester 3\\SE Praktikum\\git\\olbaid-mortel-7720\\Images\\Tilesets\\Level1.png", UriKind.RelativeOrAbsolute));
              myImageBrush.ViewboxUnits = BrushMappingMode.Absolute;
              myImageBrush.Stretch = Stretch.None;
              myImageBrush.AlignmentX = AlignmentX.Left;
              myImageBrush.AlignmentY = AlignmentY.Top;

              double x = -rednermap[t, i].x - 14;
              double y = rednermap[t, i].y;
              //myImageBrush.Transform = new MatrixTransform(1.0d, 0.0d, 0.0d, 1.0d, x, y); //m11 m12 m21 m22 x y 
              myImageBrush.Transform = new MatrixTransform(1.0d, 0.0d, 0.0d, 1.0d, -174d, -32d); //m11 m12 m21 m22 x y 
              rectangles[i].Fill = myImageBrush;


              Grid.SetColumn(rectangles[i], c);
              Grid.SetRow(rectangles[i], r);
            }
            i++;
          }



        }
      }



      /*
      List<Rectangle> rectangles = new List<Rectangle>();

      for (int i = 0; i < 5; i++)
      {
        rectangles.Add(new Rectangle());
        rectangles[i].Width = 206;
        rectangles[i].Height = 64;

        ImageBrush myImageBrush = new ImageBrush();
        myImageBrush.ImageSource = new BitmapImage(new Uri("C:\\Users\\Konsti\\OneDrive - Technische Hochschule Nürnberg Georg Simon Ohm\\Documents\\Studium\\Semester 3\\SE Praktikum\\git\\olbaid-mortel-7720\\Images\\Tilesets\\Level1.png", UriKind.RelativeOrAbsolute));
        myImageBrush.ViewboxUnits = BrushMappingMode.Absolute;
        myImageBrush.Stretch = Stretch.None;
        myImageBrush.AlignmentX = AlignmentX.Left;
        myImageBrush.AlignmentY = AlignmentY.Top;
        myImageBrush.Transform = new MatrixTransform(1.0d, 0.0d, 0.0d, 1.0d, -174d, -32d); //m11 m12 m21 m22 x y 
        rectangles[i].Fill = myImageBrush;

        Grid.SetRow(rectangles[i], i + 3);
        Grid.SetColumn(rectangles[i], 0);
        this.Contr Control.Add(rectangles[i]);
      }

      */


      // Create a brush and set the source to the tileset image


    }
  }
}
