using Stedders.Resources;

namespace Stedders.Components
{
    internal class Player : Component
    {
        public float Throttle = 0f;
        public float MaxThrottle = 5f;
        public float MinThrottle = -5f;

        public float Speed = 2f;

        public List<Equipment> Inventory = ResourceManager.Instance.PlayerInventory;

        //public float BioMassContainer = 0f;
        //public float MaxBioMassContainer = 100f;
    }
}
