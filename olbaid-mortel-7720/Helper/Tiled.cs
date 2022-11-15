namespace olbaid_mortel_7720.Helper
{
  class Tiled
  {
    int Versatzx = 14;
    public int RectangleWidth { get; set; } //Ractangel
    public int RectangleHeight { get; set; } //Ractangel

    public int RenderTransformOrigin { get; set; } // Default RenderTransformOrigin="0,0"

    public string ImageSource { get; set; } //Tielsetz png 

    public int ractX { get; set; } //Left Corner Image
    public int ractY { get; set; } //Top Corner Image

    public Tiled(int height, int width, int x, int y)
    {
      ractX = -x - Versatzx;
      ractY = -y;
      RectangleWidth = width + x;
      RectangleHeight = height + y;
    }
  }
}
