using Godot;
using System;

public partial class MusicManager : Node
{
	private AnimationPlayer _animationPlayer;

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("./AnimationPlayer");
		_animationPlayer.AnimationFinished += _animationPlayer_AnimationFinished;

        _animationPlayer.Play("Default");

    }

	private void _animationPlayer_AnimationFinished(StringName animName)
	{
		_animationPlayer.Play("Default");
	}
}
