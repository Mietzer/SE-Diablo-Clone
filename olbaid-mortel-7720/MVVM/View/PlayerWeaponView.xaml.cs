using olbaid_mortel_7720.Helper;
using olbaid_mortel_7720.MVVM.Model;
using System;
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
    private Player player;
    public BitmapImage Image
    {
      get { return _image; }
      private set
      {
        if (value == _image) return;
        _image = value;
        this.OnPropertyChanged(nameof(Image));
      }
    }

    public PlayerWeaponView(Player player)
    {
      InitializeComponent();
      this.DataContext = this;
      Image = ImageImporter.Import(ImageCategory.ITEMS, player.CurrentWeapon.GetImageString());
      this.player = player;
      player.WeaponSwap += Update;
    }

    private void Update(object sender, EventArgs e)
    {
      Image = ImageImporter.Import(ImageCategory.ITEMS, this.player.CurrentWeapon.GetImageString());
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