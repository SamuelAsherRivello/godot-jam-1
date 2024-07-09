using RMC.Core.Patterns.StateMachines;

namespace RMC.Racing2D.Standard.States
{
    /// <summary>
    /// Example concrete state
    /// </summary>
    public partial class Racing2DBaseState : BaseState
    {
        protected Scene02_Racer2DGame GameScene { get { return _gameScene;}}
        private Scene02_Racer2DGame _gameScene;
        
        // Initialization -------------------------------
        public Racing2DBaseState(
            
            StateMachine stateMachine, 
            Scene02_Racer2DGame gameScene) 
        
            : base(stateMachine)
        {
            _gameScene = gameScene;
        }
    }
}

