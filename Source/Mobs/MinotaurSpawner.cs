using Godot;
using System;

public partial class MinotaurSpawner : Node3D
{
	private PackedScene _spawnScene;

	public override void _Ready()
	{
		_spawnScene = ResourceLoader.Load<PackedScene>("res://Scenes/Mobs/Minotaur.tscn");
		CallDeferred("Spawn");
    }

	public override void _Process(double delta)
	{

	}

	public void Spawn()
	{
		var node = (Node3D)_spawnScene.Instantiate();
		node.Position = GlobalPosition;
		GetTree().Root
			.GetNode("Level")
			.AddChild(node);
    }
}
