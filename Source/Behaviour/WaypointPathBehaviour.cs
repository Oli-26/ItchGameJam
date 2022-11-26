using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class WaypointPathBehaviour : Behaviour
{
    private List<Node> _waypoints { get; set; } = new List<Node>();
    [Export] public Node WaypointA { get; set; }
    [Export] public Node WaypointB { get; set; }
    [Export] public Node WaypointC { get; set; }
    [Export] public Node WaypointD { get; set; }
    [Export] public Node WaypointE { get; set; }
    [Export] public Node WaypointF { get; set; }

    private int _currentTarget = 0;

    public override void _Ready()
	{
		base._Ready();

        if (WaypointA != null)
        {
            _waypoints.Add(WaypointA);
        }
        if (WaypointB != null)
        {
            _waypoints.Add(WaypointB);
        }
        if (WaypointC != null)
        {
            _waypoints.Add(WaypointC);
        }
        if (WaypointD != null)
        {
            _waypoints.Add(WaypointD);
        }
        if (WaypointE != null)
        {
            _waypoints.Add(WaypointE);
        }
        if (WaypointF != null)
        {
            _waypoints.Add(WaypointF);
        }
    }

	public override void _Process(double delta)
	{
        if (MobController.Intent is SeekWaypointIntent)
        {
            DisengageFromWaypoint();
        }
        if (MobController.Intent == null)
        {
            SeekNextWaypoint();
        }
    }

    private void DisengageFromWaypoint()
    {
        var distanceToTarget = (MobController.Intent.Position - Mob.Position).Length();
        if (distanceToTarget < 0.5f)
        {
            MobController.ClearIntent(1);
        }
    }

    private void SeekNextWaypoint()
    {
        if (_waypoints.Count == 0)
        {
            return;
        }
        _currentTarget = (_currentTarget + 1) % _waypoints.Count;
        var target = _waypoints[_currentTarget];
        MobController.OfferIntent(new SeekWaypointIntent((target as Node3D).GlobalPosition));
    }
}
