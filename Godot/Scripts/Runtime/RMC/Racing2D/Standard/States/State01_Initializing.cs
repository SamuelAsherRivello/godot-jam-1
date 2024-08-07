﻿using Godot;
using RMC.Core.Patterns.StateMachines;
using RMC.Core.Utilities;
using RMC.Racing2D.Players;
using RMC.Racing2D.Vehicles;

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
            Scene02_Racer2DGame gameScene)
        
            : base(stateMachine, gameScene)
        {
        }

        // Methods ---------------------------------------
        public override void Enter()
        {
            // Clear old vehicles            
            GameScene.Track.ControllableVehicles.Clear();
            
            // Create player
            var playerPath = FileAccessUtility.FindFileOnceInResources("LocalPlayerControllableVehicle.tscn");
            PackedScene playerScene = GD.Load<PackedScene>(playerPath);
            LocalPlayerControllableVehicle player = playerScene.Instantiate<LocalPlayerControllableVehicle>();
            player.SetBodyColor((GameScene.Racing2DModel.GetCurrentPlayerMenuConfiguration().Resource as CarAppearance).BodyColor);
            player.Name = $"Vehicle 01 (Player)";
            GameScene.VehicleParent.AddChild(player);
            GameScene.Track.ControllableVehicles.Add(player);
            
            // Position Player
            player.GlobalPosition = GameScene.Track.TrackStartingArea.StartingPoints[0].GlobalPosition;
            player.Rotation = GameScene.Track.TrackStartingArea.StartingPoints[0].Rotation;

            // Create enemies
            var enemyPath = FileAccessUtility.FindFileOnceInResources("AIControllableVehicle.tscn");
            PackedScene enemyScene = GD.Load<PackedScene>(enemyPath);

            for (int i = 1; i <= 3; i++)
            {
                AIControllableVehicle enemy = enemyScene.Instantiate<AIControllableVehicle>();
                enemy.SetupControllableVehicle(GameScene.Track);
                enemy.SetupAIControllableVehicle(GameScene.Racing2DModel.GetCurrentEnemyMenuConfiguration().Resource as AICharacteristics);
                GameScene.VehicleParent.AddChild(enemy);
                GameScene.Track.ControllableVehicles.Add(enemy);
                
                // Name Enemy (01 through 03)
                enemy.Name = $"Vehicle {(i+1):00} (Enemy)";
                
                // Position Enemy at Spot (02 through 04)
                enemy.GlobalPosition = GameScene.Track.TrackStartingArea.StartingPoints[i].GlobalPosition;
                enemy.Rotation = GameScene.Track.TrackStartingArea.StartingPoints[i].Rotation;
            }
            
            // Done!
            _stateMachine.StateChange(GameScene.State02Starting);
            
        }

        public override void Execute(double delta)
        {
        }

        public override void Exit()
        {
        }
    }

}

