using Godot;
using System;

public partial class CarCharacteristics : Resource
{
    [Export(PropertyHint.Range, "0.1f, 100000.0f")]
    private float _MaxEngineStrength = 1.0f;

    [Export(PropertyHint.Range, "0.1f, 2.0f")]
    private float _MaxSteeringValue = 1.0f;

    public float MaxEngineStrength { get => _MaxEngineStrength; set => _MaxEngineStrength = value; }
    public float MaxSteeringValue { get => _MaxSteeringValue; set => _MaxSteeringValue = value; }
}
