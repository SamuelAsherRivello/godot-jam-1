using System.Threading.Tasks;
using Godot;
using RMC.Core.Patterns.StateMachines;
using RMC.Mingletons;
using RMC.Racing2D.Audio;

namespace RMC.Racing2D.Standard.States
{
    /// <summary>
    /// Example concrete state
    /// </summary>
    public partial class State02_Starting : Racing2DBaseState
    {
        // Initialization -------------------------------
        public State02_Starting(
            StateMachine stateMachine,
            Scene02_Racer2DGame gameScene)
        
            : base(stateMachine, gameScene)
        {
        }

        // Methods ---------------------------------------
        public override async void Enter()
        {
            GameScene.GameView.StatusLabelShowMessage("3...");
            Mingleton.Instance.GetSingleton<AudioManager>().PlayAudio("Pickup01.mp3");
            await Task.Delay(500);
            
            //
            GameScene.GameView.StatusLabelShowMessage("2...");
            Mingleton.Instance.GetSingleton<AudioManager>().PlayAudio("Pickup01.mp3");
            await Task.Delay(500);

            //
            GameScene.GameView.StatusLabelShowMessage("1...");
            Mingleton.Instance.GetSingleton<AudioManager>().PlayAudio("Pickup01.mp3");
            await Task.Delay(500);
            
            //
            GameScene.GameView.StatusLabelShowMessage("Go!");
            Mingleton.Instance.GetSingleton<AudioManager>().PlayAudio("Pickup01.mp3");
            
            // Done!
            _stateMachine.StateChange(GameScene.State03Racing);
            
            // After race starts, show laps
            await Task.Delay(500);
            GameScene.GameView.StatusLabelShowLaps();
        }

        public override void Execute(double delta)
        {
        }

        public override void Exit()
        {
        }
    }

}

