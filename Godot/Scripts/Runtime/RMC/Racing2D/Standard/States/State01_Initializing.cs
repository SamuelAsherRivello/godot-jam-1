using RMC.Core.Patterns.StateMachines;

namespace RMC.Racing2D.Standard.States
{
    /// <summary>
    /// Example concrete state
    /// </summary>
    public partial class State01_Initializing : Racing2DBaseState
    {
        // Initialization -------------------------------
        public State01_Initializing(
            
            StateMachine stateMachine,
            Scene02_Racer2DGame scene02Racer2D)
        
            : base(stateMachine, scene02Racer2D)
        {
        }

        // Methods ---------------------------------------
        public override void Enter()
        {
        }

        public override void Execute(double delta)
        {
        }

        public override void Exit()
        {
        }
    }

}

