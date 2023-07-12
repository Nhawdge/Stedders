namespace Stedders.Components
{
    internal class Player : Component
    {
        public float Throttle = 0f;
        public float MaxThrottle = 5f;
        public float MinThrottle = -5f;

        public float Speed = 2f;
    }
}
