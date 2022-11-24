using System.IO;
using System.Text.Json;

namespace olbaid_mortel_7720.Helper
{
  public class DataProvider
  {
    #region Properties

    #endregion Properties

    #region Constructor

    #endregion Constructor

    #region Methods
    /// <summary>
    /// Saves data as a readonly-hidden json file
    /// </summary>
    /// <typeparam name="T">Serializable objecttyp</typeparam>
    /// <param name="data">Object to save</param>
    /// <param name="filename"=Name of file without ending</param>
    public void SaveData<T>(T data, string fileName)
    {
      string path = Path.Combine(fileName + ".json");
      //Convert To Json 
      string jsonString = JsonSerializer.Serialize(data);

      if (!File.Exists(path))
        File.Create(path).Close();

      //Make file writable
      File.SetAttributes(path, FileAttributes.Normal);
      File.WriteAllText(path, jsonString);

      //Prevent external changes
      File.SetAttributes(path, FileAttributes.ReadOnly);
      File.SetAttributes(path, FileAttributes.Hidden);
    }

    /// <summary>
    /// Loads data from a readonly JSON file
    /// </summary>
    /// <typeparam name="T">Deserializable class in file</typeparam>
    /// <param name="filename"=Name of file without ending</param>
    /// <returns></returns>
    public T? LoadData<T>(string filename)
    {
      string path = Path.Combine(filename + ".json");
      if (!File.Exists(path))
        return default(T);

      //Check weather file was changed or not
      if ((File.GetAttributes(path) & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
        return default(T);

      string jsonString = File.ReadAllText(path);
      return JsonSerializer.Deserialize<T>(jsonString);
    }


    #endregion Methods


  }
}
