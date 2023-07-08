using Raylib_CsLo;
using System.Net.Security;
using System.Numerics;

namespace Stedders.Components
{
    public class Sprite : Component
    {
        public Texture Texture;
        public bool IsFlipped = false;
        public bool IsCentered = true;
        public int Column = 0;
        public int Row = 0;
        public int SpriteWidth = 0;
        public int SpriteHeight = 0;
        public float Scale = 1f;
        public float Rotation = 0f;
        public float RotationAsRadians => (Rotation - 90) * (float)(Math.PI / 180);
        public Vector2 Position;
        public Color Color;
        public int FrameCounter = 0;
        public MechPieces MechPiece;

        public Vector2 Origin
        {
            get
            {
                if (IsCentered)
                    return new Vector2(SpriteWidth / 2 * Scale, SpriteHeight / 2 * Scale);
                return Vector2.Zero;
            }
        }

        public Rectangle Source
        {
            get => new Rectangle(
                    Column * SpriteWidth,
                    Row * SpriteHeight,
                    SpriteWidth * (IsFlipped ? -1 : 1),
                    SpriteHeight
                    );
        }

        public Rectangle Destination
        {
            get => new Rectangle(
                    Position.X,
                    Position.Y,
                    SpriteWidth * Scale,
                    SpriteHeight * Scale);
        }

        public Sprite(Texture texture, int col = 0, int row = 0, float scale = 1, bool isCentered = true)
        {
            Texture = texture;
            Position = Vector2.Zero;
            Scale = scale;
            Color = Raylib.WHITE;
            IsCentered = isCentered;
            Column = col;
            Row = row;
            SpriteWidth = texture.width;
            SpriteHeight = texture.height;
        }

        public enum MechPieces
        {
            None,
            Torso,
            Legs
        }
    }
}