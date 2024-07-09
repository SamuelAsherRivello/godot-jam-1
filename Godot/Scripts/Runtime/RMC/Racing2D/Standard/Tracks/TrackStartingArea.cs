using Godot;
using Godot.Collections;

namespace RMC.Racing2D.Tracks
{
    public partial class TrackStartingArea : Node3D
    {
        [Export]
        public Area3D Area3D;
        
        [Export] 
        public Array<Node3D> StartingPoints;
    }
}