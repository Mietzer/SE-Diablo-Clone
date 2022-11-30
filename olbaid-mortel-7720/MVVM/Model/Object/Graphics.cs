namespace olbaid_mortel_7720.Object
{
  /// <summary>
  ///  Interface for all graphics for objects
  /// </summary>
  public class Graphics
  {
    public string PathtoGraphics;
    //Variables for determination Tileset
    public int Imageheight;
    public int Imagewidth;
    public int Imagex;
    public int Imagey;
    //Location in Redert Map
    public int Index;

    public Graphics(string pathtographics, int imageheight, int imagewidth, int imagex, int imagey, int index)
    {
      this.PathtoGraphics = pathtographics;
      this.Imageheight = imageheight;
      this.Imagewidth = imagewidth;
      this.Imagex = imagex;
      this.Imagey = imagey;
      this.Index = index;
    }
  }
}
