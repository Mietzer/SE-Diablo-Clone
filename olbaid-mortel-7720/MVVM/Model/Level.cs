﻿using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model.Enemies;
using olbaid_mortel_7720.MVVM.Model.Object;
using olbaid_mortel_7720.MVVM.Model.Object.Weapons;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System;
using System.Collections.Generic;



namespace olbaid_mortel_7720.MVVM.Model
{
  public class Level
  {
    #region Properties

    //TODO: ADD Properties for Level

    public Map Map;

    public List<DropObject> DropObjects;

    private List<Enemy> _enemySpawnList;
    public List<Enemy> EnemySpawnList
    {
      get { return _enemySpawnList; }
      private set { _enemySpawnList = value; }
    }

    #endregion Properties

    public Level(Map map)
    {
      Map = map;
      DropObjects = new List<DropObject>();
    }

    #region Methods
    public void SpawnEnemies(MapViewModel mapModel, uint enemyCount)
    {
      Random rnd = new Random();
      List<Enemy> spawnList = new List<Enemy>();
      List<SpawnObject> objectList = this.Map.LoadObjects();
      List<SpawnObject> spawnPoints = new List<SpawnObject>();
      int index;
      int spawnGen;

      foreach (SpawnObject x in objectList)
      {
        if (x.Name.Contains("Enemy Spawn"))
        {
          spawnPoints.Add(x);
        }
      }

      spawnPoints = FixCoordinates(spawnPoints);

      for (int i = 0; i < enemyCount; i++)
      {
        spawnGen = rnd.Next(0, 100);
        if (spawnGen <= 35)
        {
          index = rnd.Next(0, spawnPoints.Count);
          spawnList.Add(new EnemyMelee(spawnPoints[index].X, spawnPoints[index].Y, mapModel));

        }
        else if (spawnGen > 35 && spawnGen <= 70)
        {
          index = rnd.Next(0, spawnPoints.Count);
          spawnList.Add(new EnemyRanged(spawnPoints[index].X, spawnPoints[index].Y, mapModel));
        }
        else if (spawnGen > 70 && spawnGen <= 80)
        {
          index = rnd.Next(0, spawnPoints.Count);
          spawnList.Add(new EnemyRareMelee(spawnPoints[index].X, spawnPoints[index].Y, mapModel));
        }
        else if (spawnGen > 80 && spawnGen <= 100)
        {
          index = rnd.Next(0, spawnPoints.Count);
          spawnList.Add(new EnemyRareRanged(spawnPoints[index].X, spawnPoints[index].Y, mapModel));
        }
        spawnList[i].IsDeath += RandomDrop;
      }
      EnemySpawnList = spawnList;
    }

    private static List<SpawnObject> FixCoordinates(List<SpawnObject> list)
    {
      int count = 0;
      foreach (SpawnObject obj in list)
      {
        if (count == 1)
        {
          obj.Y = obj.Y - 20;
        }
        else if (count == 2)
        {
          obj.Y = obj.Y - 100;
        }
        else if (count == 3)
        {
          obj.Y = obj.Y - 40;
          obj.X = obj.X + 50;
        }
        else if (count == 4)
        {
          obj.X = obj.X - 150;
        }
        else if (count == 6)
        {
          obj.X = obj.X - 150;
        }
        else if (count == 7)
        {
          obj.Y = obj.Y - 60;
        }
        count++;
      }

      return list;
    }


    private void RandomDrop(object sender, EnemyDeathPoint e)
    {
      Random rnd = new Random();
      int DropNummer = rnd.Next(0, 1);

      if (DropNummer >= 0 && 4 >= DropNummer)
      {
        DropObject dropObject = new DropObject(e.X, e.Y, "DropObject", true);
        switch (DropNummer)
        {
          case 0:
            dropObject.AddAsLoot(new Medicine(10, 20));
            DropObjects.Add(dropObject);
            break;
          case 1:
            dropObject.AddAsLoot(new Paralysis(10, 20));
            DropObjects.Add(dropObject);
            break;
          case 2:
            //Armor
            break;
          case 3:
            //Protection 
            break;
          case 4:
            //upgrade +1 Damage 
            break;
        }
        ObjectDropt?.Invoke(this, EventArgs.Empty);
      }

    }
    #endregion Methods
    #region Events

    public event EventHandler ObjectDropt;

    #endregion Events
  }
}
