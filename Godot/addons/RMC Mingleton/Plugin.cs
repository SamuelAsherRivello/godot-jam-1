#if TOOLS
using Godot;

namespace RMC.Mingletons
{
    [Tool]
    public partial class Plugin : EditorPlugin
    {
        
        private const string pathRMC = "RMC";
        private const string pathRMCMingleton = "RMC/RMC Mingleton";
        private const string pathRMCMingleton2 = "RMC/RMC Mingleton/Test";
        public override void _EnterTree()
        {
            AddToolMenuItem(pathRMC, new Callable(this, "OnTest"));
            AddToolSubmenuItem(pathRMCMingleton, new PopupMenu());
            AddToolSubmenuItem(pathRMCMingleton2, new PopupMenu());
        }

        public override void _ExitTree()
        {
            RemoveToolMenuItem(pathRMC);
            RemoveToolMenuItem(pathRMCMingleton);
            RemoveToolMenuItem(pathRMCMingleton2);
        }

        private void OnTest()
        {
            GD.Print("OnTest");
        }
    }
}
#endif