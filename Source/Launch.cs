using Godot;
using System;

public partial class Launch : Node3D
{
	public override void _Ready()
	{
		CallDeferred("Start");
	}

	private void Start()
	{
        var levelManager = GetTree().Root
            .GetNode<LevelManager>("./LevelManager");
        levelManager.SetActiveScene(Levels.MainMenu);
	}
}
