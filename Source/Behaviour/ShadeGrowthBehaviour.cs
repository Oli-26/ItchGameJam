using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ShadeGrowthBehaviour : Behaviour
{
    private NavigationBehaviour _navigationBehaviour;
    private FogVolume _fogVolume;

    [Export] public float SyphonRange { get; set; } = 5f;
    public float Progress { get; set; } = 0f;

    public override void _Ready()
    {
        base._Ready();
        _navigationBehaviour = GetNode<NavigationBehaviour>("../NavigationBehaviour");
        if (_navigationBehaviour == null)
        {
            throw new Exception("Navigation behaviour not defined");
        }
        _fogVolume = GetNode<FogVolume>("../FogVolume");
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

            if (PlayerIsWithinRange(player))
            {
                Progress += (float)delta * 0.5f;

            }
        }

        var relativeStrength = Math.Min(Progress, 10f) / 10f;

        _fogVolume.Extents = new Vector3(.5f + 5f * relativeStrength, 2f + 5f * relativeStrength, .5f + 5f * relativeStrength);
        var material = _fogVolume.Material as FogMaterial;
        material.Density = 1f + relativeStrength * 3f;
        _navigationBehaviour.ChaseSpeed = 2f * relativeStrength * 4f;
    }

    private bool PlayerIsWithinRange(Node3D player)
    {

        var distanceToTarget = (player.GlobalPosition - Mob.GlobalPosition).Length();
        return distanceToTarget <= SyphonRange;
    }
}
