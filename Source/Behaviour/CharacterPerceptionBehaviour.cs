using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CharacterPerceptionBehaviour : Behaviour
{
	private VisionManager _visionManager;

	[Export] public float ViewRange { get; set; } = 10f;

	public override void _Ready()
	{
		base._Ready();
		_visionManager = GetNode<VisionManager>("../VisionManager");
		if (_visionManager == null)
		{
			throw new Exception("No vision manager - this must exist on mob");
		}
	}

	public override void _Process(double delta)
	{
		if (MobController == null)
		{
			return;
		}

		var root = GetTree().Root.GetNode("Level");
		var players = root.GetChildren()
			.Where(x => ((string)x.Name).StartsWith("Player", StringComparison.InvariantCultureIgnoreCase))
			.Cast<Node3D>();
		foreach (var player in players)
        {

            if (PlayerIsWithinRange(player)
				&& _visionManager.CanDetect(player))
			{
				MobController.OfferIntent(new ChasePlayerIntent(player));
            }
		}
	}

	private bool PlayerIsWithinRange(Node3D player)
	{

        var distanceToTarget = (player.GlobalPosition - Mob.GlobalPosition).Length();
		return distanceToTarget <= ViewRange;
    }
}
