
namespace RMC.Core.Templates
{
	/// <summary>
	/// TODO: Add comments
	/// </summary>
	public partial class Dog : Animal
	{
		//  Events ----------------------------------------
        
		
		//  Properties ------------------------------------
        
		
		//  Fields ----------------------------------------
        
		
		//  Initialization  -------------------------------
		public Dog() : base ()
		{
			
		}
        
		//  Godot Methods ---------------------------------
		/// <summary>
		/// Called when the node enters the scene tree for the first time.
		/// </summary>
		public override void _Ready()
		{
			base._Ready();
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
			base._ExitTree();
		}

		
		//  Methods ---------------------------------------
        
		
		//  Event Handlers --------------------------------
	}
}