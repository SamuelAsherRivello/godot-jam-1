using RMC.Mini;
using RMC.Mini.Controller;
using RMC.Mini.Features.SceneSystem;
using RMC.Mini.Service;
using RMC.Racing2D.Mini.Model;
using RMC.Racing2D.Standard;

namespace RMC.Racing2D.Mini.Features.Menu
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class MenuController: BaseController // Extending 'base' is optional
        <Racing2DModel, MenuView, DummyService> 
    {
        public MenuController(
            Racing2DModel model, MenuView view, DummyService service) 
            : base(model, view, service)
        {
        }

        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                // Model
                _model.HasLoadedService.OnValueChanged.AddListener(HasLoadedService_OnValueChanged);
                    
                // View
                _view.OnPlayGame.AddListener(View_OnPlayGame);
        
            }
        }




        //  Methods ---------------------------------------
        public override void Dispose()
        {
            base.Dispose();
            
            // Model
            _model.HasLoadedService.OnValueChanged.RemoveListener(HasLoadedService_OnValueChanged);
                    
            // View
            _view.OnPlayGame.RemoveListener(View_OnPlayGame);

        }


        //  Event Handlers --------------------------------
        private void HasLoadedService_OnValueChanged(bool oldvalue, bool newvalue)
        {
            _view.RefreshUI();
        }
        
        private void View_OnPlayGame()
        {
            RequireIsInitialized();
            
            // Demonstrates proper Controller-to-Controller communication with a Command
            Context.CommandManager.InvokeCommand(
                new LoadSceneRequestCommand(Racer2DConstants.Scene02_Game, LoadSceneMode.Single));
        }
    }
}