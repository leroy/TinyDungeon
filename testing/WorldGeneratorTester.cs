using Godot;
using System;
using TinyDungeon;

public class WorldGeneratorTester : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Export]
	public NodePath world;

	private Viewport viewport;
	
	private ViewportContainer viewportContainer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetProcessInput(true);
		
		viewportContainer = GetNode<ViewportContainer>("ViewportContainer");
		viewport = GetNode<Viewport>("ViewportContainer/Viewport");
		viewport.Size = new Vector2(480, 320);
		
		WorldGenerator worldGenerator = new WorldGenerator();


		World worldScene = GetNode<World>(world);

		TileMap tileMap = worldScene.GetNode<TileMap>("TileMap");

		TileGrid grid = worldGenerator.Generate();

		Tile[] tiles = grid.GetTiles();

		foreach(Tile tile in tiles) {
			tileMap.SetCell((int) tile.Position.x, (int) tile.Position.y, ((int)tile.Type));
		}
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
