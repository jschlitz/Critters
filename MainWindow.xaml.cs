using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Critters
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {      
      InitializeComponent();
      var rand = new Random(413);
      Renderer renderer = new Renderer { Canvas = this.MainCanvas };
      var f = new Field(80, 64, Neighborhood.Moore,1);
      for (int i = 0; i < f.MaxX; i++)
      {
        for (int j = 0; j < f.MaxY; j++)
        {
          //f.Cells[i, j].Vegetation = (rand.NextDouble() > 0.7) ? rand.Next(1, 11) : 0;
          f.Cells[i, j].Vegetation = (i == 1) ? rand.Next(1, 11) : 0;
          f.CalculateNeighbors(i,j);
        }
      }

      f.Renderer = renderer;

      DataContext = f;

    //https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/geometry-overview - consider combinedgeometry. where should Render() live?

    }
  }
}
