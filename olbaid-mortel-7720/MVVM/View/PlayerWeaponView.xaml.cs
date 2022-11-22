using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace olbaid_mortel_7720.MVVM.View
{
  public partial class PlayerWeaponView : UserControl, INotifyPropertyChanged
  {
    private BitmapImage _image;
    
    public BitmapImage Image
    {
      get { return _image; }
      private set
      {
        if (value == _image) return;
        _image = value;
        OnPropertyChanged();
      }
    }
    
    public PlayerWeaponView(Player player)
    {
      InitializeComponent();
      this.DataContext = this;
      Image = RessourceImporter.Import(ImageCategory.ITEMS, player.CurrentWeapon.GetImageString());
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
      ImgWeapon.Height = ActualHeight / 2.5;
      ImgWeapon.Width = ActualHeight / 2.5 * 2;
    }
  }
}