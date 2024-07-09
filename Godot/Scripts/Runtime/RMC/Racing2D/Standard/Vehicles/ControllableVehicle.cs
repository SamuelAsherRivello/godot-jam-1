using Godot;
using RMC.Core.Audio;
using RMC.Mingletons;
using RMC.Racing2D.Tracks;

namespace RMC.Racing2D.Vehicles
{
    public abstract partial class ControllableVehicle : VehicleBody3D
    {
        [Export] private CarCharacteristics CarCharacteristics;
        [Export] private Area3D _collisionArea3D;
        [Export] private MeshInstance3D _bodyMeshInstance3D;

        public void SetBodyColor(Color color)
        {
            var activeMaterial = _bodyMeshInstance3D.GetActiveMaterial(0);
            var overrideMaterial = activeMaterial.Duplicate() as StandardMaterial3D;
            overrideMaterial.AlbedoColor = color;
            _bodyMeshInstance3D.SetSurfaceOverrideMaterial(0, overrideMaterial);
        }

        public enum ControllableVehicleType
        {
            LOCAL_PLAYER,
            AI_PLAYER
        }

        public bool IsEnabled { get; set; } = false;
        public int LapCurrent { get; set; } = 0;
        public bool HasExitedStartingArea { get; set; } = false;

        public abstract ControllableVehicleType GetControllableVehicleType();

        protected void SetInputs(float newSteeringInput, float newAccelerationInput)
        {
            _currentSteeringInput = Mathf.Clamp(newSteeringInput, -1.0f, 1.0f);
            _currentAccelerationInput = Mathf.Clamp(newAccelerationInput, -1.0f, 1.0f);
        }

        private float _currentSteeringInput = 0.0f;
        private float _currentAccelerationInput = 0.0f;
        private Track _track;

        protected Track GetTrack() { return _track; }

        public void SetupControllableVehicle(Track newTrack)
        {
            _track = newTrack;
        }

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
            if (!IsEnabled)
            {
                Steering = 0;
                EngineForce = 0;
                return;
            }
            Steering = _currentSteeringInput * CarCharacteristics.MaxSteeringValue;
            EngineForce = _currentAccelerationInput * CarCharacteristics.MaxEngineStrength;
        }
        
        private void OnBodyEntered(Node body)
        {
            //GD.Print($"Collided with: {body.Name}");
            if (!Mingleton.Instance.HasSingleton<AudioManager>())
            {
                return;
            }
            
            ControllableVehicle controllableVehicle = body as ControllableVehicle;
            if (controllableVehicle != null)
            {
                //Hit other car
                Mingleton.Instance.GetSingleton<AudioManager>().PlayAudio("Hit02.wav");
                return;
            }
            
            GridMap gridMap = body as GridMap;
            if (gridMap != null)
            {
                //Hit wall or something else
                Mingleton.Instance.GetSingleton<AudioManager>().PlayAudio("Hit01.mp3");
                return;
            }
           
        }
    }
}