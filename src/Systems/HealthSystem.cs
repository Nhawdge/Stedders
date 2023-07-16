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

                var allBuildings = Engine.Entities.Where(x => x.HasTypes(typeof(Building)));
                foreach (var entity in allBuildings)
                {
                    var myHealth = entity.GetComponent<Building>();
                    if (myHealth.Health <= 0)
                    {
                        //entitiesToRemove.Add(entity);
                        entity.Components.Remove(myHealth);
                    }
                    if (entity.HasTypes(typeof(Barn)))
                    {
                        var sprite = entity.GetComponent<Sprite>();
                        if (myHealth.Health > 50)
                        {

                            sprite.Play("Health100");
                        }
                        else if (myHealth.Health > 10)
                        {
                            sprite.Play("Health50");
                        }
                        else
                        {
                            sprite.Play("Health0");
                        }
                    }
                }
                entitiesToRemove.ForEach(x => Engine.Entities.Remove(x));

            }
        }
    }
}
