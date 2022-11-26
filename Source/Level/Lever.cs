using Godot;
using System;
using System.Linq;

public partial class Lever : Node3D, IActivatable
{
	private AnimationPlayer _animationPlayer;
	private MeshInstance3D _leverMesh;
	private Material _leverMaterial;
	private Shader _shader;
	private CharacterBody3D _lastInvoker;

	private bool _active = false;

	[Export] public Node Target { get; set; }
	[Export] public bool Active { get => _active; set => UpdateActive(value); }

	private bool _showInteractive = true;
	public bool ShowInteractive { 
		get => _showInteractive; 
		private set
		{
			if (_showInteractive == value)
			{
				return;
			}
			_showInteractive = value;

			ToggleShader();
        }
	}

	private void ToggleShader()
    {
		var material = _showInteractive
			? new ShaderMaterial()
			{
				Shader = _shader,
				NextPass = _leverMaterial
			}
			: _leverMaterial;
		_leverMesh.SetSurfaceOverrideMaterial(0, material);
    }

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("./AnimationPlayer");
        _leverMesh = GetNode<MeshInstance3D>("./Lever");
		_leverMaterial = _leverMesh.GetSurfaceOverrideMaterial(0);
		_shader = ResourceLoader.Load<Shader>("res://Shaders/outline.gdshader");

        ToggleShader();
        PlayAnimation();
    }

    public override void _Process(double delta)
    {
		ShowInteractive = this.GetNearbyPlayers(2f).Any();
    }

	public void UpdateActive(bool active)
	{
		_active = active;
		PlayAnimation();
        if (Target != null)
        {
            var activatable = Target as IActivatable;
            activatable.Activate(Active, this, _lastInvoker);
        }
    }

	private void PlayAnimation()
	{
        if (Active)
        {
            _animationPlayer.Play("PushDown");
        }
        else
        {
            _animationPlayer.Play("PullUp");
        }
    }

	public void Use(CharacterBody3D invoker)
	{
		GD.Print("used");
		GD.Print(invoker.GetType().Name);
		_lastInvoker = invoker;
        Active = !Active;
	}

	public void Activate(bool isActive, Node3D source, CharacterBody3D invoker)
	{
		Active = !Active;
	}
}
