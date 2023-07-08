using Raylib_CsLo;
using System.Numerics;
using System.Text.Json;

namespace Stedders.Utilities
{
    public class TextureManager
    {
        public Dictionary<string, SpriteSheet> TextureStore { get; set; } = new();

        public void LoadTextures()
        {
            TextureStore.Add("Mech2", SpriteSheet.NewSheet("Assets/Mech2"));
        }
    }

    public class Frame
    {
        public Texture Texture;
        public Rectangle Source;
        public Vector2 Origin;
        public float Duration;
        public int TotalFrames;
    }

    public class SpriteSheet
    {
        public string Path = string.Empty;
        public Texture Texture;
        public Dictionary<string, Dictionary<int, Frame>> Animations = new();
        public int CurrentFrameIndex;
        public float FrameTime;
        public string CurrentAnimationKey;

        public Frame CurrentFrame()
        {
            if (Animations.TryGetValue(CurrentAnimationKey, out var frames))
                if (frames.TryGetValue(CurrentFrameIndex, out var frame))
                {
                    FrameTime += Raylib.GetFrameTime();
                    if (FrameTime > frame.Duration)
                    {
                        FrameTime -= frame.Duration;
                        CurrentFrameIndex = (CurrentFrameIndex + 1) % frame.TotalFrames;

                    }
                    return frame;
                }

            return null;
        }
        public void Play(string animationKey)
        {
            if (Animations.TryGetValue(CurrentAnimationKey, out var frames))
                this.CurrentAnimationKey = animationKey;
            else
                throw new ArgumentException(animationKey + " is not a valid animation key for " + Path);
        }

        public static SpriteSheet NewSheet(string path)
        {
            var sheet = new SpriteSheet();
            sheet.Path = path;
            var data = File.ReadAllText(path + ".json");
            var json = JsonSerializer.Deserialize<ImageAtlas>(data);
            if (json is null)
            {
                throw new Exception("Failed to load atlas.json");
            }

            sheet.Texture = Raylib.LoadTexture(path + ".png");

            foreach (var frameTag in json.meta.frameTags)
            {
                //sheet.Animations.Add(frameTag.name);
            }


            return sheet;

        }

    }

    public class ImageAtlas
    {
        public Frame[] frames { get; set; }
        public Meta meta { get; set; }


        public class Meta
        {
            public string app { get; set; }
            public string version { get; set; }
            public string image { get; set; }
            public string format { get; set; }
            public Size size { get; set; }
            public string scale { get; set; }
            public Frametag[] frameTags { get; set; }
            public Layer[] layers { get; set; }
            public object[] slices { get; set; }
        }

        public class Size
        {
            public int w { get; set; }
            public int h { get; set; }
        }

        public class Frametag
        {
            public string name { get; set; }
            public int from { get; set; }
            public int to { get; set; }
            public string direction { get; set; }
        }

        public class Layer
        {
            public string name { get; set; }
            public int opacity { get; set; }
            public string blendMode { get; set; }
        }

        public class Frame
        {
            public string filename { get; set; }
            public Frame1 frame { get; set; }
            public bool rotated { get; set; }
            public bool trimmed { get; set; }
            public Spritesourcesize spriteSourceSize { get; set; }
            public Sourcesize sourceSize { get; set; }
            public int duration { get; set; }
        }

        public class Frame1
        {
            public int x { get; set; }
            public int y { get; set; }
            public int w { get; set; }
            public int h { get; set; }
        }

        public class Spritesourcesize
        {
            public int x { get; set; }
            public int y { get; set; }
            public int w { get; set; }
            public int h { get; set; }
        }

        public class Sourcesize
        {
            public int w { get; set; }
            public int h { get; set; }
        }
    }
}