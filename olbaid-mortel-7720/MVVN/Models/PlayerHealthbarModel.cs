using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace olbaid_mortel_7720.MVVN.Models;

/// <summary>
/// TODO: Model should perhaps be the player itself.
/// Model class providing a health property.
/// </summary>
public class PlayerHealthbarModel : INotifyPropertyChanged
{
    private int _health;
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
    
    public PlayerHealthbarModel()
    {
        _health = 100;
    }
    
    public void UpdateHealth(int health)
    {
        _health = health;
        OnPropertyChanged(nameof(Health));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        if (!String.IsNullOrEmpty(propertyName))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}