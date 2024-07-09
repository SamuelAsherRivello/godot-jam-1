using System.Collections.Generic;
using Godot;
using RMC.Core.Observables;
using RMC.Core.Utilities;
using RMC.Mini;
using RMC.Mini.Model;
using RMC.Racing2D.Vehicles;

namespace RMC.Racing2D.Mini.Model
{
    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public class Racing2DModel: BaseModel // Extending 'base' is optional
    {
        //  Properties ------------------------------------
        
        public Observable<List<PlayerMenuConfiguration>> PlayerMenuConfigurations { get { return _playerMenuConfigurations;} }
        public Observable<List<EnemyMenuConfiguration>> EnemyMenuConfigurations { get { return _enemyMenuConfigurations;} }
        public Observable<int> PlayerMenuConfigurationIndex { get { return _playerMenuConfigurationIndex;} }
        public Observable<int> EnemyMenuConfigurationIndex { get { return _enemyMenuConfigurationIndex;} }
        public Observable<bool> HasNavigationBack { get { return _hasNavigationBack;} }
        public Observable<int> LapCurrent { get { return _lapCurrent;} }
        public Observable<int> LapMax{ get { return _lapMax;} }
        
        //  Fields ----------------------------------------
        private readonly Observable<List<PlayerMenuConfiguration>> _playerMenuConfigurations =
            new Observable<List<PlayerMenuConfiguration>>();
        private readonly Observable<List<EnemyMenuConfiguration>> _enemyMenuConfigurations = 
            new Observable<List<EnemyMenuConfiguration>>();
        
        private readonly Observable<bool> _hasNavigationBack = new Observable<bool>();
        private readonly Observable<int> _playerMenuConfigurationIndex = new Observable<int>();
        private readonly Observable<int> _enemyMenuConfigurationIndex = new Observable<int>();
        private readonly Observable<int> _lapCurrent = new Observable<int>();
        private readonly Observable<int> _lapMax = new Observable<int>();
        
        //  Convenience ----------------------------------------
        public PlayerMenuConfiguration GetCurrentPlayerMenuConfiguration ()
        {
            return _playerMenuConfigurations.Value[_playerMenuConfigurationIndex.Value]; 
        }
        
        public EnemyMenuConfiguration GetCurrentEnemyMenuConfiguration ()
        {
            return _enemyMenuConfigurations.Value[_enemyMenuConfigurationIndex.Value]; 
        }
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _enemyMenuConfigurations.Value = new List<EnemyMenuConfiguration>();
                _playerMenuConfigurations.Value = new List<PlayerMenuConfiguration>();
                _playerMenuConfigurationIndex.Value = 0;
                _enemyMenuConfigurationIndex.Value = 0;
                _hasNavigationBack.Value = false;
                _lapCurrent.Value = 1;
                _lapMax.Value = 1;
              
                //////////////////////////////////////////////
                // Player Cars 
                //TODO: Replace nulls and remove this comment
                _playerMenuConfigurations.Value.Add(new PlayerMenuConfiguration("Red",
                    GD.Load<CarAppearance>(FileAccessUtility.FindFileOnceInResources("RedCarAppearance.tres"))));
                _playerMenuConfigurations.Value.Add(new PlayerMenuConfiguration("Blue",
                    GD.Load<CarAppearance>(FileAccessUtility.FindFileOnceInResources("BlueCarAppearance.tres"))));
                _playerMenuConfigurations.Value.Add(new PlayerMenuConfiguration("Green",
                    GD.Load<CarAppearance>(FileAccessUtility.FindFileOnceInResources("GreenCarAppearance.tres"))));

                //////////////////////////////////////////////
                // Enemy Cars 
                _enemyMenuConfigurations.Value.Add(new EnemyMenuConfiguration("Easy",
                    GD.Load<AICharacteristics>(FileAccessUtility.FindFileOnceInResources("EasyAI.tres"))));
                _enemyMenuConfigurations.Value.Add(new EnemyMenuConfiguration("Medium",
                    GD.Load<AICharacteristics>(FileAccessUtility.FindFileOnceInResources("MediumAI.tres"))));
                _enemyMenuConfigurations.Value.Add(new EnemyMenuConfiguration("Hard",
                    GD.Load<AICharacteristics>(FileAccessUtility.FindFileOnceInResources("HardAI.tres"))));
            }
        }

        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
 
    }
}