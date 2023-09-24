using Raylib_CsLo;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Components
{
    internal class Tilemap : Render
    {
        internal long X = 0;
        internal long Y = 0;
        internal Vector2 Dimensions;
        public Tilemap(TextureKey key, long x, long y, float scale = 1, bool isCentered = true) : base(key, scale, isCentered)
        {
            X = x;
            Y = y;
            Dimensions = new Vector2(16, 16);
        }

        public override Rectangle Source
        {
            get
            {
                return new Rectangle(
                    X,
                    Y,
                    Dimensions.X * (IsFlipped ? -1 : 1),
                    Dimensions.Y
                );
            }
        }

        public virtual Rectangle Destination
        {
            get => new Rectangle(
                    Position.X * Scale,
                    Position.Y * Scale,
                    Dimensions.X * Scale,
                    Dimensions.Y * Scale);
        }
    }
}
