using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class NavigationBehaviour : Behaviour
{
	[Export] public float WalkSpeed { get; set; } = 2f;
    [Export] public float ChaseSpeed { get; set; } = 7f;

	private static readonly float _turnFactor = 0.05f;

	private AnimationPlayer _animationPlayer;
	private NavigationAgent3D _navigationAgent3D;

	public override void _Ready()
	{
		base._Ready();
        _navigationAgent3D = Mob.GetNode<NavigationAgent3D>("./NavigationAgent3D");
		if (Mob.HasNode("./AnimationPlayer"))
		{
			_animationPlayer = Mob.GetNode<AnimationPlayer>("./AnimationPlayer");
            foreach (var animation in _animationPlayer.GetAnimationList())
            {
                GD.Print(animation);
            }
		}
		else
		{
			GD.Print("No animation player");
		}
	}

	public override void _Process(double delta)
	{
		if (MobController.Intent == null)
		{
			Mob.Velocity = Vector3.Zero;
            _animationPlayer?.Play(AnimationNames.Idle);
			return;
		}

		if (_navigationAgent3D == null)
		{
			return;
		}
		_navigationAgent3D.SetTargetLocation(MobController.Intent.Position);
		var nextLocation = _navigationAgent3D.GetNextLocation();

		var targetDirection = (nextLocation - Mob.Position);
		targetDirection.y = 0;

        if (targetDirection.Length() < 0.0001)
		{
			return;
		}

		var moveDirection = targetDirection.Normalized();
		var speed = MobController.IsAggressive ? ChaseSpeed : WalkSpeed;
		if (MobController.IsAggressive)
		{
			_animationPlayer?.Play(AnimationNames.RunForward, customSpeed: 0.6f);
		}
		else
		{
			_animationPlayer?.Play(AnimationNames.WalkForward, customSpeed: 0.3f);
		}
		var targetVelocity = moveDirection * speed;
		Mob.Velocity = MathHelpers.Lerp(Mob.Velocity, targetVelocity, 0.01f);

		var targetRotation = Mob.Transform.LookingAt(Mob.Position + moveDirection, Vector3.Up).basis.GetRotationQuaternion();
		var turnAmount = _turnFactor;
		Mob.Quaternion = (turnAmount * targetRotation + (1 - turnAmount) * Mob.Quaternion).Normalized();
	}

}
