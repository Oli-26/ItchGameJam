using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class GravityBehaviour : Behaviour
{
    [Export] public float Gravity { get; set; } = 9.81f * 10;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        var velocity = Mob.Velocity;
        if (!Mob.IsOnFloor())
        {
            velocity.y -= Gravity * (float)delta;
            Mob.Velocity += Vector3.Down * Gravity * (float)delta;
        } 
        else
        {
            velocity.y = 0;
        }
        Mob.Velocity = velocity;
    }

}
