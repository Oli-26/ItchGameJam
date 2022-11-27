using Godot;

public partial class Gateway : Node3D, IActivatable
{
	private AnimationPlayer _animationPlayer;
	private CharacterBody3D _lastInvoker;
	private bool _open = false;

	[Export] public bool Open { get => _open; set => UpdateOpen(value); }

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("./AnimationPlayer");
	}

	public void UpdateOpen(bool open)
	{
		_open = open;
		PlayAnimation();
	}

	private void PlayAnimation()
	{
		if (Open)
		{
			_animationPlayer.Play("Open");
		}
		else
		{
			_animationPlayer.Play("Close");
		}
	}

	public void Activate(bool isActive, Node3D source, CharacterBody3D invoker)
	{
		Open = isActive;
	}
}
