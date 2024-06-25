using Godot;
using RMC.Racing2D.Vehicles;

namespace RMC.Racing2D.Players
{
	public partial class LocalPlayerController : Node3D
	{
		[Export] ControllableVehicle controllableVehicle;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public override void _PhysicsProcess(double delta)
		{
			controllableVehicle.SetInputs(Input.GetAxis("SteerRight", "SteerLeft"),
				Input.GetAxis("Brake", "Accelerate"));
		}
	}
}
