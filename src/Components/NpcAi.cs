using System.Numerics;

namespace Stedders.Components
{
    internal class NpcAi : Component
    {
        public float Speed { get; set; } = 100f;
        public float EatingSpeed { get; set; } = 5f;
        public Vector2 TargetPosition { get; set; }
        public float AttackRange { get; set; } = 15f;
        public float AttackDamage { get; set; } = 15;

        public EnemyAction Action;
        public float TimeSinceLastActionChanged = 10f;
        public float ThinkingTime = 10f;
        public float Belly = 0f;
        public float BellyMax = 300f;
    }
    public enum EnemyAction
    {
        Idle,
        Move,
        Attack,
        Eat,
        Flee,
        Dead
    }
}
