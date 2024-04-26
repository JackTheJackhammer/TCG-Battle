using Godot;
using System;

public partial class map : Node
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InitMap();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		return;
	}

	private void InitMap()
	{
		var scene = GD.Load<PackedScene>("res://Tile.tscn"); // Will load when the script is instanced.

		for (int x = 0; x < 20; x++)
		{
			for (int y = 0; y < 20; y++)
			{
				var inst = scene.Instantiate<Node2D>();
				inst.Position = new Vector2(x * 80, y * 80);
				AddChild(inst);


			}
		}
	}

}
