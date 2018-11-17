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
      Renderer r = new Renderer { Canvas = this.MainCanvas };
      var f = new Field(10, 10, Neighborhood.Moore,2);
      for (int i = 0; i < f.MaxX; i++)
      {
        for (int j = 0; j < f.MaxY; j++)
        {
          f.Cells[i, j].Vegetation = i * 10 + j;
          f.CalculateNeighbors(i,j);
        }
      }

      f.Renderer = r;

      DataContext = f;

    //https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/geometry-overview - consider combinedgeometry. where should Render() live?

    }
  }
}
