using RMC.Core.Events;
using RMC.Mini;
using RMC.Mini.Controller;
using RMC.Mini.Features.SceneSystem;
using RMC.Mini.Service;
using RMC.Racing2D.Mini.Model;
using RMC.Racing2D.Standard;

namespace RMC.Racing2D.Mini.Features.Game
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class GameController: BaseController // Extending 'base' is optional
        <Racing2DModel, GameView, DummyService> 
    {
        public GameController(
            Racing2DModel model, GameView view, DummyService service) 
            : base(model, view, service)
        {
        }
        
        //  Events ---------------------------------------
        public readonly RmcEvent OnFun = new RmcEvent();
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                // View
                _view.OnBack.AddListener(View_OnBack);
                
            }
        }

        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
        
        private void View_OnBack()
        {
            _view.BackButton.Disabled = true;
            // Demonstrates proper Controller-to-Controller communication with a Command
            Context.CommandManager.InvokeCommand(
                new LoadSceneRequestCommand(Racer2DConstants.Scene01_Menu, LoadSceneMode.Single));
        }
    }
}