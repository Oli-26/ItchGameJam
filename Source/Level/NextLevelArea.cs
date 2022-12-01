using Godot;
using System;
using System.Threading.Tasks;

public partial class NextLevelArea : Area3D
{
    private LevelManager _levelManager;

    [Export] public string NextLevelPath { get; set; }

    private bool _triggered = false;

    public override void _Ready()
	{
        _levelManager = GetTree().Root.GetNode<LevelManager>("LevelManager");
        BodyEntered += OnEntered;
    }

	private void OnEntered(Node3D body)
    {
        if (_triggered)
        {
            return;
        }
        if (!string.IsNullOrEmpty(NextLevelPath)
            && body.Name.ToString().StartsWith("player", StringComparison.InvariantCultureIgnoreCase))
        {
            _triggered = true;
            var animationPlayer = body.GetNode<AnimationPlayer>("./Head/Camera3d/FadeOverlay/AnimationPlayer");
            animationPlayer.Play("DeathFade");
            Task.Delay(3000)
                .ContinueWith(t =>
                {
                    _levelManager.SetActiveScene(NextLevelPath);
                });
        }
    }
}
