using Godot;
using System;

public partial class Mob : CharacterBody3D
{

	private MobController _mobController;

	public override void _Ready()
	{
		_mobController = GetNode<MobController>("./MobController");
	}

	public override void _PhysicsProcess(double delta)
    {
        if (MoveAndSlide())
        {
        }
    }
}
