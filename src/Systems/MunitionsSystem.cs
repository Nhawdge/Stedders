using Stedders;
using Stedders.Components;

namespace Stedders.Systems
{
    internal class MunitionsSystem : GameSystem
    {
        public MunitionsSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State != States.Game)
            {
                return;
            }

            var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(Equipment)));

            foreach (var entity in allEntities)
            {

            }
        }
    }
}
