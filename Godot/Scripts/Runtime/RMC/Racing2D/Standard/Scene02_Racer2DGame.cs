using Godot;
using System;
using System.Collections.Generic;
using RMC.Core.Patterns.StateMachines;
using RMC.Mingletons;
using RMC.Mini.Features.SceneSystem;
using RMC.Racing2D.Players;
using RMC.Racing2D.Vehicles;
using RMC.Racing2D.Mini;
using RMC.Racing2D.Mini.Features.Game;
using RMC.Racing2D.Mini.Model;
using RMC.Racing2D.Standard.States;


namespace RMC.Racing2D.Standard
{
    public partial class Scene02_Racer2DGame : Node3D
    {
        //  Fields ----------------------------------------
        [Export] 
        private GameView _gameView;

        [Export] 
        private Track _track;

        [Export] 
        private ControllableVehicle _controllablePlayerVehicle;

        [Export] 
        private Node3D _vehiclesNode;

        private string _lastMeshName = "";
        private float _lastRotationInDegrees = -1;
        private List<ControllableVehicle> _controllableVehicles = new List<ControllableVehicle>();
        private StateMachine _stateMachine;
        private State01_Initializing _state01Initializing;
        private State02_Starting _state02Starting;
        private State03_Racing _state03Racing;
        private State04_Ending _state04Ending;
        
        //  Godot Methods ---------------------------------

        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            GD.Print($"Scene02_Game._Ready()");
            
            //---------------------------------
            AddFeature();
            SetupControllableVehicles();

            //---------------------------------
            Racing2DMini racing2DMini = Mingleton.Instance.GetOrCreateAsClass<Racing2DMini>();
            Racing2DModel racing2DModel = racing2DMini.ModelLocator.GetItem<Racing2DModel>();
            
            GD.Print("Enemy: " + racing2DModel.GetCurrentEnemyMenuConfiguration().Title);
            GD.Print("Player: " + racing2DModel.GetCurrentPlayerMenuConfiguration().Title);


            //---------------------------------
            _stateMachine = new StateMachine();
            
            _stateMachine.OnStateEnter.AddListener(StateMachine_OnStateEnter);
            _stateMachine.OnStateExecute.AddListener(StateMachine_OnStateExecute);
            _stateMachine.OnStateExit.AddListener(StateMachine_OnStateExit);
            
            _state01Initializing = new State01_Initializing(_stateMachine, this);
            _state02Starting = new State02_Starting(_stateMachine, this);
            _state03Racing = new State03_Racing(_stateMachine, this);
            _state04Ending = new State04_Ending(_stateMachine, this);
            
            //
            _stateMachine.StateChange(_state01Initializing);
            
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
        public void SetupControllableVehicles()
        {
            foreach (var node in _vehiclesNode.GetChildren())
            {
                if (node is ControllableVehicle)
                {
                    _controllableVehicles.Add((ControllableVehicle)node);
                }
            }

            foreach (var vehicle in _controllableVehicles)
            {
                vehicle.SetupForRace(_track);
            }
        }

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
            IState nextState = null;
            switch (currentState)
            {
                case State01_Initializing:
                    nextState = _state02Starting;
                    break;
                case State02_Starting:
                    nextState = _state03Racing;
                    break;
                case State03_Racing:
                    nextState = _state04Ending;
                    break;
                case State04_Ending:
                    nextState = null;
                    break;
            }

            if (nextState != null)
            {
                GD.Print($"Changing from {currentState} to {nextState}");
                _stateMachine.StateChange(nextState);
            }
            
  
        }
        
        private void StateMachine_OnStateExit(IState previousState, IState currentState)
        {
            //GD.Print($"StateMachine_OnStateExit from {previousState} to {currentState}");
        }
    }
}


