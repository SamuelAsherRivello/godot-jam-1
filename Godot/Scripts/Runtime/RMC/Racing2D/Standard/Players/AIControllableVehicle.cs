using Godot;
using RMC.Racing2D.Vehicles;
using System;
using System.Diagnostics;

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
            Race();

            base._PhysicsProcess(delta);
        }

        private void Race()
        {
            float newSteeringInput = 0.0f;
            float newAcceleration = 0.0f;

            const float maxSpeed = 15.0f;
            if (LinearVelocity.Length() < maxSpeed)
                newAcceleration = 1.0f;

            var flowMap = GetTrack().FlowGridMap;
            if (GetTrack().HasGridDirectionalInformation(flowMap, Position))
            {
                Vector3 currentDirection = Vector3.Forward * GlobalBasis.GetRotationQuaternion();
                float meshRotationAtCurrentPositionDEG = GetTrack().GetMeshRotationInDegreesAtPosition(GetTrack().FlowGridMap, Position);
                Quaternion trackRotation = Quaternion.FromEuler(new Vector3(0.0f, Mathf.DegToRad(meshRotationAtCurrentPositionDEG), 0.0f));
                Vector3 northWorldVector = new Vector3(0.0f, 0.0f, 1.0f);
                Vector3 targetDirection = trackRotation * northWorldVector;
                float differenceAngle = Mathf.RadToDeg(currentDirection.SignedAngleTo(targetDirection, Vector3.Up));

                float steeringStrength = 0.0f;
                float differenceAbs = Mathf.Abs(differenceAngle);

                const float maxSteeringThresholdDEG = 20.0f;
                if (differenceAbs > maxSteeringThresholdDEG)
                    steeringStrength = 1.0f;
                else
                    steeringStrength = Mathf.Lerp(0.0f, 1.0f, differenceAbs / maxSteeringThresholdDEG);

                newSteeringInput = -1.0f * steeringStrength * Mathf.Sign(differenceAngle);
            }
            
            SetInputs(newSteeringInput, newAcceleration);
        }

        public override ControllableVehicleType GetControllableVehicleType()
        {
            return ControllableVehicleType.AI_PLAYER;
        }
    }
}

