
using System;
using Godot;

namespace RMC.Racing2D.Mini.Model
{
    
    /// <summary>
    /// Data for configuration set in the main menu
    /// and used in the game for a car
    /// </summary>
    [Serializable]
    public partial class MenuConfiguration<T> : Node where T : Resource
    {
        //  Properties ------------------------------------
        public string Title { get; private set; }
        public T Resource { get; private set; }
        
        //  Fields ----------------------------------------
        
        //  Initialization  -------------------------------
        public MenuConfiguration(string title, T resource) 
        {
            Title = title;
            Resource = resource;
        }
        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
 
    }
}