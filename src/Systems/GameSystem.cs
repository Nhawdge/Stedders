namespace Stedders.Systems
{
    public abstract class GameSystem
    {
        public GameEngine Engine { get; private set; }
        public GameSystem(GameEngine gameEngine)
        {
            Engine = gameEngine;
        }
        public abstract void Update();

        public virtual void UpdateNoCamera()
        {
            return;
        }
    }
}