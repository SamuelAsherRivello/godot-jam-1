
using System;
using Godot;

namespace RMC.Racing2D.Mini.Model
{
    
    [Serializable]
    public partial class EnemyMenuConfiguration : MenuConfiguration<Resource> // Change to <EnemyResource> or?
    {
        //  Properties ------------------------------------
        
        //  Fields ----------------------------------------
        
        //  Initialization  -------------------------------
        public EnemyMenuConfiguration(string title, Resource resource) 
            : base(title, resource)
        {
        }
        
        public EnemyMenuConfiguration(): base ("", null)
        {
        }
        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
 
    }
}