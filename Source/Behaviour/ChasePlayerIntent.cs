using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChasePlayerIntent : IIntent
{
	private readonly Node3D _node;
	public int Priority => 10;

    public Vector3 Position => _node.Position;
	public PlayerHealth Health => _node.GetNode<PlayerHealth>("./PlayerHealth");


	public ChasePlayerIntent(Node3D node)
	{
		_node = node;
	}
}
