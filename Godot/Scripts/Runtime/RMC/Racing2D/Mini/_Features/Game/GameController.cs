using RMC.Core.Events;
using RMC.Mini;
using RMC.Mini.Controller;
using RMC.Mini.Service;
using RMC.Racing2D.Mini.Model;

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
                
                // Model
                _model.HasLoadedService.OnValueChanged.AddListener(HasLoadedService_OnValueChanged);
                    
                // View
                _view.OnFun.AddListener(View_OnFun);
                
            }
        }

        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
        private void HasLoadedService_OnValueChanged(bool oldvalue, bool newvalue)
        {
            _view.RefreshUI();
        }
        
        private void View_OnFun()
        {
            Racing2DModel model = Context.ModelLocator.GetItem<Racing2DModel>();
            _view.ShowMessage(model.FunMessage.Value);
        }
    }
}