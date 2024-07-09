using Godot;
using RMC.Core.Patterns.StateMachines;
using RMC.Mingletons;
using RMC.Mini.Features.SceneSystem;
using RMC.Racing2D.Mini;
using RMC.Racing2D.Mini.Features.Game;
using RMC.Racing2D.Mini.Model;
using RMC.Racing2D.Standard.States;
using RMC.Racing2D.Tracks;


namespace RMC.Racing2D.Standard
{
    public partial class Scene02_Racer2DGame : Node3D
    {
        //  Properties -------------------------------------
        public Racing2DModel Racing2DModel
        {
            get 
            {
                Racing2DMini racing2DMini = Mingleton.Instance.GetOrCreateAsClass<Racing2DMini>();
                return racing2DMini.ModelLocator.GetItem<Racing2DModel>();
            }
        }
        
        //  Fields ----------------------------------------
        [Export] 
        private GameView _gameView;

        //  Fields For States -----------------------------
        [Export] 
        public Track Track;

        [Export] 
        public Node3D VehicleParent;
        
        private StateMachine _stateMachine;
        public State01_Initializing State01Initializing;
        public State02_Starting State02Starting;
        public State03_Racing State03Racing;
        public State04_Ending State04Ending;

        
        //  Godot Methods ---------------------------------

        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            GD.Print($"Scene02_Game._Ready()");
                        

            //Mini Mvcs Setup ---------------------------------
            AddFeature();

            
            //Standard Setup  ---------------------------------
            _stateMachine = new StateMachine();
            //
            _stateMachine.OnStateEnter.AddListener(StateMachine_OnStateEnter);
            _stateMachine.OnStateExecute.AddListener(StateMachine_OnStateExecute);
            _stateMachine.OnStateExit.AddListener(StateMachine_OnStateExit);
            //
            State01Initializing = new State01_Initializing(_stateMachine, this);
            State02Starting = new State02_Starting(_stateMachine, this);
            State03Racing = new State03_Racing(_stateMachine, this);
            State04Ending = new State04_Ending(_stateMachine, this);
            //
            _stateMachine.StateChange(State01Initializing);
            
        }


        /// <summary>
        /// Called every frame. 'delta' is the elapsed time since the previous frame.
        /// </summary>
        public override void _Process(double delta)
        {
            base._Process(delta);
            _stateMachine?.StateExecute(delta);
        }


        /// <summary>
        /// Called when the node is about to leave the SceneTree
        /// </summary>
        public override void _ExitTree()
        {
            RemoveFeature();

            // Optional: Handle any cleanup here...
        }

        
        //  Methods ---------------------------------------

        private void AddFeature()
        {
            Racing2DMini mini = Mingleton.Instance.GetOrCreateAsClass<Racing2DMini>();

            //  Scene-Specific ----------------------------
            GameFeature feature = new GameFeature();
            feature.AddView(_gameView);
            mini.AddFeature<GameFeature>(feature);

            //  Scene-Agnostic (Permanent) -----------------
            if (!mini.HasFeature<SceneSystemFeature>())
            {
                SceneSystemFeature sceneSystemFeature = new SceneSystemFeature();
                mini.AddFeature<SceneSystemFeature>(sceneSystemFeature);
            }
        }

        private void RemoveFeature()
        {
            Racing2DMini mini = Mingleton.Instance.GetOrCreateAsClass<Racing2DMini>();

            //  Scene-Specific ----------------------------
            mini.RemoveFeature<GameFeature>();

        }
        
        private void StateMachine_OnStateEnter(IState previousState, IState currentState)
        {
            //GD.Print($"StateMachine_OnStateEnter from {previousState} to {currentState}");
        }
        
        private void StateMachine_OnStateExecute(IState previousState, IState currentState)
        {
            // IState nextState = null;
            // switch (currentState)
            // {
            //     case State01_Initializing:
            //         nextState = State02Starting;
            //         break;
            //     case State02_Starting:
            //         nextState = State03Racing;
            //         break;
            //     case State03_Racing:
            //         nextState = State04Ending;
            //         break;
            //     case State04_Ending:
            //         nextState = null;
            //         break;
            // }
            //
            // if (nextState != null)
            // {
            //     GD.Print($"Changing from {currentState} to {nextState}");
            //     _stateMachine.StateChange(nextState);
            // }
            
  
        }
        
        private void StateMachine_OnStateExit(IState previousState, IState currentState)
        {
            //GD.Print($"StateMachine_OnStateExit from {previousState} to {currentState}");
        }
    }
}


