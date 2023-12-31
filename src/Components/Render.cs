using Raylib_CsLo;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Components
{
    public class Render : Component
    {
        public Texture Texture;
        public bool IsFlipped = false;
        public OriginAlignment OriginPos = OriginAlignment.Center;
        public int Column = 0;
        public int Row = 0;
        public int SpriteWidth = 0;
        public int SpriteHeight = 0;
        public float Scale = 1f;
        public float Rotation = 0f;
        public bool CanRotate = true;
        public float ZIndex = 0f;
        public float RotationAsRadians => (Rotation - 90) * (float)(Math.PI / 180);
        public Vector2 Position;
        public Color Color;
        public MechPieces MechPiece;
        public TextureKey Key;

        public float RenderRotation
        {
            get => CanRotate ? Rotation : 0f;
        }

        public virtual Vector2 Origin
        {
            get
            {
                switch (OriginPos)
                {
                    case OriginAlignment.Center:
                        return new Vector2(SpriteWidth / 2 * Scale, SpriteHeight / 2 * Scale);
                    case OriginAlignment.LeftCenter:
                        return (new Vector2(0, SpriteHeight / 2 * Scale));
                    case OriginAlignment.LeftBottom:
                        return new Vector2(0, SpriteHeight * Scale);
                    case OriginAlignment.LeftTop:
                    default:
                        return Vector2.Zero;
                }
            }
        }

        public enum OriginAlignment
        {
            Center,
            LeftTop,
            LeftCenter,
            LeftBottom,
        }

        public virtual Rectangle Source
        {
            get => new Rectangle(
                    Column * SpriteWidth,
                    Row * SpriteHeight,
                    SpriteWidth * (IsFlipped ? -1 : 1),
                    SpriteHeight
                    );
        }

        public virtual Rectangle Destination
        {
            get => new Rectangle(
                    Position.X,
                    Position.Y,
                    SpriteWidth * Scale,
                    SpriteHeight * Scale);
        }

        public Render(TextureKey key, float scale = 1, bool isCentered = true)
        {
            Texture = TextureManager.Instance.GetTexture(key);
            Key = key;
            Position = Vector2.Zero;
            Scale = scale;
            Color = Raylib.WHITE;
            OriginPos = isCentered ? OriginAlignment.Center : OriginAlignment.LeftTop;
            SpriteWidth = Texture.width;
            SpriteHeight = Texture.height;
        }
    }

    public enum MechPieces
    {
        None,
        Torso,
        Legs
    }
}