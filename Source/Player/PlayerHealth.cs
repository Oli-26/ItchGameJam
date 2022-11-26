using Godot;
using System;

public partial class PlayerHealth : Node
{
    private DeathHandler _deathHandler;

    public float Health { get; private set;  } = 100f;

    public override void _Ready()
    {
        _deathHandler = GetParent().GetNode<DeathHandler>("./DeathHandler");
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
    }

}
