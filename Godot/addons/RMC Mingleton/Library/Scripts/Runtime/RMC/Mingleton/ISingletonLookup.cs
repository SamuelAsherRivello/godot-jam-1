using System;

namespace RMC.Mingletons
{
    public interface ISingletonLookup : IDisposable
    {
        //  Properties ----------------------------------------
        
        //  Methods -------------------------------------------
        void AddSingleton<T>(T instance, string key = "") where T : class;
        bool HasSingleton<T>(string key = "") where T : class;
        T GetSingleton<T>(string key = "") where T : class;
        void RemoveSingleton<T>(string key = "") where T : class;
    }
}