using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using Stedders.Utilities;

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
                var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(Plant), typeof(Field)));
                var entitiesToRemove = new List<Entity>();
                foreach (var entity in allEntities)
                {
                    var field = entity.GetComponent<Field>();
                    var plant = entity.GetComponent<Plant>();
                    if (plant.PlantBody <= 0)
                    {
                        entity.Components.Remove(plant);
                        var plantSprite = entity.GetComponents<Sprite>().First(x => x.AnimationDataPath == "Assets/Plant1");
                        entity.Components.Remove(plantSprite);
                        field.HasCrop = false;
                    }

                    plant.Growth += Raylib.GetFrameTime() * plant.GrowthRate * (state.TimeOfDay == TimeOfDay.Day ? plant.DayGrowthModifier : plant.NightGrowthModifier);

                    if (plant.Growth > plant.GrowthRequired)
                    {
                        var mySprite = entity.GetComponents<Sprite>().First(x => x.AnimationDataPath == "Assets/Plant1");
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
                            plant.PlantBody += 25 * plant.GrowthStage;
                            mySprite.Play($"Grow{plant.GrowthStage}");
                        }
                    }
                }
                entitiesToRemove.ForEach(x => Engine.Entities.Remove(x));
            }
        }
    }
}
