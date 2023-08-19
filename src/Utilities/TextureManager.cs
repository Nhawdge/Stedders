using Raylib_CsLo;

namespace Stedders.Utilities
{
    public class TextureManager
    {
        public Dictionary<TextureKey, Texture> TextureStore { get; set; } = new();

        public void LoadTextures()
        {
            TextureStore.Add(TextureKey.Mech1, Raylib.LoadTexture("Assets/Art/Mech1.png"));
            TextureStore.Add(TextureKey.Mech2, Raylib.LoadTexture("Assets/Art/fastmechwalksample.png"));
            TextureStore.Add(TextureKey.Mech2Top, Raylib.LoadTexture("Assets/Art/Mech2Top.png"));
            TextureStore.Add(TextureKey.Background, Raylib.LoadTexture("Assets/Art/background.png"));
            TextureStore.Add(TextureKey.Plant1, Raylib.LoadTexture("Assets/Art/Plant1.png"));
            TextureStore.Add(TextureKey.Waterbeam, Raylib.LoadTexture("Assets/Art/waterbeam.png"));
            TextureStore.Add(TextureKey.Enemy1, Raylib.LoadTexture("Assets/Art/Enemy1.png"));
            TextureStore.Add(TextureKey.Person1, Raylib.LoadTexture("Assets/Art/Person1.png"));
            TextureStore.Add(TextureKey.Laser, Raylib.LoadTexture("Assets/Art/Laser.png"));
            TextureStore.Add(TextureKey.WaterCannon, Raylib.LoadTexture("Assets/Art/WaterCannon.png"));
            TextureStore.Add(TextureKey.SeedCannon, Raylib.LoadTexture("Assets/Art/SeedCannon.png"));
            TextureStore.Add(TextureKey.LaserCannon, Raylib.LoadTexture("Assets/Art/LaserCannon.png"));
            TextureStore.Add(TextureKey.Harvester, Raylib.LoadTexture("Assets/Art/Harvester.png"));
            TextureStore.Add(TextureKey.MechDirection, Raylib.LoadTexture("Assets/Art/MechDirection.png"));
            TextureStore.Add(TextureKey.Barn, Raylib.LoadTexture("Assets/Art/Barn.png"));
            TextureStore.Add(TextureKey.Silo, Raylib.LoadTexture("Assets/Art/Silo.png"));
            TextureStore.Add(TextureKey.Field1, Raylib.LoadTexture("Assets/Art/Field1.png"));
            TextureStore.Add(TextureKey.WaterBlob, Raylib.LoadTexture("Assets/Art/WaterBlob.png"));
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
        MechDirection,
        Barn,
        Silo,
        Field1,
        WaterBlob,
    }
}