
using olbaid_mortel_7720.MVVM.Model;
using olbaid_mortel_7720.MVVM.Viewmodel;
using System.Windows.Controls;
/*
             <Grid Name="myGrid">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
                <ColumnDefinition Width="32"/>
            </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
            <RowDefinition Height="32"/>
        </Grid.RowDefinitions>


        <Rectangle Width="213" Height="85" RenderTransformOrigin="0,0" Grid.Column="0" Grid.Row="0" Grid.ColumnSpan="1" Grid.RowSpan="2" >
            <Rectangle.Fill>
                <ImageBrush ImageSource="pack://application:,,,/Images/Tilesets/Level1_old.png" AlignmentX="Left" AlignmentY="top" Stretch="None" TileMode="None" ViewboxUnits="Absolute" RenderOptions.BitmapScalingMode="HighQuality">
                    <ImageBrush.Transform>
                        <MatrixTransform>
                            <MatrixTransform.Matrix>
                                <Matrix M11="0.75" M12="0" M21="0" M22="0.75" OffsetX="-128" OffsetY="-32"/>
                            </MatrixTransform.Matrix>
                        </MatrixTransform>
                    </ImageBrush.Transform>
                </ImageBrush>
            </Rectangle.Fill>
        </Rectangle>

    </Grid>
 */

namespace olbaid_mortel_7720.MVVM.View
{
  /// <summary>
  /// Interaktionslogik für MapView.xaml
  /// </summary>
  public partial class MapView : UserControl
  {
    public MapViewModel Vm;
    public MapView(Map map)
    {
      InitializeComponent();
      //myGrid
      Vm = new MapViewModel(MyCanvas, map);
    }



  }
}

