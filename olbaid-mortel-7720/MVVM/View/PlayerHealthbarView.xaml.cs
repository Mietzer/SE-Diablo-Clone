using olbaid_mortel_7720.MVVM.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace olbaid_mortel_7720.MVVM.Views
{
  /// <summary>
  ///   View component for the health bar in the overall player's gui.
  ///   Looks best at side ratio of Height 1 to Width 6.
  /// </summary>
  public partial class PlayerHealthbarView : UserControl
  {
    private readonly PlayerHealthbarModel _model;

    /// <summary>
    ///   Constructor.
    /// </summary>
    public PlayerHealthbarView()
    {
      InitializeComponent();
      _model = DataContext as PlayerHealthbarModel;
      if (_model != null)
      {
        _model.PropertyChanged += OnModelPropertyChanged;
      }
    }

    #region EventHandlers
    /// <summary>
    ///   Should update the healthbar's design when the player's effect changes.
    /// </summary>
    private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(PlayerHealthbarModel.Effect))
      {
        PlayerEffect effect = _model.Effect;
        BitmapImage iconSource;
        switch (effect)
        {
          case PlayerEffect.Burning:
            iconSource = new BitmapImage(new Uri("pack://application:,,,/Images/Healthbar/Icons/heart-burnt.gif"));
            break;
          case PlayerEffect.Poisoned:
            iconSource =
              new BitmapImage(new Uri("pack://application:,,,/Images/Healthbar/Icons/heart-poisoned.png"));
            break;
          case PlayerEffect.Healing:
            iconSource = new BitmapImage(new Uri("pack://application:,,,/Images/Healthbar/Icons/heart-healing.gif"));
            break;
          case PlayerEffect.Protected:
            iconSource =
              new BitmapImage(new Uri("pack://application:,,,/Images/Healthbar/Icons/heart-protected.gif"));
            break;
          default:
            iconSource = new BitmapImage(new Uri("pack://application:,,,/Images/Healthbar/Icons/heart-normal.png"));
            break;
        }
        ImageBehavior.SetAnimatedSource(ImgIcon, iconSource);

        if (effect == PlayerEffect.Poisoned)
        {
          ImgBar.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Healthbar/bar-poisoned.png"));
        }
        else
        {
          ImgBar.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Healthbar/bar-normal.png"));
        }
      }
    }

    /// <summary>
    ///   Adjusts the percentage text to the correct size in proportion to the healthbar's size.
    /// </summary>
    private void OnViewLoaded(object sender, RoutedEventArgs e)
    {
      int sizeOfText = (int)Math.Round(ActualHeight / 2);
      TxtPercentage.FontSize = sizeOfText;
      TxtPercentage.Margin = new Thickness(0, 0, sizeOfText, 0);
    }
    #endregion
  }
}