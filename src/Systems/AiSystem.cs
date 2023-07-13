using Stedders.Components;

namespace Stedders.Systems
{
    internal class AiSystem : GameSystem
    {
        public AiSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var enemyBases = Engine.Entities.Where(x => x.GetComponent<NpcAi>() is not null);

                foreach (var entityBase in enemyBases)
                {

                 
                }
            }
        }
    }
}
