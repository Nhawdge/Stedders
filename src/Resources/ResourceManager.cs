using Stedders.Components;

namespace Stedders.Resources
{
    internal class ResourceManager
    {
        private ResourceManager() { }

        public static ResourceManager Instance { get; } = new();

        public List<Equipment> PlayerInventory = new();

    }
}
