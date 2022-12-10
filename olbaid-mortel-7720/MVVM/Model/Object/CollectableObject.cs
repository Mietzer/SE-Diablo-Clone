using olbaid_mortel_7720.Engine;
using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace olbaid_mortel_7720.MVVM.Model.Object
{
  /// <summary>
  /// Class for all types of objects that are collectable
  /// </summary>
  public abstract class CollectableObject : GameObject, ICollectable
  {
    private int overallLifetime;
    private int remainingLifetime;

    public bool CanBeCollected()
    {
      return remainingLifetime > 0;
    }

    public CollectableObject(string name, bool visible, int lifetime) : base(name, visible, false)
    {
      this.overallLifetime = this.remainingLifetime = lifetime;
      GameTimer.Instance.GameTick += this.OnGameTick;
    }

    private void OnGameTick(EventArgs e)
    {
      this.remainingLifetime--;
      if (this.remainingLifetime <= 0)
      {
        this.Remove();
      }
    }

    public void OnCollect(Player player)
    {
      Debug.WriteLine(this.Name + " collected");
      this.Remove();
      this.WhenCollected(player);
    }

    public abstract void WhenCollected(Player player);

    public void Spawn(Canvas canvas, int x, int y)
    {
      Debug.WriteLine(this.Name + " spawned");
      // TODO: Add to canvas
    }

    private void Remove()
    {
      GameTimer.Instance.GameTick -= this.OnGameTick;
      // TODO: remove from canvas
    }
  }
}
