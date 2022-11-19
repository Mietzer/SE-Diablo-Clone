using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Windows.Controls;
/*
        <Rectangle Width="auto" Height="auto" RenderTransformOrigin="0,0" Grid.Column="0" Grid.Row="0" Grid.ColumnSpan="20" Grid.RowSpan="15" >
            <Rectangle.Fill>
                <ImageBrush ImageSource="pack://application:,,,/Images/Tilesets/Level1.png" AlignmentX="Left" AlignmentY="top" Stretch="None" TileMode="None" ViewboxUnits="Absolute" RenderOptions.BitmapScalingMode="HighQuality">
                    <ImageBrush.Transform>
                        <ScaleTransform ScaleX="0.75" ScaleY="0.75" />
                    </ImageBrush.Transform>
                </ImageBrush>
            </Rectangle.Fill>
        </Rectangle>
        <Rectangle Width="2000" Height="32" RenderTransformOrigin="0,0" Grid.Column="1" Grid.Row="0" >
            <Rectangle.Fill>
                <ImageBrush ImageSource="pack://application:,,,/Images/Tilesets/Level1.png" AlignmentX="Left" AlignmentY="top" Stretch="None" TileMode="None" ViewboxUnits="Absolute" >
                    <ImageBrush.Transform>
                        <TranslateTransform X="-206" Y="0" />
                    </ImageBrush.Transform>
                </ImageBrush>
            </Rectangle.Fill>
        </Rectangle>
 */

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für MapView.xaml
  /// </summary>
  public partial class MapView : UserControl
  {
    public MapView(Map map)
    {
      InitializeComponent();

      MapViewModel vm = new MapViewModel(myGrid, map);
    }



  }
}
