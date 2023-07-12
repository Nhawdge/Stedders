using Raylib_CsLo;
using Stedders.Components;
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
                foreach (var entity in allEntities)
                {
                    var plant = entity.GetComponent<Plant>();
                    plant.Growth += Raylib.GetFrameTime() * plant.GrowthRate * (state.TimeOfDay == TimeOfDay.Day ? 1 : 0.5f);

                    if (plant.Growth > plant.GrowthRequired)
                    {
                        var mySprite = entity.GetComponents<Sprite>().First();
                        if (plant.GrowthStage >= plant.MaxGrowthStage)
                        {
                            Console.WriteLine("Growth completed");
                            mySprite.Play($"Mature");
                        }
                        else
                        {

                            plant.GrowthStage++;
                            plant.Growth = 0;
                            mySprite.Play($"Grow{plant.GrowthStage}");
                        }
                    }
                }
            }
        }
    }
}
