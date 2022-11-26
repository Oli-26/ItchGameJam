using Godot;
using System;

public partial class PlayerHealth : Node
{
    private DeathHandler _deathHandler;
    private AnimationPlayer _effectAnimationPlayer;

    public float Health { get; private set;  } = 100f;

    public override void _Ready()
    {
        _deathHandler = GetParent().GetNode<DeathHandler>("./DeathHandler");
        _effectAnimationPlayer = GetParent().GetNode<AnimationPlayer>("./Head/Camera3d/DamageOverlay/AnimationPlayer");
    }

    public void Damage(float damage)
    {
        if (Health == 0f)
        {
            return;
        }

        Health -= damage;
        Health = Math.Max(Health, 0f);
        if (Health == 0f)
        {
            _deathHandler.Die();
        }
        if (Health < 100f)
        {
            _effectAnimationPlayer.Play("FadeIn");
        }
    }

}
