using System;
using Godot;
using RMC.Core.Events;
using RMC.Mini;
using RMC.Mini.View;
using RMC.Racing2D.Mini.Model;

namespace RMC.Racing2D.Mini.Features.Menu
{
    /// <summary>
    /// The View handles user interface and user input
    ///
    /// Relates to the <see cref="MenuController"/>
    /// 
    /// </summary>
    public partial class MenuView: Control, IView
    {
        //  Events ----------------------------------------
        public readonly RmcEvent OnPlayGame = new RmcEvent();
        public readonly RmcEvent OnChangePlayer = new RmcEvent();
        public readonly RmcEvent OnChangeEnemy = new RmcEvent();
        
        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public IContext Context { get { return _context;} }
        
        //  Fields ----------------------------------------
        private bool _isInitialized = false;
        private IContext _context;
        
        [Export] 
        private Label _titleLabel;
        
        [Export] 
        private RichTextLabel _statusRichLabel;
        
        [Export] 
        private Button _playGameButton;
        
        [Export] 
        private Button _changePlayerButton;
        
        [Export] 
        private Button _changeEnemyButton;

        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
                
                _playGameButton.Pressed += PlayGameButton_OnPressed;
                _changePlayerButton.Pressed += ChangePlayerButton_OnPressed;
                _changeEnemyButton.Pressed += ChangeEnemyButton_OnPressed;
                
                RefreshUI();
            }
        }


        public void RequireIsInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }
        
        
        
        //  Godot Methods ---------------------------------
		
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
        }

        
        /// <summary>
        /// Called every frame. 'delta' is the elapsed time since the previous frame.
        /// </summary>
        public override void _Process(double delta)
        {
        }
		
		
        /// <summary>
        /// Called when the node is about to leave the SceneTree
        /// </summary>
        public override void _ExitTree()  
        {
            RequireIsInitialized();
            
            // Optional: Handle any cleanup here...
        }


        //  Methods ---------------------------------------
        public void RefreshUI()
        {
            RequireIsInitialized();
            
            Racing2DModel model = Context.ModelLocator.GetItem<Racing2DModel>();
            
            string playerTitle = model.PlayerMenuConfigurations.Value[model.PlayerMenuConfigurationIndex.Value].Title;
            string enemyInfo = model.EnemyMenuConfigurations.Value[model.EnemyMenuConfigurationIndex.Value].Title;
            
            _statusRichLabel.ParseBbcode( $"[center]Your Car Is [color=#4ab3ff]{playerTitle}[/color] " +
                                          $"& Your Enemy Is [color=#4ab3ff]{enemyInfo}[/color][/center].");
        }
        
        
        //  Event Handlers --------------------------------
        private void PlayGameButton_OnPressed()
        {
            OnPlayGame.Invoke();
        }
        
        private void ChangePlayerButton_OnPressed()
        {
            OnChangePlayer.Invoke();
        }
        
        private void ChangeEnemyButton_OnPressed()
        {
            OnChangeEnemy.Invoke();
        }
    }
}