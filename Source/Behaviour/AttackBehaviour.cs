using Godot;
using System;
using System.Threading.Tasks;

public partial class AttackBehaviour : Behaviour
{
	private bool _cooldown = false;
	[Export] public float Damage { get; set; } = 50f;
	[Export] public float Range {get; set; } = 1.5f;

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
		targetDirection.y = 0;

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

		intent.Health.Damage(100f);
        GD.Print("attacking");
	}
}
