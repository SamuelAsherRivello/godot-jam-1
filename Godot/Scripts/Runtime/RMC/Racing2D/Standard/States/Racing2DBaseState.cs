using RMC.Core.Patterns.StateMachines;

namespace RMC.Racing2D.Standard.States
{
    /// <summary>
    /// Example concrete state
    /// </summary>
    public partial class Racing2DBaseState : BaseState
    {
        protected Scene02_Racer2DGame Scene02_Racer2DGame { get { return _scene02Racer2D;}}
        private Scene02_Racer2DGame _scene02Racer2D;
        
        // Initialization -------------------------------
        public Racing2DBaseState(
            
            StateMachine stateMachine, 
            Scene02_Racer2DGame scene02Racer2D) 
        
            : base(stateMachine)
        {
            _scene02Racer2D = scene02Racer2D;
        }
    }
}

