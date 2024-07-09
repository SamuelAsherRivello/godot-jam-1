using System;
using Godot;
using RMC.Core.Patterns.StateMachines;
using RMC.Mingletons;
using RMC.Racing2D.Audio;
using RMC.Racing2D.Vehicles;

// ReSharper disable once PossibleLossOfFraction
namespace RMC.Racing2D.Standard.States
{
    /// <summary>
    /// Example concrete state
    /// </summary>
    public partial class State03_Racing : Racing2DBaseState
    {
        // Initialization -------------------------------
        public State03_Racing(
            
            StateMachine stateMachine,
            Scene02_Racer2DGame gameScene)
        
            : base(stateMachine, gameScene)
        {
        }

        // Methods ---------------------------------------
        public override void Enter()
        {
            GameScene.Track.TrackStartingArea.OnBodyEntered.AddListener(TrackStartingArea_OnBodyEntered);
            GameScene.Track.TrackStartingArea.OnBodyExited.AddListener(TrackStartingArea_OnBodyExited);
            
            GameScene.Track.ControllableVehicles.ForEach((controllableVehicle) =>
            {
                controllableVehicle.IsEnabled = true;
            });
        }

        public override void Execute(double delta)
        {
        }

        public override void Exit()
        {
            GameScene.Track.ControllableVehicles.ForEach((controllableVehicle) =>
            {
                controllableVehicle.IsEnabled = false;
            });
            
            GameScene.Track.TrackStartingArea.OnBodyEntered.RemoveListener(TrackStartingArea_OnBodyEntered);
            GameScene.Track.TrackStartingArea.OnBodyExited.RemoveListener(TrackStartingArea_OnBodyEntered);
        }
        
        // Methods ---------------------------------------
        private void TrackStartingArea_OnBodyEntered(Node body)
        {
            ControllableVehicle controllableVehicle = body as ControllableVehicle;
            if (controllableVehicle == null)
            {
                return;
            }

            if (!controllableVehicle.HasExitedStartingArea)
            {
                return;
            }
            controllableVehicle.LapCurrent++;

            //Whoever is winning updates the model
            GameScene.Racing2DModel.LapCurrent.Value =
                Math.Max(GameScene.Racing2DModel.LapCurrent.Value, controllableVehicle.LapCurrent);
            GameScene.GameView.StatusLabelShowLaps();

            // Make sound

            float percentComplete = GameScene.Racing2DModel.LapCurrent.Value / GameScene.Racing2DModel.LapMax.Value;
            percentComplete = Math.Max(0.5f, percentComplete);
            Mingleton.Instance.GetSingleton<AudioManager>().PlayAudio(
                "LapCompleteChime.mp3", 
                0.5f + percentComplete);
            
            //Check gameover
            if (controllableVehicle.LapCurrent >= GameScene.Racing2DModel.LapMax.Value)
            {
                _stateMachine.StateChange(GameScene.State04Ending);
            }
        }
        
        private void TrackStartingArea_OnBodyExited(Node body)
        {
            ControllableVehicle controllableVehicle = body as ControllableVehicle;
            if (controllableVehicle == null)
            {
                return;
            }

            if (!controllableVehicle.HasExitedStartingArea)
            {
                controllableVehicle.HasExitedStartingArea = true;
            }
        }
    }
}

