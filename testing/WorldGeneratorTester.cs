using Godot;
using System;
using System.Collections.Generic;
using TinyDungeon;
using TinyDungeon.Grid;
using TinyDungeon.World;
using TinyDungeon.WorldGenerator;
using Tile = TinyDungeon.World.Tile;
using TileSet = TinyDungeon.World.TileSet;

public class WorldGeneratorTester : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Export]
	public NodePath world;

	private ViewportContainer _viewportContainer;

	private Viewport viewport;

	private Camera2D _camera;
	
	private DungeonGenerator _generator;

	private World worldScene;

	private TileMap _tileMap;

	private Vector2 _mousePosition;

	private Vector2 _selectedTilePosition;

	private Vector2 _prevSelectedTilePosition;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetProcessInput(true);

		_viewportContainer = GetNode<ViewportContainer>("ViewportContainer");
		viewport = GetNode<Viewport>("ViewportContainer/Viewport");
		_camera = GetNode<Camera2D>("ViewportContainer/Viewport/TouchCamera2D");
		
		viewport.Size = new Vector2(480, 320);

		_generator = new DungeonGenerator(new DungeonGenerator.DungeonGeneratorSettings());

		worldScene = GetNode<World>(world);

		_tileMap = worldScene.GetNode<TileMap>("TileMap");

		UpdateTileMap();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion)
		{
			_mousePosition = (((InputEventMouseMotion) @event).Position - _viewportContainer.RectGlobalPosition) / (GetViewportRect().Size / viewport.Size);
			_selectedTilePosition = _tileMap.WorldToMap(_mousePosition + _camera.GetCameraPosition());

			if (_selectedTilePosition != _prevSelectedTilePosition)
			{
				UpdateTileInfo();
			}
		}
	}

	private void UpdateTileInfo()
	{
		Label tileType = GetNode<Label>("TileInfoOverlay/MarginContainer/GridContainer/TileType");
		Label tilePosition = GetNode<Label>("TileInfoOverlay/MarginContainer/GridContainer/TilePosition");

		Tile tile = _generator.GetResults().Data.GetTile(_selectedTilePosition);

		tileType.Text = tile.Type.ToString();
		tilePosition.Text = tile.Position.ToString();
		
		_prevSelectedTilePosition = _selectedTilePosition;
	}


	private void _on_Reset_pressed()
	{
		_generator.Reset();
		UpdateTileMap();
	}


	private void _on_Generate_pressed()
	{
		_generator.Reset();
		_generator.Generate();
		UpdateTileMap();
	}

	private void _on_Step_pressed()
	{
		UpdateTileMap();
	}

	private void UpdateTileMap()
	{
		_tileMap.Clear();

		foreach (Tile tile in _generator.GetResults())
		{

			TileSetTile tileSetTile = TileSet.GetTileId(tile.Type);
			
			_tileMap.SetCell((int) tile.Position.x, (int) tile.Position.y, tileSetTile.Id, tileSetTile.FlipX, tileSetTile.FlipY, tileSetTile.Transpose);
		}
		
		// Tile[][] tiles = _generator.GetResults().Tiles;

		// for (int x = 0; x < tiles.Length; x++)
		// {
			// for (int y = 0; y < tiles[x].Length; y++)
			// {
				// tileMap.SetCell(x, y, TileSet.GetTileId(tiles[x][y].Type));

			// }
		// }
	}
}



