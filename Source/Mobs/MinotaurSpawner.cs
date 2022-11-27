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
        GD.Print("aaaa");
        _spawnScene = ResourceLoader.Load<PackedScene>("res://Scenes/Mobs/Minotaur.tscn");
		CallDeferred("Spawn");
    }

	public void Spawn()
	{
		GD.Print("spawner pos:" + this.GlobalPosition);
		GD.Print(GetTree().Root
			.GetNode<Node3D>("Level").GlobalPosition);
		//var r = Random.NextDouble();
		//GD.Print(r + " / " + InitialSpawnChance);
  //      if (r > InitialSpawnChance)
		//{
		//	GD.Print("Spawn check fail");
		//	return;
		//}
		GD.Print("Spawn check pass");
		var node = (Node3D)_spawnScene.Instantiate();
		GetTree().Root
			.GetNode("Level")
			.AddChild(node);
		CallDeferred("Initialise", node);
    }

	public void Initialise(Node3D node)
	{
        node.Position = this.GlobalPosition;
		Task.Delay(1000).ContinueWith(t =>
		{
			GD.Print("new pos:" + node.GlobalPosition);

		});
    }
}
