using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class GravityBehaviour : Behaviour
{
    [Export] public float Gravity { get; set; } = 9.81f;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Mob.Velocity;
        if (!Mob.IsOnFloor())
        {
            velocity += Vector3.Down * Gravity * (float)delta;
        }
        else
        {
            if (velocity.y > 0)
            {
                velocity.y = 0;
            }
        }
        Mob.Velocity = velocity;
    }

}
