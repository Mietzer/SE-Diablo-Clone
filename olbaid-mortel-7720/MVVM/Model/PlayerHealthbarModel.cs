using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace olbaid_mortel_7720.MVVN.Models
{
  /// <summary>
  ///   TODO: Model should perhaps be the player itself.
  ///   Model class providing a health property.
  /// </summary>
  public class PlayerHealthbarModel : INotifyPropertyChanged
  {
    private PlayerEffect _effect;
    private int _health;

    public PlayerHealthbarModel()
    {
      _health = 100;
      _effect = PlayerEffect.None;
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

    public PlayerEffect Effect
    {
      get => _effect;
      set
      {
        if (value == _effect) return;
        _effect = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void UpdateHealth(int health)
    {
      Health = health;
    }

    public void UpdateEffect(PlayerEffect effect)
    {
      Effect = effect;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
      if (!string.IsNullOrEmpty(propertyName))
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}