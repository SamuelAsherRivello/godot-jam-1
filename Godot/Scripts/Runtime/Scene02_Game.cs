using Godot;
using System;

public partial class Scene02_Game : Node3D
{

    [Export]
    private Track _track;
    
    [Export]
    private ControllableVehicle _controllableVehicle;
    
    public override void _Ready()
    {
    }

    private string _lastName = "";
    public override void _Process(double delta)
    {
 
    }
}
