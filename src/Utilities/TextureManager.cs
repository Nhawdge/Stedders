using Raylib_CsLo;
using System.Text.Json;

namespace Stedders.Utilities
{
    public class TextureManager
    {
        public Dictionary<Textures, Texture> TextureStore { get; set; } = new();

        public void LoadTextures()
        {
            //var data = File.ReadAllText("Assets/atlas.json");
            //var json = JsonSerializer.Deserialize<ImageAtlas>(data);
            //if (json is null)
            //{
            //    throw new Exception("Failed to load atlas.json");
            //}
            //var atlasTexture = Raylib.LoadTexture("Assets/atlas.png");

            //foreach (var texture in json.sprites)
            //{
            //    TextureStore.Add(Enum.Parse<Textures>(texture.nameId), atlasTexture);
            //}

            //TextureStore.Add(Textures.Bob, Raylib.LoadTexture("Assets/Bob.png"));
            //TextureStore.Add(Textures.Tim, Raylib.LoadTexture("Assets/Tim.png"));
            //TextureStore.Add(Textures.Kevin, Raylib.LoadTexture("Assets/Kevin.png"));
        }
    }

    public enum Textures
    {
        BaseTent,
        Bob,
        Tim,
        Kevin,
    }
}
