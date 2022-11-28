using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CharacterPerceptionBehaviour : Behaviour
{
	private VisionManager _visionManager;

	[Export] public float ViewRange { get; set; } = 18f;
	[Export] public AudioStream AggroSound { get; set; }

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
		if (MobController.Intent is ChasePlayerIntent)
		{
			return;
		}

		foreach (var player in Mob.GetNearbyPlayers(ViewRange))
        {
            if (_visionManager.CanDetect(player))
			{
				MobController.OfferIntent(new ChasePlayerIntent(player));
				if (AggroSound != null)
                {
                    Mob.PlaySound(AggroSound, 40);
                }
            }
		}
	}
}
