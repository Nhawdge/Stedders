using Raylib_CsLo;

namespace Stedders.Utilities
{
    public class TextureManager
    {
        public Dictionary<TextureKey, Texture> TextureStore { get; set; } = new();

        public void LoadTextures()
        {
            TextureStore.Add(TextureKey.Mech1, Raylib.LoadTexture("Assets/Mech1.png"));
            TextureStore.Add(TextureKey.Mech2, Raylib.LoadTexture("Assets/Mech2.png"));
            TextureStore.Add(TextureKey.Background, Raylib.LoadTexture("Assets/background.png"));
            TextureStore.Add(TextureKey.Plant1, Raylib.LoadTexture("Assets/Plant1.png"));
            TextureStore.Add(TextureKey.Waterbeam, Raylib.LoadTexture("Assets/waterbeam.png"));
        }
        public Texture GetTexture(TextureKey key)
        {
            return TextureStore[key];
        }
    }

    public enum TextureKey
    {
        Mech1,
        Mech2,
        Background,
        Waterbeam,
        Plant1
    }
}