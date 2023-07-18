using System.Numerics;

namespace Stedders.Components
{
    internal class Motion : Component
    {
        public Vector2 Target;
        public float Speed = 1f;
        public Action OnTarget = () => { };
    }
}
