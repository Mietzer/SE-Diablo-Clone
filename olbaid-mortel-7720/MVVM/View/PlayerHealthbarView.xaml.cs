using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  ///   View component for the health bar in the overall player's gui.
  ///   Looks best at side ratio of Height 1 to Width 6.
  /// </summary>
  public partial class PlayerHealthbarView : UserControl
  {
    private readonly Player _model;

    /// <summary>
    ///   Constructor.
    /// </summary>
    public PlayerHealthbarView(Player player)
    {
      InitializeComponent();
      DataContext = player;
      _model = player;
      _model.PropertyChanged += OnModelPropertyChanged;
    }

    ~PlayerHealthbarView() { }
    #region EventHandlers
    /// <summary>
    ///   Should update the healthbar's design when the player's effect changes.
    /// </summary>
    private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(Player.Effect))
      {
        PlayerEffect effect = _model.Effect;
        BitmapImage iconSource;
        switch (effect)
        {
          case PlayerEffect.Burning:
            iconSource = ImageImporter.Import(ImageCategory.HEALTHBAR_ICONS, "heart-burnt.gif");
            break;
          case PlayerEffect.Poisoned:
            iconSource = ImageImporter.Import(ImageCategory.HEALTHBAR_ICONS, "heart-poisoned.png");
            break;
          case PlayerEffect.Healing:
            iconSource = ImageImporter.Import(ImageCategory.HEALTHBAR_ICONS, "heart-healing.gif");
            break;
          case PlayerEffect.Protected:
            iconSource = ImageImporter.Import(ImageCategory.HEALTHBAR_ICONS, "heart-protected.gif");
            break;
          default:
            iconSource = ImageImporter.Import(ImageCategory.HEALTHBAR_ICONS, "heart-normal.png");
            break;
        }
        ImageBehavior.SetAnimatedSource(ImgIcon, iconSource);

        if (effect == PlayerEffect.Poisoned)
        {
          ImgBar.Source = ImageImporter.Import(ImageCategory.HEALTHBAR, "bar-poisoned.png");
        }
        else
        {
          ImgBar.Source = ImageImporter.Import(ImageCategory.HEALTHBAR, "bar-normal.png");
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

    private void CtlHealthbar_Unloaded(object sender, RoutedEventArgs e)
    {
      _model.PropertyChanged -= OnModelPropertyChanged;
    }
  }
}