using Godot;
using System;

public class TestCamera : Camera2D
{
	private bool dragging = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetProcessInput(true);
		
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.GetType() == typeof(InputEventMouseButton))
		{
			if (@event.IsPressed())
			{
				dragging = true;
			}
			else
			{
				dragging = false;
			}

			return;
		}

		if (@event.GetType() == typeof(InputEventMouseMotion) && dragging)
		{
			Position += ((InputEventMouseMotion) @event).Speed;
		}
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
