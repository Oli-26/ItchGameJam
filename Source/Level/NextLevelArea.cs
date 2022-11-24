using Godot;
using System;

public partial class NextLevelArea : Area3D
{
    private LevelManager _levelManager;

    [Export] public string NextLevelPath { get; set; }

    public override void _Ready()
	{
        _levelManager = GetTree().Root.GetNode<LevelManager>("LevelManager");
        BodyEntered += OnEntered;
    }

	private void OnEntered(Node3D body)
    {
		if (!string.IsNullOrEmpty(NextLevelPath)
            && body.Name.ToString().StartsWith("player", StringComparison.InvariantCultureIgnoreCase))
        {
            _levelManager.SetActiveScene(NextLevelPath);
        }
    }
}
