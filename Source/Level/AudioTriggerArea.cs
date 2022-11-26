using Godot;
using System;

public partial class AudioTriggerArea : Area3D
{
	private AudioStreamPlayer3D _audioStreamPlayer3D;
	[Export] AudioStream Sound { get; set; }
	[Export] bool SingleUse { get; set; } = true;

	private bool _triggered = false;

	public override void _Ready()
	{
		_audioStreamPlayer3D = GetNode<AudioStreamPlayer3D>("./AudioStreamPlayer3D");
		_audioStreamPlayer3D.Stream = Sound;
        BodyEntered += OnEntered;
    }

	private void OnEntered(Node3D body)
    {
		if (_triggered && SingleUse)
		{
			return;
		}
		if (body.Name.ToString().StartsWith("player", StringComparison.InvariantCultureIgnoreCase))
		{
			_triggered = true;
            _audioStreamPlayer3D.Play();
		}
    }
}
