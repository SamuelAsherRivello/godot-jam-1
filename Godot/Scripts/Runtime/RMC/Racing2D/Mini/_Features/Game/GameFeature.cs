using RMC.Mini;
using RMC.Mini.Features;
using RMC.Mini.Service;
using RMC.Racing2D.Mini.Model;

namespace RMC.Racing2D.Mini.Features.Game
{
    /// <summary>
    /// Set up a collection of related <see cref="IConcern"/> instances
    /// </summary>
    public class GameFeature: BaseFeature // Extending 'base' is optional
    {
        //  Initialization --------------------------------

        //  Methods ---------------------------------------
        
        public override void Build()
        {
            RequireIsInitialized();
            
            // Get from mini
            Racing2DModel racing2DModel = MiniMvcs.ModelLocator.GetItem<Racing2DModel>();
            
            // Create new controller
            GameController controller = 
                new GameController(racing2DModel, View as GameView, new DummyService());
            
            // Add to mini
            MiniMvcs.ControllerLocator.AddItem(controller);
            MiniMvcs.ViewLocator.AddItem(View);
            
            // Initialize
            View.Initialize(MiniMvcs.Context);
            controller.Initialize(MiniMvcs.Context);
     
            // Set Mode
            racing2DModel.HasNavigationBack.Value = true;
        }

        public override void Dispose()
        {
            RequireIsInitialized();
            
            if (MiniMvcs.ControllerLocator.HasItem<GameController>())
            {
                MiniMvcs.ControllerLocator.GetItem<GameController>().Dispose();
                MiniMvcs.ControllerLocator.RemoveItem<GameController>();
                MiniMvcs.ViewLocator.RemoveItem<GameView>();
            }
        }

        //  Event Handlers --------------------------------

    }
}