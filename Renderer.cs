using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace Critters
{
  /// <summary>
  /// Handles the rendering  
  /// </summary>
  /// <remarks>If I really MVVM this out, use an interface in the VM</remarks>
  public class Renderer
  {
    public Brush Background { get; set; } = Brushes.DarkSlateGray;
    public Brush Vegetation { get; set; } = Brushes.ForestGreen;
    public Brush Edge { get; set; } = Brushes.SlateGray;
    public Canvas Canvas { get; set; }
    public int CellSize { get; set; } = 24;

    public void Render(Field field)
    {
      
      var grid = new StreamGeometry();
      using (var ctx = grid.Open())
      {
        for (int x = 0; x < field.MaxX; x++)
        {
          ctx.BeginFigure(new Point(CellSize*x, CellSize * field.MaxY), true, false);
          ctx.LineTo(new Point(CellSize*x, 0),true, false);
        }
        for (int y = 0; y < field.MaxY; y++)
        {
          ctx.BeginFigure(new Point(CellSize*field.MaxX, CellSize * y), true, false);
          ctx.LineTo(new Point(0, CellSize * y), true, false);
        }
      }
      var gridPath = new Path { Stroke = Edge, StrokeThickness = 1 };
      grid.Freeze();
      gridPath.Data = grid;

      Canvas.Children.Add(gridPath);
    }
  }
}
