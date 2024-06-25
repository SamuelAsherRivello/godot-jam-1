using Godot;
using RMC.Mingletons;
using RMC.Racing2D.Audio;

namespace RMC.Racing2D.Vehicles
{
    public partial class ControllableVehicle : VehicleBody3D
    {
        [Export] private CarCharacteristics CarCharacteristics;
        [Export] private Area3D _collisionArea3D;
        
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
            
            //TODO: Not sure why I need #1 "collisionArea.Connect"
            //and can't just use #2 "this.Connect", but only #1 works.
            _collisionArea3D.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
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
        
        private void OnBodyEntered(Node body)
        {
            //GD.Print($"Collided with: {body.Name}");
            Mingleton.Instance.GetSingleton<AudioManager>().PlayAudio("Hit01.mp3");
        }
    }
}