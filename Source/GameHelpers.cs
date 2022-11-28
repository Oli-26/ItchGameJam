using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


    public static void PlaySound(this Node3D node, AudioStream stream, float volumeDb = -15)
    {
        var audioStreamPlayer3D = new AudioStreamPlayer3D()
        {
            Stream = stream,
            VolumeDb = volumeDb
        };
        node.AddChild(audioStreamPlayer3D);
        audioStreamPlayer3D.Play();
        Task.Delay((int)(stream.GetLength() * 1000)).ContinueWith(t =>
        {
            audioStreamPlayer3D.QueueFree();
        });
    }
}
