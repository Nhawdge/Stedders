namespace Stedders.Components
{
    internal class Player : Component
    {
        public float Throttle = 0f;
        public float MaxThrottle = 50f;
        public float MinThrottle = -50f;

        public float Speed = 10f;
    }
}
