using olbaid_mortel_7720.Engine;
using olbaid_mortel_7720.MVVM.Model.Object;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace olbaid_mortel_7720.Helper
{
  public class EnemyDeathPoint : EventArgs
  {
    public ReadOnlyCollection<CollectableObject> Drops { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
  }
}
