using Raylib_CsLo;

namespace Stedders.Utilities
{
    internal class SoundManager
    {
        public Dictionary<SoundKey, Texture> SoundStore { get; set; } = new();

        public void LoadTextures()
        {
            SoundStore.Add(SoundKey.Ambiance, Raylib.LoadTexture("Assets/Sound/Stedders_Day_Map_Ambience.wav"));
            SoundStore.Add(SoundKey.FlowerGrowth, Raylib.LoadTexture("Assets/Sound/Stedders_Flower_Growing.wav"));
        }
        public Texture GetTexture(SoundKey key)
        {
            return SoundStore[key];
        }
    }

    public enum SoundKey
    {
        Ambiance,
        FlowerGrowth,
    }
}