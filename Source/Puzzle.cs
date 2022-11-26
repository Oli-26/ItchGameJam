using Godot;
using System;

public partial class Puzzle : Node3D, IActivatable
{
    private SpotLight3D _spotLight;

    [Export] public Node3D LeverA { get; set; }

    [Export] public Node3D LeverB { get; set; }

    [Export] public Node3D LeverC { get; set; }

    [Export] public Node3D LeverD { get; set; }

    [Export] public Node3D LeverE { get; set; }



    [Export] public Node3D TargetA { get; set; }
    [Export] public Node3D TargetB { get; set; }
    [Export] public Node3D TargetC { get; set; }
    [Export] public Node3D TargetD { get; set; }
    [Export] public Node3D TargetE { get; set; }
    [Export] public Node3D TargetF { get; set; }
    [Export] public Node3D TargetG { get; set; }
    [Export] public Node3D TargetH { get; set; }
    [Export] public Node3D TargetI { get; set; }


    private bool _passed = false;

    public bool Passed
    {
        get => _passed;
        set
        {
            if (_passed == value)
            {
                return;
            }
            _passed = value;
            OnStateChange();
        }
    }

    public override void _Ready()
	{
        _spotLight = GetNode<SpotLight3D>("SpotLight3D");
        _spotLight.LightColor = new Color(1.0f, 0.0f, 0.0f);

    }

    public override void _Process(double delta)
    {
        var random = new Random();
        if (random.Next() % 10 != 0)
        {
            return;
        }

        var leverA = (LeverA as Lever);
        var leverB = (LeverB as Lever);
        var leverC = (LeverC as Lever);
        var leverD = (LeverD as Lever);
        var leverE = (LeverE as Lever);
        Passed = (leverA.Active && leverB.Active && leverC.Active && leverD.Active && leverE.Active);
    }

    private void OnStateChange()
    {
        if (Passed)
        {
            _spotLight.LightColor = new Color(0.0f, 1.0f, 0.0f);
            (TargetA as IActivatable)?.Activate(true, this, null);
            (TargetB as IActivatable)?.Activate(true, this, null);
            (TargetC as IActivatable)?.Activate(true, this, null);
            (TargetD as IActivatable)?.Activate(true, this, null);
            (TargetE as IActivatable)?.Activate(true, this, null);
            (TargetF as IActivatable)?.Activate(true, this, null);
            (TargetG as IActivatable)?.Activate(true, this, null);
            (TargetH as IActivatable)?.Activate(true, this, null);
            (TargetI as IActivatable)?.Activate(true, this, null);
        }
        else
        {
            _spotLight.LightColor = new Color(1.0f, 0.0f, 0.0f);
        }
    }

    public void Activate(bool isActive, Node3D source, CharacterBody3D invoker)
    {
    }
}
