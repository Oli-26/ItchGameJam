using Godot;
using System;

public partial class TrapArea : Area3D
{
    public override void _Ready()
    {
        BodyEntered += OnEntered;
    }

    private void OnEntered(Node3D body)
    {
        if (body.Name.ToString().StartsWith("player", StringComparison.InvariantCultureIgnoreCase))
        {
            body.GetNode<PlayerHealth>("./PlayerHealth")
                .Damage(35);
            ((CharacterBody3D)body).Velocity = Vector3.Up * 5;
        }
    }
}
