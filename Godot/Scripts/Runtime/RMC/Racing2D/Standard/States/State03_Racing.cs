﻿using RMC.Core.Patterns.StateMachines;

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
        }

        public override void Execute(double delta)
        {
        }

        public override void Exit()
        {
        }
    }

}
