using Godot;
using System;

public partial class MinotaurSpawner : Node3D
{
	private static readonly Random Random = new Random();
	private PackedScene _spawnScene;

	[Export] public float InitialSpawnChance { get; set; } = 1.0f;

	public override void _Ready()
	{
		_spawnScene = ResourceLoader.Load<PackedScene>("res://Scenes/Mobs/Minotaur.tscn");
		CallDeferred("Spawn");
    }

	public void Spawn()
	{
		if (Random.NextDouble() > InitialSpawnChance)
		{
			return;
		}
		var node = (Node3D)_spawnScene.Instantiate();
		node.Position = GlobalPosition;
		GetTree().Root
			.GetNode("Level")
			.AddChild(node);
    }
}
