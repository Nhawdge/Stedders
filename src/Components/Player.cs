using Stedders.Resources;
using System.Numerics;

namespace Stedders.Components
{
    internal class Player : Component
    {
        public float Speed = 100f;
        public Vector2 Movement = Vector2.Zero;

        public List<Equipment> Inventory = ResourceManager.Instance.PlayerInventory;
    }
}
