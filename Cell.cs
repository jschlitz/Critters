using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Critters
{
  public class Cell: ViewModel
  {
    public const int MAX_VEGETATION = 10;
    int _Vegetation;

    public int Vegetation
    {
      get => _Vegetation;
      set
      {
        if (_Vegetation == value) return;

        _Vegetation = value;
        OnPropertyChanged();
      }
    }

    private Cell _Upcoming;

    public IList<Cell> CachedNeighbors { get; set; }

    public void Process(IEnumerable<Cell> neighbors)
    { }
  }
}
