using Godot;
using System;

public partial class Scene02_Game : Node3D
{

    [Export]
    private Track _track;
    
    [Export]
    private LocalPlayerController _localPlayerController;
    
    public override void _Ready()
    {
    }

    private string _lastName = "";
    public override void _Process(double delta)
    {
        base._Process(delta);
        
            var name = _track.GetPlayerCurrentMeshName(_localPlayerController.Position);
        
        if (_lastName != name)
        {
            //GD.Print(name);
            _lastName = name;
        }
    }
}
