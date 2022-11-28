using Godot;
using System;

public partial class PlayerHealth : Node
{
    private Random random = new Random();
    private DeathHandler _deathHandler;
    private TextureRect _damageEffect;

    [Export] public AudioStream GaspSound { get; set; }

    public float Health { get; private set; } = 100f;

    public override void _Ready()
    {
        _deathHandler = GetParent().GetNode<DeathHandler>("./DeathHandler");
        _damageEffect = GetParent().GetNode<TextureRect>("./Head/Camera3d/DamageOverlay/TextureRect");
    }

    public void Damage(float damage)
    {
        if (Health == 0f)
        {
            return;
        }

        Health -= damage;
        Health = Math.Max(Health, 0f);
        if (Health > 0f)
        {
            if (GaspSound != null)
            {
                (this.GetParent() as Node3D).PlaySound(GaspSound);
            }
        }
        else
        {
            _deathHandler.Die();
        }
        UpdateOverlay();
        GD.Print(Health);
    }

    public override void _Process(double delta)
    {
        Health += (float)(3.0 * delta);
        Health = Math.Min(Health, 100);
        if (random.Next(10) == 0)
        {
            UpdateOverlay();
        }
    }


    private void UpdateOverlay()
    {
        _damageEffect.SelfModulate = new Color(1f, 1f, 1f, (100 - Health) / 100f);
    }

}