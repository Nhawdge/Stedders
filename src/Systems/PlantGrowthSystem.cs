using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using Stedders.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stedders.Systems
{
    internal class PlantGrowthSystem : GameSystem
    {
        public PlantGrowthSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(Plant)));
                var entitiesToRemove = new List<Entity>();
                foreach (var entity in allEntities)
                {
                    var plant = entity.GetComponent<Plant>();
                    if (plant.PlantBody <= 0)
                    {
                        entitiesToRemove.Add(entity);
                    }

                    plant.Growth += Raylib.GetFrameTime() * plant.GrowthRate * (state.TimeOfDay == TimeOfDay.Day ? plant.DayGrowthModifier : plant.NightGrowthModifier);

                    if (plant.Growth > plant.GrowthRequired)
                    {
                        var mySprite = entity.GetComponents<Sprite>().First();
                        if (plant.GrowthStage >= plant.MaxGrowthStage)
                        {
                            mySprite.Play($"Mature");
                            entity.Components.Add(new SoundAction(SoundKey.FlowerGrowth));
                            plant.Growth = 0;
                            plant.GrowthRate = 0f;
                        }
                        else
                        {
                            plant.GrowthStage++;
                            plant.Growth = 0;
                            mySprite.Play($"Grow{plant.GrowthStage}");
                        }
                    }
                }
                entitiesToRemove.ForEach(x => Engine.Entities.Remove(x));
            }
        }
    }
}
