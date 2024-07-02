using Godot;
using RMC.Mingletons;
using RMC.Mini.Features.SceneSystem;
using RMC.Racing2D.Mini;
using RMC.Racing2D.Mini.Features.Menu;


namespace RMC.Racing2D.Standard
{
    public partial class Scene01_Menu : Node3D
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
            GD.Print($"Scene01_Game._Ready()");
            AddFeature();
        }


        /// <summary>
        /// Called every frame. 'delta' is the elapsed time since the previous frame.
        /// </summary>
        public override void _Process(double delta)
        {
            base._Process(delta);
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
            MenuFeature feature = new MenuFeature();
            feature.AddView(_menuView);
            mini.AddFeature<MenuFeature>(feature);

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
    }
}
        
        
