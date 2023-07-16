using Raylib_CsLo;

namespace Stedders.Utilities
{
    public class SoundManager
    {
        public Dictionary<SoundKey, Sound> SoundStore { get; set; } = new();
        public Dictionary<MusicKey, Music> MusicStore { get; set; } = new();

        public void LoadSounds()
        {
            SoundStore.Add(SoundKey.FlowerGrowth, Raylib.LoadSound("Assets/Sound/Stedders_Flower_Growing.wav"));
            SoundStore.Add(SoundKey.Laser, Raylib.LoadSound("Assets/Sound/Laser.wav"));

            MusicStore.Add(MusicKey.Ambiance, Raylib.LoadMusicStream("Assets/Sound/Stedders_Day_Map_Ambience.wav"));
            MusicStore.Add(MusicKey.Menu, Raylib.LoadMusicStream("Assets/Sound/Stedders_Menu_Music.wav"));
            MusicStore.Add(MusicKey.GamePlay, Raylib.LoadMusicStream("Assets/Sound/Stedders_Gameplay_Music.wav"));
        }

        public Sound GetSound(SoundKey key)
        {
            if (SoundStore.Count <= 0)
            {
                LoadSounds();
            }
            return SoundStore[key];
        }
        public Music GetMusic(MusicKey key)
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
        FlowerGrowth,
        Laser,
    }

    public enum MusicKey // probably C# (Get it??)
    {
        Ambiance, 
        Menu,
        GamePlay,
    }
}