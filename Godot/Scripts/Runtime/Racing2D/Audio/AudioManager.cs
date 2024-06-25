using System.Collections.Generic;
using Godot;
using RMC.Core.Utilities;
using RMC.Mingletons;

namespace RMC.Racing2D.Audio
{
    /// <summary>
    /// This workflow is for a Singleton that is a Node
    ///
    /// Where you want to add it to the Mingleton in the _Ready method
    /// </summary>
    public partial class AudioManager : Node
    {
        private readonly List<AudioStreamPlayer3D> _audioStreamPlayer3Ds = new List<AudioStreamPlayer3D>();
        private readonly Dictionary<string, AudioStreamMP3> _audioStreamMp3s = new Dictionary<string, AudioStreamMP3>();

        public override async void _Ready()
        {
            base._Ready();
            
            // Make AudioManager a Singleton
            await Mingleton.InstantiateAsync();
            Mingleton.Instance.AddSingleton<AudioManager>(this);
            
            // Add all children to list. It's WHERE to play.
            foreach (Node child in GetChildren())
            {
                if (child is AudioStreamPlayer3D audioStreamPlayer3D)
                {
                    _audioStreamPlayer3Ds.Add(audioStreamPlayer3D);
                }
            }
        }

        public void PlayAudio(string fileName)
        {
            AudioStreamPlayer3D audioStreamPlayer3D = GetAvailableAudioStreamPlayer3D();
            audioStreamPlayer3D.Stream = GetOrCreateCachedAudioStream(fileName);
        
            GD.Print("AudioManager.PlayAudio() = " + fileName);
            audioStreamPlayer3D.Play();
        }
        
        public AudioStreamPlayer3D GetAvailableAudioStreamPlayer3D ()
        {
            // Find non-busy player. It's WHERE to play.
            foreach (AudioStreamPlayer3D audioStreamPlayer3D in _audioStreamPlayer3Ds)
            {
                if (!audioStreamPlayer3D.Playing)
                {
                    return audioStreamPlayer3D;
                }
            }
            return null;
        }
        

        private AudioStreamMP3 GetOrCreateCachedAudioStream(string fileName)
        {
            // Find/creat a cached Stream. It's WHAT to play
            if (!_audioStreamMp3s.TryGetValue(fileName, out AudioStreamMP3 audioStream))
            {
                audioStream = LoadMp3ByFilename(fileName);
                _audioStreamMp3s.Add(fileName, audioStream);
            }

            return audioStream;
        }
        
        
        /// <summary>
        /// Load MP3 from local files system by name
        /// </summary>
        /// <param name="fileName">Such as "Coin01.mp3"</param>
        /// <returns></returns>
        private AudioStreamMP3 LoadMp3ByFilename(string fileName)
        {
            string filePath = FileAccessUtility.FindFileOnceInResources(fileName);
            AudioStreamMP3 audioStreamMp3 = GD.Load<AudioStreamMP3>(filePath);
            return audioStreamMp3;
        }
    }
}