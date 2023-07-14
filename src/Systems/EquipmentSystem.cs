using CsvHelper.Configuration.Attributes;
using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using System.Numerics;

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
                foreach (var entity in allEntities)
                {
                    var equipment = entity.GetComponents<Equipment>();
                    foreach (var item in equipment)
                    {
                        if (item.Ammo <= 0)
                        {
                            item.IsFiring = false;
                        }
                        if (item.IsFiring)
                        {
                            item.IsFiring = false;
                            item.Fire(allEnemies, entity, item);
                        }
                        else
                        {
                            item.Range = Math.Max(0, item.Range - (500 * Raylib.GetFrameTime()));
                        }
                    }
                }
            }
        }


    }
}
