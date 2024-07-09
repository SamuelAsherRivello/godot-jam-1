using Godot;
using System;
using System.Collections.Generic;
using RMC.Mingletons;
using RMC.Mini.Features.SceneSystem;
using RMC.Racing2D.Players;
using RMC.Racing2D.Vehicles;
using RMC.Racing2D.Mini;
using RMC.Racing2D.Mini.Features.Game;
using RMC.Racing2D.Mini.Model;


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

        //  Godot Methods ---------------------------------

        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            GD.Print($"Scene02_Game._Ready()");
            AddFeature();
            SetupControllableVehicles();

            Racing2DMini racing2DMini = Mingleton.Instance.GetOrCreateAsClass<Racing2DMini>();
            Racing2DModel racing2DModel = racing2DMini.ModelLocator.GetItem<Racing2DModel>();
            
            GD.Print("Enemy: " + racing2DModel.GetCurrentEnemyMenuConfiguration().Title);
            GD.Print("Player: " + racing2DModel.GetCurrentPlayerMenuConfiguration().Title);
        }


        /// <summary>
        /// Called every frame. 'delta' is the elapsed time since the previous frame.
        /// </summary>
        public override void _Process(double delta)
        {
            base._Process(delta);

            var meshName = _track.GetMeshNameAtPosition(_track.FlowGridMap, _controllablePlayerVehicle.Position);
            var rotationInDegrees =
                _track.GetMeshRotationInDegreesAtPosition(_track.FlowGridMap, _controllablePlayerVehicle.Position);

            if (_lastMeshName != meshName)
            {
                GD.Print("meshName: " + meshName);
                _lastMeshName = meshName;
            }

            if (Math.Abs(_lastRotationInDegrees - rotationInDegrees) > 0.1f)
            {
                GD.Print("rotationInDegrees: " + rotationInDegrees);
                _lastRotationInDegrees = rotationInDegrees;
            }
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
    }
}
        
        
