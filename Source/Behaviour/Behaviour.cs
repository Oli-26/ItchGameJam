﻿using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract partial class Behaviour : Node
{
	protected MobController MobController { get; private set; }
    protected CharacterBody3D Mob { get; private set; }

    public override void _Ready()
    {
        MobController = GetParent<MobController>();
        Mob = MobController.GetParent<CharacterBody3D>();
    }
}
