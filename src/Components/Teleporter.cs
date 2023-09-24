using System.Numerics;

namespace Stedders.Components
{
    internal class Teleporter<T> : Component
    {
        public Vector2 Destination { get; set; }
        public Vector2 Position { get; set; }
        public T TeleportType { get; set; }
        public string Scene { get; set; }
    }
}
