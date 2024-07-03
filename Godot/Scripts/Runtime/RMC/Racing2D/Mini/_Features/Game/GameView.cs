using System;
using Godot;
using RMC.Core.Events;
using RMC.Mini;
using RMC.Mini.View;
using RMC.Racing2D.Mini.Model;

namespace RMC.Racing2D.Mini.Features.Game
{
    /// <summary>
    /// The View handles user interface and user input
    ///
    /// Relates to the <see cref="GameController"/>
    /// 
    /// </summary>
    public partial class GameView: Control, IView
    {
        //  Events ----------------------------------------
        public readonly RmcEvent OnBack = new RmcEvent();
        
        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public IContext Context { get { return _context;} }
        public Button BackButton { get { return _backButton; }}
        
        //  Fields ----------------------------------------
        private bool _isInitialized = false;
        private IContext _context;

        [Export] 
        private Label _titleLabel;
        
        [Export] 
        private Label _statusLabel;
        
        [Export] 
        private Button _backButton;

        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
                
                BackButton.Pressed += BackButtonOnPressed;
                
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
            _titleLabel.Text = "Game";
            _statusLabel.Text = $"Lap {model.LapCurrent.Value} / {model.LapMax.Value}";
        }
        
        public void ShowMessage(string message)
        {
            //In this simple demo we just log out text
            GD.Print(message);
        }
        
        //  Event Handlers --------------------------------
        private void BackButtonOnPressed()
        {
            OnBack.Invoke();
        }
    }
}