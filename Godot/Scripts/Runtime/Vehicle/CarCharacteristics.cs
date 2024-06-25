using Godot;
using System;

public partial class CarCharacteristics : Resource
{
    [Export(PropertyHint.Range, "0.1f, 100000.0f")]
    private float _MaxEngineStrength = 1.0f;

    [Export(PropertyHint.Range, "0.1f, 2.0f")]
    private float _MaxSteeringValue = 1.0f;

    [Export(PropertyHint.Range, "0.1f, 10000.0f")]
    private float _TotalMass = 1.0f;

    public float MaxEngineStrength { get => _MaxEngineStrength; }
    public float MaxSteeringValue { get => _MaxSteeringValue; }
    public float TotalMass { get => _TotalMass; }
}
