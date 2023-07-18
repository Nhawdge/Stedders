using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Systems
{
    internal class EquipmentSystem : GameSystem
    {
        public EquipmentSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(Equipment)));
                var allEnemies = Engine.Entities.Where(x => x.HasTypes(typeof(NpcAi), typeof(Sprite), typeof(Health)));
                IEnumerable<Entity> entitiesToAdd = new List<Entity>();
                IEnumerable<Entity> entitiesToRemove = new List<Entity>();
                foreach (var entity in allEntities)
                {
                    var equipment = entity.GetComponents<Equipment>();
                    foreach (var item in equipment)
                    {
                        item.ShotCooldown -= item.ShotCoolDownRate * Raylib.GetFrameTime();
                        item.ShotCooldown = Math.Max(item.ShotCooldown, 0);
                        item.Idle(entity, item);
                        if (item.IsFiring)
                        {
                            item.IsFiring = false;
                            (entitiesToAdd, entitiesToRemove) = item.Fire(Engine.Entities, entity, item);
                        }
                    }
                }
                foreach (var entity in entitiesToRemove)
                {
                    Engine.Entities.Remove(entity);
                }
                foreach (var entity in entitiesToAdd)
                {
                    Engine.Entities.Add(entity);
                }

            }
        }


    }
}
