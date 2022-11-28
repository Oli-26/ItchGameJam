using Godot;
using System.Threading.Tasks;

public partial class DeathHandler : Node
{
    private AnimationPlayer _animationPlayer;
    [Export] public AudioStream ScreamSound { get; set; }

    public override void _Ready()
    {
        _animationPlayer = GetParent().GetNode<AnimationPlayer>("./Head/Camera3d/FadeOverlay/AnimationPlayer");
    }

    public void Die()
    {
        _animationPlayer.Play("DeathFade");
        if (ScreamSound != null)
        {
            (this.GetParent() as Node3D).PlaySound(ScreamSound);
        }
        Task.Delay(3000)
            .ContinueWith(t =>
            {
                var levelManager = GetTree().Root
                    .GetNode<LevelManager>("./LevelManager");
                levelManager.RestartLevel();
            });
    }
}
