using RMC.Mingletons;
using RMC.Mini;
using RMC.Mini.Controller;
using RMC.Mini.Features;
using RMC.Mini.Model;
using RMC.Mini.Service;
using RMC.Mini.View;
using RMC.Racing2D.Mini.Model;

namespace RMC.Racing2D.Mini
{
    /// <summary>
    /// See <see cref="MiniMvcs{TContext,TModel,TView,TController,TService}"/>
    /// </summary>
    public class Racing2DMini: MiniMvcs
            <Context, 
                IModel, 
                IView, 
                IController,
                IService>
    {
        
        //  Fields ----------------------------------------

        
        //  Properties ------------------------------------
        /// <summary>
        /// This is an optional addon that gives this <see cref="ConfiguratorMini"/>
        /// the support of a <see cref="IFeatureBuilder{F}"/> to easily 
        /// add and remove <see cref="IFeature"/>. 
        /// </summary>
        private FeatureBuilder FeatureBuilder { get; set; }
        
        //  Initialization  -------------------------------
        
        /// <summary>
        /// Since this constructor is called by <see cref="Mingleton"/>,
        /// we also want to initialize within
        /// </summary>
        public Racing2DMini()
        {
            Initialize();
        }
        
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                // Context
                _context = new Context();
                
                // Model
                Racing2DModel model = new Racing2DModel();
         
                // Service
                //(none)
                
                // Feature
                FeatureBuilder = new FeatureBuilder();
                
                // Initialize
                model.Initialize(_context); //Added to locator inside
                FeatureBuilder.Initialize(this);
                
            }
        }

        
        //  Methods  -------------------------------
        public bool HasFeature<TFeature>(string key = "") where TFeature : IFeature
        {
            RequireIsInitialized();
            
            return FeatureBuilder.HasFeature<TFeature>(key);
        }
        
        public void AddFeature<TFeature>(TFeature feature, string key = "") where TFeature : IFeature
        {
            RequireIsInitialized();

            FeatureBuilder.AddFeature<TFeature>(feature, key);
        }
        
        public void RemoveFeature<TFeature>(string key = "") where TFeature : IFeature
        {
            RequireIsInitialized();

            FeatureBuilder.RemoveFeature<TFeature>(key);
        }
        
    }
}