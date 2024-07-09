using Godot;

namespace RMC.Racing2D.Vehicles
{
    [GlobalClass, Tool]
    public partial class CarAppearance : Resource
    {
        [Export]
        private Color _BodyColor;

        public Color BodyColor
        {
            get => _BodyColor;
        }
    }

}
