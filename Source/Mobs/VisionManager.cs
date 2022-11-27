using Godot;
using System;
using System.Linq;

public partial class VisionManager : Node3D
{
	[Export] public float ConeArc { get; set; } = 180;
	[Export] public float ProximityThreshold { get; set; } = 4.5f;

	public override void _Ready()
	{
	}

	public bool CanDetect(Node3D target)
	{
		return (IsInVisionCone(target)
			&& HasLineOfSight(target))
			|| IsWithinProximity(target);
	}

	private bool IsWithinProximity(Node3D target)
	{
		var distance = (GlobalTransform.origin - target.GlobalTransform.origin).Length();
		return distance <= ProximityThreshold;
	}

	private bool IsInVisionCone(Node3D target)
	{
		var forward = -GlobalTransform.basis.z;
		var thisPos = GlobalTransform.origin;
		var directionToPoint = target.Transform.origin - thisPos;

        return directionToPoint.AngleTo(forward) <= Mathf.DegToRad(ConeArc / 2);
	}

	private bool HasLineOfSight(Node3D target)
	{
		var spaceState = GetWorld3d().DirectSpaceState;
		var result = spaceState.IntersectRay(new PhysicsRayQueryParameters3D
		{
			From = GlobalTransform.origin,
			To = target.GlobalPosition
		});
		var collider = result["collider"];
        return collider.Obj == target;
	}
}
