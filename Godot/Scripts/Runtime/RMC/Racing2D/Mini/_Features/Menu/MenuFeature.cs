using RMC.Mini;
using RMC.Mini.Features;
using RMC.Mini.Service;
using RMC.Racing2D.Mini.Model;

namespace RMC.Racing2D.Mini.Features.Menu
{
    /// <summary>
    /// Set up a collection of related <see cref="IConcern"/> instances
    /// </summary>
    public class MenuFeature: BaseFeature // Extending 'base' is optional
    {
        //  Initialization --------------------------------

        //  Methods ---------------------------------------
        public override void Build()
        {
            RequireIsInitialized();
            
            // Get from mini
            Racing2DModel racing2DModel = MiniMvcs.ModelLocator.GetItem<Racing2DModel>();
            
            // Create new controller
            MenuController controller = 
                new MenuController(racing2DModel, View as MenuView, new DummyService());
            
            // Add to mini
            MiniMvcs.ControllerLocator.AddItem(controller);
            MiniMvcs.ViewLocator.AddItem(View);
            
            // Initialize
            View.Initialize(MiniMvcs.Context);
            controller.Initialize(MiniMvcs.Context);
            
            // Set Mode
            racing2DModel.HasNavigationBack.Value = false;
        }

        
        public override void Dispose()
        {
            RequireIsInitialized();
            
            if (MiniMvcs.ControllerLocator.HasItem<MenuController>())
            {
                //TODO: Maybe make RemoveItem(willDispose==true) for all locators?
                MiniMvcs.ControllerLocator.GetItem<MenuController>().Dispose();
                MiniMvcs.ControllerLocator.RemoveItem<MenuController>();
                MiniMvcs.ViewLocator.RemoveItem<MenuView>();
            }
        }
    }
}