
using System;
using Godot;

namespace RMC.Racing2D.Mini.Model
{
    [Serializable]
    public partial class PlayerMenuConfiguration : MenuConfiguration<Resource> // Change to <PlayerResource> or?
    {
        //  Properties ------------------------------------
        
        //  Fields ----------------------------------------
        
        //  Initialization  -------------------------------
        public PlayerMenuConfiguration(string title, Resource resource) 
            : base(title, resource)
        {
        }

        public PlayerMenuConfiguration(): base ("", null)
        {
        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
 
    }
}