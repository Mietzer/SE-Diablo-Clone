using System;
using System.IO;
using System.Windows.Media.Imaging;
using TiledCS;

namespace olbaid_mortel_7720.Helper
{
  internal static class RessourceImporter
  {
    /// <summary>
    /// Import a Tiled object from a file
    /// </summary>
    /// <param name="path">file path</param>
    /// <returns>either a TiledMap or a TiledSet</returns>
    /// <exception cref="FileLoadException">if file doesn't exist or error while loading</exception>
    internal static Union<TiledTileset, TiledMap> ImportTiled(string path)
    {
      string filePath = "Levels/" + path;

      try
      {
        if (path.EndsWith(".tsx"))
        {
          return new Union<TiledTileset, TiledMap>(new TiledTileset(filePath));
        }
        else if (path.EndsWith(".tmx"))
        {
          return new Union<TiledTileset, TiledMap>(new TiledMap(filePath));
        }
        else
        {
          throw new FileLoadException($"Tiled file '{filePath}' has not a valid extension!");
        }
      }
      catch (IOException e)
      {
        throw new FileLoadException($"Tiled file '{filePath}' not found!");
      }
    }
    
    /// <summary>
    /// Import a bitmap image from a file
    /// </summary>
    /// <param name="path">file path</param>
    /// <returns>a bitmap</returns>
    /// <exception cref="FileLoadException">if file doesn't exist or error while loading</exception>
    internal static BitmapImage ImportPath(string path)
    {
      string filePath = "Images/" + path;
      if (!path.EndsWith(".png") && !path.EndsWith(".gif")) throw new FileLoadException($"Image '{filePath}' has not a valid extension!");

      try
      {
        return new BitmapImage(new Uri($"pack://application:,,,/{filePath}", UriKind.RelativeOrAbsolute));
      }
      catch (IOException e)
      {
        throw new FileLoadException($"Image '{filePath}' not found!");
      }
    }
    
    /// <summary>
    /// Import a bitmap image from a file
    /// </summary>
    /// <param name="category">the category the bitmap image belongs to</param>
    /// <param name="name">the file name with extension</param>
    /// <returns>a bitmap</returns>
    internal static BitmapImage Import(ImageCategory category, string name)
    {
      return ImportPath($"{category.Value}{name}");
    }

  }
}