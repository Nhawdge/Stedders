using Stedders.Entities;

namespace Stedders.Utilities
{
    internal class BaseScene
    {
        internal Action Onload;
        internal Action OnUnload;
        internal Action GameLoop;
        public List<Entity> Entities = new();
    }
}
