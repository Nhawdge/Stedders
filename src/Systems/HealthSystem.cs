using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using Stedders.Utilities;

namespace Stedders.Systems
{
    internal class HealthSystem : GameSystem
    {
        public HealthSystem(GameEngine gameEngine) : base(gameEngine)
        {
            Rand = new Random();
        }

        public Random Rand { get; }

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
                    myHealth.CurrentHealth += myHealth.RegenRate * Raylib.GetFrameTime();
                    if (myHealth.CurrentHealth <= 0)
                    {
                        if (entity.HasTypes(typeof(NpcAi)))
                        {
                            var deathSoundOptions = new List<SoundKey>() { SoundKey.Enemy1Death1, SoundKey.Enemy1Death2, SoundKey.Enemy1Death3 };
                            Engine.Singleton.Components.Add(new SoundAction(deathSoundOptions[Rand.Next(0, deathSoundOptions.Count-1)]));
                            Engine.Singleton.GetComponent<GameState>().Stats.TotalEnemiesKilled += 1;
                        }
                        entitiesToRemove.Add(entity);
                    }
                }

                var allBuildings = Engine.Entities.Where(x => x.HasTypes(typeof(Building)));
                foreach (var entity in allBuildings)
                {
                    var myHealth = entity.GetComponent<Building>();
                    if (myHealth.Health <= 0)
                    {
                        entity.Components.Remove(myHealth);
                    }
                    if (entity.HasTypes(typeof(Barn)) || entity.HasTypes(typeof(Silo)))
                    {
                        var sprite = entity.GetComponent<Sprite>();
                        var healthPercent = myHealth.Health / myHealth.MaxHealth * 100;
                        if (healthPercent > 50)
                        {
                            sprite.Play("Health100");
                        }
                        else if (healthPercent > 10)
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
