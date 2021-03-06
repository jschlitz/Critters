﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Critters
{
  public class Field
  {
    public Field(int x, int y, Neighborhood n = Neighborhood.Moore, int neighborhoodSize=1)
    {
      RenderOneCommand = new RelayCommand(RenderOne);

      Neighborhood = n;
      NeighborhoodSize = neighborhoodSize;
      MaxX = x;
      MaxY = y;

      Cells = new Cell[x, y];
      for (int i = 0; i < x; i++)
      {
        for (int j = 0; j < y; j++)
        {
          Cells[i, j] = new Cell();
        }
      }
      for (int i = 0; i < x; i++)
      {
        for (int j = 0; j < y; j++)
        {
          CalculateNeighbors(i, j);
        }
      }
    }

    public Renderer Renderer { get; set; }
    public ICommand RenderOneCommand { get; private set;}

    private void RenderOne(object obj)
    {
      Tick();
      Renderer.Render(this);
    }

    public void CalculateNeighbors(int x, int y)
    {
      Cells[x,y].CachedNeighbors = new List<Cell>();

      //TODO: collapse:
      if (Neighborhood == Neighborhood.Moore)
      {
        for (int xc = -1 * NeighborhoodSize; xc <= NeighborhoodSize; xc++)
        {
          for (int yc = -1 * NeighborhoodSize; yc <= NeighborhoodSize; yc++)
            Cells[x, y].CachedNeighbors.Add(Cells[(xc + x + MaxX) % MaxX, (yc + y + MaxY) % MaxY]);
        }
      }
      else
      {
        for (int xc = -1 * NeighborhoodSize; xc <= NeighborhoodSize; xc++)
        {
          for (int yc = -1 * NeighborhoodSize; yc <= NeighborhoodSize; yc++)
            if (Math.Abs(xc) + Math.Abs(yc) <= NeighborhoodSize)
              Cells[x, y].CachedNeighbors.Add(Cells[(xc + x + MaxX) % MaxX, (yc + y + MaxY) % MaxY]);
        }
      }
      Cells[x, y].CachedNeighbors.Remove(Cells[x, y]);
    }

    public void Tick()
    {
      foreach (var c in Cells)
      {
        c.CalculateVeg();
      }
      foreach (var c in Cells)
      {
        c.CommitVeg();
      }
    }

    public int MaxX { get; private set; }
    public int MaxY { get; private set; }
    public int NeighborhoodSize { get; private set; }
    public Neighborhood Neighborhood { get; private set; }
    public Cell[,] Cells { get; private set; }
  }

  public enum Neighborhood{ Moore, VonNeumann};
}
/*

  */
