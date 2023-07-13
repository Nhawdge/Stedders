using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Systems
{
    internal class HealthSystem : GameSystem
    {
        public HealthSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {

            }
        }
    }
}
