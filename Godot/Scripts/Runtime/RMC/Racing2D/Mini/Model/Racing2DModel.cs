using RMC.Core.Observables;
using RMC.Mini;
using RMC.Mini.Model;

namespace RMC.Racing2D.Mini.Model
{
    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public class Racing2DModel: BaseModel // Extending 'base' is optional
    {
        //  Properties ------------------------------------
        public Observable<bool> HasNavigationBack { get { return _hasNavigationBack;} }
        public Observable<int> LapCurrent { get { return _lapCurrent;} }
        public Observable<int> LapMax{ get { return _lapMax;} }
        
        //  Fields ----------------------------------------
        private readonly Observable<bool> _hasNavigationBack = new Observable<bool>();
        private readonly Observable<bool> _hasLoadedService = new Observable<bool>();
        private readonly Observable<int> _lapCurrent = new Observable<int>();
        private readonly Observable<int> _lapMax = new Observable<int>();
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _hasNavigationBack.Value = false;
                _hasLoadedService.Value = false;
                _lapCurrent.Value = 1;
                _lapMax.Value = 3;
            }
        }

        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
 
    }
}