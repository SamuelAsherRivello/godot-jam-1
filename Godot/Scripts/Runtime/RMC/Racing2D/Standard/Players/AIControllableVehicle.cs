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

        private AICharacteristics _aiCharacteristics;

        public void SetupAIControllableVehicle(AICharacteristics newAiCharacteristics)
        {
            _aiCharacteristics = newAiCharacteristics;

            GD.Print($"Setting up AI car MaxSpeed={_aiCharacteristics.MaxSpeed} MinCurveSpeed={_aiCharacteristics.MinCurveSpeed}");
        }

        private void Race()
        {
            const float predictionSeconds = 0.4f;
            const float maxSteeringThresholdDEG = 45.0f;

            float currentTargetSpeed = _aiCharacteristics.MaxSpeed;

            float newSteeringInput = 0.0f;
            float newAcceleration = 0.0f;

            Vector3 currentDirection = GlobalBasis.GetRotationQuaternion() * Vector3.Back;

            if (HasDirectionalInformationAtWorldPosition(Position))
            {
                Vector3 targetDirection = GetTargetDirectionAtWorldPosition(Position);

                float differenceAngle, differenceAbs;
                ComputeDirectionAngleDifference(currentDirection, targetDirection, out differenceAngle, out differenceAbs);

                float steeringStrength = 0.0f;
                if (differenceAbs > maxSteeringThresholdDEG)
                {
                    steeringStrength = 1.0f;
                    currentTargetSpeed = _aiCharacteristics.MinCurveSpeed;
                }
                else
                {
                    steeringStrength = Mathf.Lerp(0.0f, 1.0f, differenceAbs / maxSteeringThresholdDEG);
                }

                newSteeringInput = steeringStrength * Mathf.Sign(differenceAngle);
            }

            Vector3 predictedPosition = GlobalPosition + (LinearVelocity * predictionSeconds);

            if (HasDirectionalInformationAtWorldPosition(predictedPosition))
            {
                Vector3 targetDirection = GetTargetDirectionAtWorldPosition(predictedPosition);

                float differenceAngle, differenceAbs;
                ComputeDirectionAngleDifference(currentDirection, targetDirection, out differenceAngle, out differenceAbs);

                if (differenceAbs > maxSteeringThresholdDEG)
                {
                    currentTargetSpeed = _aiCharacteristics.MinCurveSpeed;
                }
            }

            const float softAccelerationThreshold = 3.0f;

            float currentSpeed = LinearVelocity.Length();
            float speedDifference = currentTargetSpeed - currentSpeed;
            float relativeSpeedDifference = speedDifference / softAccelerationThreshold;
            float normalizedRelativeSpeedDifference = (relativeSpeedDifference + 1.0f) * 0.5f;

            if (Mathf.Abs(speedDifference) < softAccelerationThreshold)
            {
                newAcceleration = Mathf.Lerp(-1.0f, 1.0f, normalizedRelativeSpeedDifference);
            }
            else
            {
                newAcceleration = Mathf.Sign(speedDifference);
            }

            SetInputs(newSteeringInput, newAcceleration);
        }

        private static void ComputeDirectionAngleDifference(Vector3 currentDirection, Vector3 targetDirection, out float differenceAngle, out float differenceAbs)
        {
            differenceAngle = Mathf.RadToDeg(currentDirection.SignedAngleTo(targetDirection, Vector3.Up));
            differenceAbs = Mathf.Abs(differenceAngle);
        }

        private bool HasDirectionalInformationAtWorldPosition(Vector3 worldPosition)
        {
            return GetTrack().HasGridDirectionalInformation(GetTrack().FlowGridMap, worldPosition);
        }

        private Vector3 GetTargetDirectionAtWorldPosition(Vector3 worldPosition)
        {
            float meshRotationAtCurrentPositionDEG = GetTrack().GetMeshRotationInDegreesAtPosition(GetTrack().FlowGridMap, worldPosition);
            Quaternion trackRotation = Quaternion.FromEuler(new Vector3(0.0f, Mathf.DegToRad(360.0f - meshRotationAtCurrentPositionDEG), 0.0f));
            Vector3 northWorldVector = Vector3.Forward;
            Vector3 targetDirection = trackRotation * northWorldVector;
            return targetDirection;
        }

        public override ControllableVehicleType GetControllableVehicleType()
        {
            return ControllableVehicleType.AI_PLAYER;
        }
    }
}

