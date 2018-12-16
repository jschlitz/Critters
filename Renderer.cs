using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Linq.Expressions;
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
    public Renderer()
    {
      Vegetation = Colors.ForestGreen;
    }

    public SolidColorBrush Background { get; set; } = Brushes.DarkSlateGray;
    private List<SolidColorBrush> VegBrushes = new List<SolidColorBrush>();
    private List<Pen> VegPens = new List<Pen>();
    public Color Vegetation
    {
      get => VegBrushes[VegBrushes.Count - 1].Color;
      set
      {
        VegBrushes.Clear();
        VegPens.Clear();

        //0 is empty
        VegBrushes.Add(Background); 
        VegPens.Add(new Pen(Background, 1));

        for (int i = 1; i <= Cell.MAX_VEGETATION; i++)
        {
          var c = Color.Multiply(value, (float)(0.5 + 0.5*i / Cell.MAX_VEGETATION));
          var b = new SolidColorBrush(c);
          VegBrushes.Add(b);
          VegPens.Add(new Pen(b, 1));
        }
      }
    }
    public Brush Edge { get; set; } = Brushes.SlateGray;
    public Canvas Canvas { get; set; }
    public int CellSize { get; set; } = 24;

    public void Render(Field field)
    {
    //TODO: the background can probably cached too.
    //  var background = new RectangleGeometry();
    //  background.Fill = Background;
    //  background.

      // TODO: consider caching the grid. or a drawingbrush?
      var grid = new StreamGeometry();
      using (var ctx = grid.Open())
      {
        for (int x = 0; x < field.MaxX; x++)
        {
          ctx.BeginFigure(new Point(CellSize * x, CellSize * field.MaxY), true, false);
          ctx.LineTo(new Point(CellSize * x, 0), true, false);
        }
        for (int y = 0; y < field.MaxY; y++)
        {
          ctx.BeginFigure(new Point(CellSize * field.MaxX, CellSize * y), true, false);
          ctx.LineTo(new Point(0, CellSize * y), true, false);
        }
      }
      var gridPath = new Path { Stroke = Edge, StrokeThickness = 1};
      grid.Freeze();
      gridPath.Data = grid;

      //veg
      Canvas.Children.Clear();
      Path[] vegPath = new Path[Cell.MAX_VEGETATION + 1];

      vegPath[0] = new Path();//0 is empty
      for (int i = 1; i <= Cell.MAX_VEGETATION; i++)
      {
        vegPath[i] = GetVegPath(i, field);
      }

      //Now actually add things, back to front.
      foreach (var v in vegPath)
        Canvas.Children.Add(v);
      Canvas.Children.Add(gridPath);
    }

    private Path GetVegPath(int index, Field field)
    {
      var gg = new GeometryGroup();
      for (int x = 0; x < field.MaxX; x++)
      {
        for (int y = 0; y < field.MaxY; y++)
        {
          if (field.Cells[x, y].Vegetation == index)
          {
            gg.Children.Add(new EllipseGeometry(new Rect(CellSize * x, CellSize * y, CellSize, CellSize)));
          }
        }
      }
      var result = new Path
      {
        Stroke = VegBrushes[index],
        Fill = VegBrushes[index],
        Data = gg
      };

      return result;
    }
  }
}
