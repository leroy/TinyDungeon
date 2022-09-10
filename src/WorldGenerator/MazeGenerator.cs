using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using TinyDungeon.Grid;
using TinyDungeon.World;
using Corner = TinyDungeon.Grid.Corner;
using Tile = TinyDungeon.Grid.Tile;

namespace TinyDungeon.WorldGenerator
{
	public class MazeGenerator : IStepableGenerator, IWorldGenerator
	{
		private RandomNumberGenerator _randomNumberGenerator;
		
		private Grid.Grid _grid;
		
		private Tile start;

		private List<Tile> _queue;

		private List<Edge> _openWalls;

		private List<Tile> _path;

		private List<Tile> _ignore;

		private int _iterations;

		public MazeGenerator(Grid.Grid grid)
		{
			_randomNumberGenerator = new RandomNumberGenerator();
			_grid = grid;
			
			Reset();
		}

		public void Reset()
		{
			_queue = new List<Tile>();
			_openWalls = new List<Edge>();
			_path = new List<Tile>();
			_ignore = new List<Tile>();
			
			start = _grid.GetRandomTile();
			

			_iterations = 0;

			_path.Add(start);
			_queue.Add(start);
		}

		public void Step()
		{
			if (IsFinished())
			{
				return;
			}
			
			_iterations++;
			GD.Print($"-- Step ({_iterations}) --");
			GD.Print($"{_queue.Count} left in queue");
			
			Tile tile = _queue.Last();


			if (_ignore.Contains(tile))
			{
				_queue.Remove(tile);
				return;
			}
			
			Tile neighbour;

			try
			{
				neighbour = GetRandomNeighbour(tile);
			}
			catch
			{
				_queue.Remove(tile);
				_ignore.Add(tile);
				return;
			}

			if (!_grid.WithinBounds(neighbour.Cell))
			{
				GD.Print($"{neighbour} not within bounds");
				return;
			}
			
			if (!HasVisited(tile, neighbour))
			{
				CarvePath(tile, neighbour);

				_queue.Add(neighbour);

				return;
			}

			_queue.Remove(tile);
		}

		public void Generate()
		{
			while (!IsFinished())
			{
				Step();
			}
		}

		public bool IsFinished()
		{
			return _queue.Count == 0;
		}
		
		public TileGrid GetResults()
		{
			TileGrid grid = new TileGrid();
			
			foreach (Tile tile in _path)
			{
				GD.Print(tile.Cell.Position);

				try
				{
					grid.SetTile(tile.Cell.Position, TileType.FLOOR);
				}
				catch
				{
					continue;
				}

				foreach (Edge edge in tile.Edges)
				{
					try
					{
						grid.SetTile(edge.Cell.Position, TileType.WALL);
					}
					catch
					{
						// ignored
					}
				}

				foreach (Corner corner in tile.Corners)
				{
					try
					{
						grid.SetTile(corner.Cell.Position, TileType.WALL);
					}
					catch
					{
						// ignored
					}
				}
				
			}

			foreach (Edge edge in _openWalls)
			{
				grid.SetTile(edge.Cell.Position, TileType.FLOOR);
			}

			return grid;
		}

		private void CarvePath(Tile from, Tile to)
		{
			Edge edge = from.EdgeBetween(to);
			
			GD.Print($"Carving path at: {edge} - from: {from} - to: {to}");

			if (_openWalls.Contains(edge))
			{
				return;
			}

			_openWalls.Add(edge);
			_path.Add(to);
		}

		private bool HasVisited(Tile from, Tile tile)
		{
			Edge edge = from.EdgeBetween(tile);

			if (_openWalls.Contains(edge))
			{
				return true;
			}

			// if (_path.Contains(tile))
			// {
			//     return true;
			// }

			return false;
		}

		private Tile GetRandomNeighbour(Tile tile)
		{
			Tile[] neighbours = tile.Neighbours.Where((Tile neighbour) => _grid.WithinBounds(neighbour.Cell) && !HasVisited(tile, neighbour)).ToArray();

			if (neighbours.Length == 0)
			{
				throw new Exception("No available neighbours");
			}
			
			return neighbours[_randomNumberGenerator.RandiRange(0, neighbours.Length - 1)];
		}
	}
}
