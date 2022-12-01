using Godot;
using System;
using System.Threading.Tasks;

public partial class AttackBehaviour : Behaviour
{
	private bool _cooldown = false;
	[Export] public float Damage { get; set; } = 99f;
	[Export] public float Range {get; set; } = 1.8f;

	public override void _Process(double delta)
	{
		if (_cooldown)
		{
			return;
		}
		if (!(MobController.Intent is ChasePlayerIntent))
		{
			return;
		}

        var intent = (ChasePlayerIntent)MobController.Intent;
        var targetDirection = (Mob.GlobalPosition - intent.Position);

		if (targetDirection.Length() < Range)
		{
			Attack();
			_cooldown = true;
			Task.Delay(3000)
				.ContinueWith(t =>
				{
					_cooldown = false;
				});
		}
	}
	
	public void Attack()
    {
        var intent = (ChasePlayerIntent)MobController.Intent;
		if (intent == null)
		{
			return;
		}
		intent.Health.Damage(50f);

		var yeetVelocity = Vector3.Up * 10 + -Mob.GlobalTransform.basis.z * 25f;
        intent.CharacterBody.Velocity += yeetVelocity;
	}
}
