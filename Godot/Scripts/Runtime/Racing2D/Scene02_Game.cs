using Godot;
using System;
using RMC.Racing2D.Players;
using RMC.Racing2D.Vehicles;

namespace RMC.Racing2D
{
    public partial class Scene02_Game : Node3D
    {

        [Export] private Track _track;

        [Export] private ControllableVehicle _controllableVehicle;

        public override void _Ready()
        {
        }

        private string _lastMeshName = "";
        private float _lastRotationInDegrees = -1;

        public override void _Process(double delta)
        {
            base._Process(delta);

            var meshName = _track.GetMeshNameAtPosition(_track.FlowGridMap, _controllableVehicle.Position);
            var rotationInDegrees =
                _track.GetMeshRotationInDegreesAtPosition(_track.FlowGridMap, _controllableVehicle.Position);

            if (_lastMeshName != meshName)
            {
                GD.Print("meshName: " + meshName);
                _lastMeshName = meshName;
            }

            if (Math.Abs(_lastRotationInDegrees - rotationInDegrees) > 0.1f)
            {
                GD.Print("rotationInDegrees: " + rotationInDegrees);
                _lastRotationInDegrees = rotationInDegrees;
            }
        }
    }
}