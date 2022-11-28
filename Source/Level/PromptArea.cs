using Godot;
using System;
using System.Threading.Tasks;

public partial class PromptArea : Area3D
{
    [Export] bool SingleUse { get; set; } = true;
    [Export] float OverlayExpireSeconds { get; set; } = 10f;

    public Node Content { get; private set; }

    private bool _triggered = false;

    public override void _Ready()
    {
        BodyEntered += OnEntered;
        Content = GetNode("./Content");
        if (Content == null)
        {
            throw new Exception($"No content defined in prompt area for: {GetPath()}");
        }
        (Content as CanvasLayer).Visible = false;
    }

    private void OnEntered(Node3D body)
    {
        if (_triggered && SingleUse)
        {
            return;
        }
        if (body.Name.ToString().StartsWith("player", StringComparison.InvariantCultureIgnoreCase))
        {
            _triggered = true;
            Apply(body as CharacterBody3D);

        }
    }

    private void Apply(CharacterBody3D player)
    {
        var overlayContainer = player.GetNode("./Head/Camera3d/Overlay");
        var ui = Content.Duplicate() as CanvasLayer;
        foreach (var child in overlayContainer.GetChildren())
        {
            child.QueueFree();
        }
        overlayContainer.AddChild(ui);
        ui.Visible = true;
        Task.Delay((int)(OverlayExpireSeconds * 1000))
            .ContinueWith(t =>
            {
                overlayContainer.RemoveChild(ui);
            });
    }
}
