using Godot;
using System;

public partial class Main : Node3D
{

    [Export]
    private Player _player;
    
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here

        GD.Print("Main._Ready() Player = " + _player);
        
        _player.CallMe(10);
        
        //connect signal to player event
        _player.Connect("mouse_entered", Callable.From(Player_OnMouseEntered));
    }

    public void _Process(float delta)
    {
        // Called every frame. Delta is the elapsed time since the previous frame.
        // Update game logic here.
    }

    public void CustomMethodTest()
    {
        int customLocalVar = 10;
        
    }

    public void Player_OnMouseEntered()
    {
        GD.Print("test");
    }
}
