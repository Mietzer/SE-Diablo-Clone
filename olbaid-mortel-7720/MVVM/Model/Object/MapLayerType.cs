﻿using olbaid_mortel_7720.Engine;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  public static class MapLayerType
  {
    public const string OUTER_WALL = "outer wall";
    public const string INNER_WALL = "Inner Wall";
    public const string DESTRUCTIBLE_WALL = "Destructible Wall";
    public const string STAIR = "Stairs";
    public const string FLOOR = "Floor";
    public const string FURNITURE = "Furniture";
    public const string HOLE = "Holes";
    public const string FURNITURE_DECORATION = "Furniture on Top";
    public const string DOOR = "outer Door";
    
    public static Barrier.BarrierType GetBarrierType(string layerName)
    {
      switch (layerName)
      {
        case OUTER_WALL:
        case INNER_WALL:
        case DESTRUCTIBLE_WALL:
          return Barrier.BarrierType.Wall;
        case HOLE:
          return Barrier.BarrierType.Hole;
        case STAIR:
        case FLOOR:
          return Barrier.BarrierType.Floor;
        case FURNITURE:
        case FURNITURE_DECORATION:
          return Barrier.BarrierType.Furniture;
        default:
          return Barrier.BarrierType.None;
      }
    }
  }
}