using Godot;
using System;

public partial class AttackBehaviour : Behaviour
{
	[Export] public float damage { get; set; } = 50f;
	[Export] public float range {get; set; } = 10f;
	
	private NavigationAgent3D _navigationAgent3D;
	public override void _Ready()
	{
		_navigationAgent3D = Mob.GetNode<NavigationAgent3D>("./NavigationAgent3D");
	}

	public override void _Process(double delta)
	{
		var nextLocation = _navigationAgent3D.GetNextLocation();
		
		var targetDirection = (nextLocation - Mob.Position);
		targetDirection.y = 0;
		if (targetDirection.Length() < range)
		{
			Attack();
			return;
		}

	}
	
	public void Attack(){
		GD.Print("attacking");
	}
}
