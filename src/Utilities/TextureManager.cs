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
            TextureStore.Add(TextureKey.Mech2Top, Raylib.LoadTexture("Assets/Mech2Top.png"));
            TextureStore.Add(TextureKey.Background, Raylib.LoadTexture("Assets/background.png"));
            TextureStore.Add(TextureKey.Plant1, Raylib.LoadTexture("Assets/Plant1.png"));
            TextureStore.Add(TextureKey.Waterbeam, Raylib.LoadTexture("Assets/waterbeam.png"));
            TextureStore.Add(TextureKey.Enemy1, Raylib.LoadTexture("Assets/Enemy1.png"));
            TextureStore.Add(TextureKey.Person1, Raylib.LoadTexture("Assets/Person1.png"));
            TextureStore.Add(TextureKey.Laser, Raylib.LoadTexture("Assets/Laser.png"));
            TextureStore.Add(TextureKey.WaterCannon, Raylib.LoadTexture("Assets/WaterCannon.png"));
            TextureStore.Add(TextureKey.SeedCannon, Raylib.LoadTexture("Assets/SeedCannon.png"));
            TextureStore.Add(TextureKey.LaserCannon, Raylib.LoadTexture("Assets/LaserCannon.png"));
            TextureStore.Add(TextureKey.Harvester, Raylib.LoadTexture("Assets/Harvester.png"));
        }
        public Texture GetTexture(TextureKey key)
        {
            if (TextureStore.Count <= 0)
            {
                LoadTextures();
            }
            return TextureStore[key];
        }
    }

    public enum TextureKey
    {
        Mech1,
        Mech2,
        Mech2Top,
        Background,
        Waterbeam,
        Plant1,
        Enemy1,
        Person1,
        Laser,
        LaserCannon,
        WaterCannon,
        SeedCannon,
        Harvester,
    }
}