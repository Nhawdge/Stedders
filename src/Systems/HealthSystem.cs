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
                var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(Health)));
                var entitiesToRemove = new List<Entity>();
                foreach (var entity in allEntities)
                {
                    var myHealth = entity.GetComponent<Health>();
                    if (myHealth.CurrentHealth <= 0)
                    {
                        entitiesToRemove.Add(entity);
                    }
                }
                entitiesToRemove.ForEach(x => Engine.Entities.Remove(x));

            }
        }
    }
}
