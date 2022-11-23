using Godot;
using System;

public partial class Fluid : MeshInstance3D
{
	private double _time = 0.0;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		_time += delta;
		var material = GetSurfaceOverrideMaterial(0) as ShaderMaterial;
        material.SetShaderParameter("time", _time);
    }
}
