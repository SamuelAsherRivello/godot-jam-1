using Godot;

namespace RMC.Racing2D.Vehicles
{
    [GlobalClass, Tool]
    public partial class AICharacteristics : Resource
    {
        [Export(PropertyHint.Range, "1.0f, 1000.0f")]
        private float _MaxSpeed = 40.0f;

        [Export(PropertyHint.Range, "1.0f, 1000.0f")]
        private float _MinCurveSpeed = 5.0f;

        public float MaxSpeed
        {
            get => _MaxSpeed;
        }

        public float MinCurveSpeed
        {
            get => _MinCurveSpeed;
        }
    }

}
