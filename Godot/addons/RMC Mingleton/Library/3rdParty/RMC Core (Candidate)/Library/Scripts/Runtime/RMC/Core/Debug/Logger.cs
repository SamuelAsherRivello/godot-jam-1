using Godot;

namespace RMC.Core.Debug
{
    public class Logger
    {
        //  Properties ----------------------------------------
        public bool IsEnabled { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        //  Initialization ------------------------------------
        public Logger(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        //  Methods -------------------------------------------
        public void GDPrint(string message)
        {
            if (IsEnabled)
            {
                GD.Print($"{Prefix} {message} {Suffix}");
            }
        }

        public void GDPrintErr(string message)
        {
            if (IsEnabled)
            {
                GD.PrintErr($"{Prefix} {message} {Suffix}");
            }
        }
    }
}