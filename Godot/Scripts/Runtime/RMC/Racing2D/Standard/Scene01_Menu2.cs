using Godot;
using RMC.Mingletons;
using RMC.Mini.Features.SceneSystem;
using RMC.Racing2D.Mini;
using RMC.Racing2D.Mini.Features.Menu;

namespace RMC.Racing2D.Standard
{
    /// <summary>
    /// This is the main entry point to one of the scenes
    /// </summary>
    public partial class Scene01_Menu2 : Node
    {
        //  Fields ----------------------------------------
        [Export]
        private MenuView _menuView;
        
        //  Godot Methods ---------------------------------
		
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            GD.Print($"Scene01_Menu._Ready()");
            AddFeature();
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
            RemoveFeature();
            // Optional: Handle any cleanup here...
        }

        //  Methods ---------------------------------------
        private void AddFeature()
        {

            Racing2DMini mini = Mingleton.Instance.GetOrCreateAsClass<Racing2DMini>();
            
            //  Scene-Specific ----------------------------
            MenuFeature menuFeature = new MenuFeature();
            menuFeature.AddView(_menuView);
            mini.AddFeature<MenuFeature>(menuFeature);
            
            //  Scene-Agnostic (Permanent) -----------------
            if (!mini.HasFeature<SceneSystemFeature>())
            {
                SceneSystemFeature sceneSystemFeature = new SceneSystemFeature();
                mini.AddFeature<SceneSystemFeature>(sceneSystemFeature);
            }
        }
        
        private void RemoveFeature()
        {
            Racing2DMini mini = Mingleton.Instance.GetOrCreateAsClass<Racing2DMini>();
            
            //  Scene-Specific ----------------------------
            mini.RemoveFeature<MenuFeature>();
            
        }
        
        //  Event Handlers --------------------------------
    }
}