namespace Stedders.Components
{
    internal class NpcAi : Component
    {
        public float Speed { get; set; } = 30f;
        public EnemyAction Action;
        public float TimeSinceLastActionChanged;
        public float ThinkingTime = 10f;
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
