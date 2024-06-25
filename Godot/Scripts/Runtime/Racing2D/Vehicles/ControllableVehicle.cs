using Godot;
using RMC.Racing2D.Players;

namespace RMC.Racing2D.Vehicles
{
    public partial class ControllableVehicle : VehicleBody3D
    {
        [Export] private CarCharacteristics CarCharacteristics;

        protected void SetInputs(float newSteeringInput, float newAccelerationInput)
        {
            _currentSteeringInput = Mathf.Clamp(newSteeringInput, -1.0f, 1.0f);
            _currentAccelerationInput = Mathf.Clamp(newAccelerationInput, -1.0f, 1.0f);
        }

        private float _currentSteeringInput = 0.0f;
        private float _currentAccelerationInput = 0.0f;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            ApplyCharacteristics();
        }

        private void ApplyCharacteristics()
        {
            Mass = CarCharacteristics.TotalMass;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }

        public override void _PhysicsProcess(double delta)
        {
            ApplyExternalInput();
        }

        private void ApplyExternalInput()
        {
            Steering = _currentSteeringInput * CarCharacteristics.MaxSteeringValue;
            EngineForce = _currentAccelerationInput * CarCharacteristics.MaxEngineStrength;
        }
    }
}