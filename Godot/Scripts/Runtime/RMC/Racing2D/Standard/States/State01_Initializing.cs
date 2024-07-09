using Godot;
using RMC.Core.Patterns.StateMachines;
using RMC.Core.Utilities;
using RMC.Mingletons;
using RMC.Racing2D.Mini;
using RMC.Racing2D.Mini.Model;
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
            Scene02_Racer2DGame scene02Racer2D)
        
            : base(stateMachine, scene02Racer2D)
        {
        }

        // Methods ---------------------------------------
        public override void Enter()
        {
            Racing2DMini racing2DMini = Mingleton.Instance.GetOrCreateAsClass<Racing2DMini>();
            Racing2DModel racing2DModel = racing2DMini.ModelLocator.GetItem<Racing2DModel>();
            
            GD.Print("Enemy: " + racing2DModel.GetCurrentEnemyMenuConfiguration().Title);
            GD.Print("Player: " + racing2DModel.GetCurrentPlayerMenuConfiguration().Title);

            // Create player
            var playerPath = FileAccessUtility.FindFileOnceInResources("LocalPlayerControllableVehicle.tscn");
            PackedScene playerScene = GD.Load<PackedScene>(playerPath);
            LocalPlayerControllableVehicle player = playerScene.Instantiate<LocalPlayerControllableVehicle>();
            player.Name = $"Vehicle 01 (Player)";
            Scene02_Racer2DGame.VehicleParent.AddChild(player);
            
            // Position Player
            player.GlobalPosition = Scene02_Racer2DGame.Track.TrackStartingArea.StartingPoints[0].GlobalPosition;
            player.Rotation = Scene02_Racer2DGame.Track.TrackStartingArea.StartingPoints[0].Rotation;

            // Create enemies
            for (int i = 0; i < 3; i++)
            {
                var enemyPath = FileAccessUtility.FindFileOnceInResources("AIControllableVehicle.tscn");
                PackedScene enemyScene = GD.Load<PackedScene>(enemyPath);
                AIControllableVehicle enemy = enemyScene.Instantiate<AIControllableVehicle>();
                Scene02_Racer2DGame.VehicleParent.AddChild(enemy);
                player.Name = $"Vehicle {i:00} (Enemy)";
                
                // Position Enemy
                enemy.GlobalPosition = Scene02_Racer2DGame.Track.TrackStartingArea.StartingPoints[(i+1)].GlobalPosition;
                enemy.Rotation = Scene02_Racer2DGame.Track.TrackStartingArea.StartingPoints[(i+1)].Rotation;
            }
            
            // Done!
            _stateMachine.StateChange(Scene02_Racer2DGame.State02Starting);
            
        }

        public override void Execute(double delta)
        {
        }

        public override void Exit()
        {
        }
    }

}

