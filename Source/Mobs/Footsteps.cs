using Godot;
using System;
using System.Threading.Tasks;

public enum WalkState
{
    Idle,
    Walking,
    Running
}

public partial class Footsteps : Node3D
{
    private Random _random;
    private AudioStreamPlayer3D _audioStreamPlayer3D;
    private AnimationPlayer _animationPlayer;

    private float _timer = 0f;

    [Export] public AudioStream[] WalkFootstepSounds { get; set; } = new AudioStream[0];
    [Export] public AudioStream[] RunFootstepSounds { get; set; } = new AudioStream[0];

    [Export] public float DelayWalk { get; set; } = 1.38f;
    [Export] public float DelayRun { get; set; } = 0.5f;

    public WalkState WalkState { get; set; } = WalkState.Idle;

    public override void _Ready()
    {
        _random = new Random();
    }

    public override void _Process(double delta)
    {
        _timer -= (float)delta;
        if (_timer < 0f)
        {
            TakeNextStep();
        }
    }

    private void TakeNextStep()
    {
        var delay = DelayWalk;
        switch (WalkState)
        {
            case WalkState.Idle:
                break;
            case WalkState.Running:
                this.PlaySound(RunFootstepSounds[_random.Next(RunFootstepSounds.Length)], -20);
                delay = DelayRun;
                break;
            case WalkState.Walking:
                this.PlaySound(WalkFootstepSounds[_random.Next(WalkFootstepSounds.Length)], -20);
                break;
        }
        _timer += delay;
    }

}
