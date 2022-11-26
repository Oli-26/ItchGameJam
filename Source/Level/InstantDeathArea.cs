using Godot;
using System;

public partial class InstantDeathArea : Area3D
{
    public override void _Ready()
    {
        BodyEntered += OnEntered;
    }

    private void OnEntered(Node3D body)
    {
        if (body.Name.ToString().StartsWith("player", StringComparison.InvariantCultureIgnoreCase))
        {
        }
    }
}
