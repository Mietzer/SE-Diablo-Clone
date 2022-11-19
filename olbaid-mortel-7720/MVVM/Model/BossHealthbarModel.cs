using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace olbaid_mortel_7720.MVVM.Models
{
  /// <summary>
  ///   TODO: Model should perhaps be the Boss itself.
  ///   Model class providing a health property.
  /// </summary>
  public class BossHealthbarModel : INotifyPropertyChanged
  {
    private int _health;

    public BossHealthbarModel()
    {
      _health = 100;
    }

    public int Health
    {
      get => _health;
      set
      {
        if (value == _health) return;
        _health = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void UpdateHealth(int health)
    {
      Health = health;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
      if (!string.IsNullOrEmpty(propertyName))
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}