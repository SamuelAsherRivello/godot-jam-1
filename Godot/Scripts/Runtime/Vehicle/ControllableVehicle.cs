using Godot;
using System;

public partial class ControllableVehicle : VehicleBody3D
{
    [Export]
    private CarCharacteristics CarCharacteristics;

    public void SetInputs(float newSteeringInput, float newAccelerationInput)
    {
        _currentSteeringInput = newSteeringInput;
        _currentAccelerationInput = newAccelerationInput;
    }

    private float _currentSteeringInput = 0.0f;
    private float _currentAccelerationInput = 0.0f;

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
        ApplyExternalInput();
    }

    private void ApplyExternalInput()   
    {
        Steering = _currentSteeringInput * CarCharacteristics.MaxSteeringValue;
        EngineForce = _currentAccelerationInput * CarCharacteristics.MaxEngineStrength;
    }
}
