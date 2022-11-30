using Godot;
using System;
using System.Threading.Tasks;

public partial class MinotaurSpawner : Node3D
{
	private static readonly Random Random = new Random();
	private PackedScene _spawnScene;

	[Export] public float InitialSpawnChance { get; set; } = 1.0f;

	public override void _Ready()
    {
        _spawnScene = ResourceLoader.Load<PackedScene>("res://Scenes/Mobs/Minotaur.tscn");
        Task.Delay(100).ContinueWith(t => CallDeferred("Spawn"));
    }

	public void Spawn()
	{
        var r = Random.NextDouble();
        GD.Print($"{r} > {InitialSpawnChance} : {r > InitialSpawnChance}");
        if (r > InitialSpawnChance)
        {
            return;
        }
        var node = (Node3D)_spawnScene.Instantiate();
        GetTree().Root
            .GetNode("Level")
            .AddChild(node);
        CallDeferred("Initialise", node);
    }

	public void Initialise(Node3D node)
	{
        node.GlobalPosition = this.GlobalPosition;
    }
}
