using Raylib_CsLo;
using Stedders.Entities;
using System.Numerics;

namespace Stedders.Utilities
{
    internal class BaseScene
    {
        internal Action Onload;
        internal Action OnUnload;
        internal Action GameLoop;

        public Vector2 MapEdge = new Vector2(4320, 2880);
        public List<Entity> Entities = new();

        public Dictionary<KeyboardKey, Action> KeyboardMapping = new Dictionary<KeyboardKey, Action>();
        public Dictionary<MouseButton, Action> MouseMapping = new Dictionary<MouseButton, Action>();
    }

}
