using Godot;
using System;

public partial class NewGameButton : Button
{
    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        var levelManager = GetTree().Root
            .GetNode<LevelManager>("./LevelManager");
        levelManager.SetActiveScene(Levels.Phase1Intro);
    }
}
