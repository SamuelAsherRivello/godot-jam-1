using Godot;
using RMC.Racing2D.Vehicles;
using System;

namespace RMC.Racing2D.Players
{
    public partial class AIControllableVehicle : ControllableVehicle
    {
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            base._Process(delta);
        }

        public override void _PhysicsProcess(double delta)
        {
            SetInputs(1.0f, 1.0f);

            base._PhysicsProcess(delta);
        }

        public override ControllableVehicleType GetControllableVehicleType()
        {
            return ControllableVehicleType.AI_PLAYER;
        }
    }
}

