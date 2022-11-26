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

    public override void _Process(double delta)
    {
        if (!Mob.IsOnFloor())
        {
            Mob.Velocity += Vector3.Down * Gravity * (float)delta;
        } 
        else
        {
            Mob.Velocity -= new Vector3(0, Mob.Velocity.y, 0);
        }
    }

}
