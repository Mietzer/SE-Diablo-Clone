using olbaid_mortel_7720.Engine;
using System.Collections.Generic;

namespace olbaid_mortel_7720.MVVM.Model
{
  public class Level
  {
    #region Properties

    //TODO: ADD Progperties for Level

    public Map Map;
    
    private List<Enemy> _enemySpawnList;
    public List<Enemy> EnemySpawnList
    {
      get { return _enemySpawnList; }
      private set { _enemySpawnList = value; }
    }

    #endregion Properties

    public Level(Map map, List<Enemy> enemySpawnList)
    {
      Map = map;
      EnemySpawnList = enemySpawnList;
    }

    #region Methods
    //TODO: Add WIN/LOSE Game 

    #endregion Methods
  }
}
