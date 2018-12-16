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
    protected Cell Upcoming { get { return _Upcoming ?? (_Upcoming = new Cell()); } }

    public IList<Cell> CachedNeighbors { get; set; }

    public void CalculateVeg()
    {
      var count = CachedNeighbors.Count(c => c.Vegetation > 0);
      Upcoming.Vegetation = (count == 3) || (count == 2) ? 10 : 0;
    }

    public void CommitVeg()
    {
      Vegetation = Upcoming.Vegetation;//TODO: consider changing _upcoming to just an int, if that's all we need here.
    }
  }
}
