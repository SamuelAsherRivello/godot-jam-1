using Godot;
using RMC.Racing2D.Vehicles;

namespace RMC.Racing2D.Players
{
	public partial class LocalPlayerControllableVehicle : ControllableVehicle
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
            SetInputs(Input.GetAxis("SteerRight", "SteerLeft"),
                Input.GetAxis("Brake", "Accelerate"));

            base._PhysicsProcess(delta);
		}

        public override ControllableVehicleType GetControllableVehicleType()
        {
			return ControllableVehicleType.LOCAL_PLAYER;
        }
    }
}
