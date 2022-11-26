using Godot;
using System.Threading.Tasks;

public partial class DeathHandler : Node
{
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetParent().GetNode<AnimationPlayer>("./Head/Camera3d/FadeOverlay/AnimationPlayer");
    }

    public void Die()
    {
        _animationPlayer.Play("DeathFade");
        Task.Delay(1500)
            .ContinueWith(t =>
            {
                var levelManager = GetTree().Root
                    .GetNode<LevelManager>("./LevelManager");
                levelManager.RestartLevel();
            });
    }
}
