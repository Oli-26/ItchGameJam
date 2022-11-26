using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class GameHelpers
{
    public static IEnumerable<CharacterBody3D> GetNearbyPlayers(this Node3D node, float radius)
    {
        IList<CharacterBody3D> players = new List<CharacterBody3D>();
        var root = node.GetTree().Root.GetNode("Level");
        var candidates = root.GetChildren()
            .Where(x => ((string)x.Name).StartsWith("Player", StringComparison.InvariantCultureIgnoreCase))
            .Cast<Node3D>();
        foreach (var player in candidates)
        {
            if (PlayerIsWithinRange(node, player, radius))
            {
                players.Add(player as CharacterBody3D);
            }
        }
        return players.ToArray();
    }

    private static bool PlayerIsWithinRange(Node3D node, Node3D player, float radius)
    {

        var distanceToTarget = (player.GlobalPosition - node.GlobalPosition).Length();
        return distanceToTarget <= radius;
    }
}
