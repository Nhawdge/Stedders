using System.Numerics;

namespace Stedders.Components
{
    internal class Teleporter : Component
    {
        public Vector2 Destination { get; set; }
        public Vector2 Position { get; set; }
        public string DestinationSceneName { get;set; }
    }
}
