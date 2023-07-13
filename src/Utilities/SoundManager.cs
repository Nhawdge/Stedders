using Raylib_CsLo;

namespace Stedders.Utilities
{
    public class SoundManager
    {
        public Dictionary<SoundKey, Sound> SoundStore { get; set; } = new();
        public Dictionary<SoundKey, Music> MusicStore { get; set; } = new();

        public void LoadSounds()
        {
            SoundStore.Add(SoundKey.Ambiance, Raylib.LoadSound("Assets/Sound/Stedders_Day_Map_Ambience.wav"));
            SoundStore.Add(SoundKey.FlowerGrowth, Raylib.LoadSound("Assets/Sound/Stedders_Flower_Growing.wav"));
            
            MusicStore.Add(SoundKey.Ambiance, Raylib.LoadMusicStream("Assets/Sound/Stedders_Day_Map_Ambience.wav"));
            MusicStore.Add(SoundKey.FlowerGrowth, Raylib.LoadMusicStream("Assets/Sound/Stedders_Flower_Growing.wav"));
        }
        public Sound GetSound(SoundKey key)
        {
            if (SoundStore.Count <= 0)
            {
                LoadSounds();
            }
            return SoundStore[key];
        }
        public Music GetMusic(SoundKey key)
        {
            if (MusicStore.Count <= 0)
            {
                LoadSounds();
            }
            return MusicStore[key];
        }
    }

    public enum SoundKey
    {
        Ambiance,
        FlowerGrowth,
    }
}