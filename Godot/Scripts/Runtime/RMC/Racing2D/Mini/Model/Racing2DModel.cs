using System.Collections.Generic;
using Godot;
using RMC.Core.Observables;
using RMC.Mini;
using RMC.Mini.Model;

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
                _lapMax.Value = 3;
              
                //////////////////////////////////////////////
                // Player Cars 
                //TODO: Replace nulls and remove this comment
                _playerMenuConfigurations.Value.Add(new PlayerMenuConfiguration("Red", null));
                _playerMenuConfigurations.Value.Add(new PlayerMenuConfiguration("Blue", null));
                _playerMenuConfigurations.Value.Add(new PlayerMenuConfiguration("Green", null));
                
                //////////////////////////////////////////////
                // Player Cars 
                //TODO: Replace nulls and remove this comment
                _enemyMenuConfigurations.Value.Add(new EnemyMenuConfiguration("Easy", null));
                _enemyMenuConfigurations.Value.Add(new EnemyMenuConfiguration("Medium", null));
                _enemyMenuConfigurations.Value.Add(new EnemyMenuConfiguration("Hard", null));
            }
        }

        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
 
    }
}