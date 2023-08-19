using Raylib_CsLo;
using Stedders.Utilities;
using System.Numerics;
using System.Text.Json;

namespace Stedders.Components
{
    public class Sprite : Render
    {
        public Dictionary<string, AnimationSets> Animations = new();
        public string AnimationDataPath = string.Empty;
        public float FrameCounter = 0;
        public int CurrentFrameIndex = 0;
        public AnimationSets CurrentAnimation;

        public Sprite(Texture texture, string animationDataPath, TextureKey key, float scale = 1, bool isCentered = true) : base(texture, key, scale, isCentered)
        {
            AnimationDataPath = animationDataPath;
            var data = File.ReadAllText(animationDataPath + ".json");
            var json = JsonSerializer.Deserialize<AsepriteData>(data);

            Animations = json.meta.frameTags.ToDictionary(x => (string)x.name,
                x => new AnimationSets(x.name, x.from, x.to, Enum.Parse<Direction>(x.direction),
                json.frames.Where(f => f.filename.StartsWith(x.name)).Select(z => new Frame(z.frame.x, z.frame.y, z.frame.w, z.frame.h, z.duration)).ToList()));

            CurrentAnimation = Animations.First().Value;
        }

        public override Rectangle Source
        {
            get
            {
                var frame = CurrentAnimation?.Frames[CurrentFrameIndex];
                if (frame == null)
                    return new Rectangle();
                FrameCounter += Raylib.GetFrameTime();
                if (FrameCounter > frame.Duration / 1000)
                {
                    FrameCounter -= frame.Duration / 1000;
                    var totalFrames = CurrentAnimation.To - CurrentAnimation.From + 1;
                    CurrentFrameIndex = (CurrentFrameIndex + 1) % totalFrames;
                }

                return new Rectangle(
                       frame.X,
                       frame.Y,
                       frame.W * (IsFlipped ? -1 : 1),
                       frame.H
                       );
            }
        }

        public override Rectangle Destination
        {
            get
            {
                var frame = CurrentAnimation?.Frames[CurrentFrameIndex];
                if (frame == null)
                    return new Rectangle();

                return new Rectangle(
                    Position.X,
                    Position.Y,
                    frame.W * Scale,
                    frame.H * Scale);
            }
        }

        public override Vector2 Origin
        {
            get
            {
                var frame = CurrentAnimation?.Frames[CurrentFrameIndex];
                if (frame == null)
                    return Vector2.Zero;

                switch (OriginPos)
                {
                    case OriginAlignment.Center:
                        return new Vector2( (frame.W / 2) * Scale,(frame.H / 2) * Scale);
                    case OriginAlignment.LeftCenter:
                        return (new Vector2(0, frame.Y + frame.H / 2 * Scale));
                    case OriginAlignment.LeftBottom:
                        return new Vector2(0, frame.Y + frame.H * Scale);
                    case OriginAlignment.LeftTop:
                    default:
                        return Vector2.Zero;
                }
            }
        }

        public void Play(string animationName, bool forceStart = false)
        {
            if (CurrentAnimation?.Name == animationName && forceStart == false)
                return;
            if (Animations.TryGetValue(animationName, out var animations))
            {
                this.CurrentAnimation = animations;
                CurrentFrameIndex = 0;
            }
            //else
            //    throw new ArgumentException($"Invalid Animation name '{animationName}' for '{AnimationDataPath}'");
        }

        public record AnimationSets(string Name, int From, int To, Direction direction, List<Frame> Frames);
        //{ "name": "WalkD", "from": 0, "to": 3, "direction": "forward" },
        public record Frame(int X, int Y, int W, int H, float Duration);

        public enum Direction
        {
            forward,
            backward,
            pingpong
        }
    }
}