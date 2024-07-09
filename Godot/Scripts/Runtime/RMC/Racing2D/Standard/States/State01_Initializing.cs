using Godot;
using RMC.Core.Patterns.StateMachines;
using RMC.Core.Utilities;
using RMC.Racing2D.Players;

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
            GD.Print("Enemy: " + GameScene.Racing2DModel.GetCurrentEnemyMenuConfiguration().Title);
            GD.Print("Player: " + GameScene.Racing2DModel.GetCurrentPlayerMenuConfiguration().Title);
            
            // Create player
            var playerPath = FileAccessUtility.FindFileOnceInResources("LocalPlayerControllableVehicle.tscn");
            PackedScene playerScene = GD.Load<PackedScene>(playerPath);
            LocalPlayerControllableVehicle player = playerScene.Instantiate<LocalPlayerControllableVehicle>();
            player.Name = $"Vehicle 01 (Player)";
            GameScene.VehicleParent.AddChild(player);
            
            // Position Player
            player.GlobalPosition = GameScene.Track.TrackStartingArea.StartingPoints[0].GlobalPosition;
            player.Rotation = GameScene.Track.TrackStartingArea.StartingPoints[0].Rotation;

            // Create enemies
            for (int i = 0; i < 3; i++)
            {
                var enemyPath = FileAccessUtility.FindFileOnceInResources("AIControllableVehicle.tscn");
                PackedScene enemyScene = GD.Load<PackedScene>(enemyPath);
                AIControllableVehicle enemy = enemyScene.Instantiate<AIControllableVehicle>();
                GameScene.VehicleParent.AddChild(enemy);
                player.Name = $"Vehicle {i:00} (Enemy)";
                
                // Position Enemy
                enemy.GlobalPosition = GameScene.Track.TrackStartingArea.StartingPoints[(i+1)].GlobalPosition;
                enemy.Rotation = GameScene.Track.TrackStartingArea.StartingPoints[(i+1)].Rotation;
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

