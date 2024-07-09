using Godot;
using Godot.Collections;
using RMC.Core.Events;

namespace RMC.Racing2D.Tracks
{
    public partial class NodeEvent : RmcEvent<Node>{}
        
    public partial class TrackStartingArea : Node3D
    {
        public NodeEvent OnBodyEntered = new NodeEvent();
        public NodeEvent OnBodyExited = new NodeEvent();

        [Export]
        public Area3D Area3D;
        
        [Export] 
        public Array<Node3D> StartingPoints;
        
        public override void _Ready()
        {
            Area3D.Connect("body_entered", 
                new Callable(this, nameof(Area3D_OnBodyEntered)));
            Area3D.Connect("body_exited", 
                new Callable(this, nameof(Area3D_OnBodyExited)));
        }

        private void Area3D_OnBodyEntered(Node body)
        {
            OnBodyEntered.Invoke(body);
        }

        private void Area3D_OnBodyExited(Node body)
        {
            OnBodyExited.Invoke(body);
        }
    }
}