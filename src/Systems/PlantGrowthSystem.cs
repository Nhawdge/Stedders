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
                var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(Field)));
                var entitiesToRemove = new List<Entity>();
                foreach (var entity in allEntities)
                {
                    var field = entity.GetComponent<Field>();
                    var sprite = entity.GetComponent<Sprite>();

                    if (field.WaterLevel > 1)
                    {
                        sprite.Play("Wet");
                    }
                    else
                    {
                        sprite.Play("Empty");
                    }

                    var plant = entity.GetComponent<Plant>();
                    if (plant is null)
                    {
                        continue;
                    }
                    if (plant.PlantBody <= 0)
                    {
                        entity.Components.Remove(plant);
                        var plantSprite = entity.GetComponents<Sprite>().First(x =>x.Key  == TextureKey.Plant1);
                        entity.Components.Remove(plantSprite);
                        field.HasCrop = false;
                    }
                    var growthModifier = state.TimeOfDay switch
                    {
                        TimeOfDay.Dawn => plant.DawnGrowthModifier,
                        TimeOfDay.Day => plant.DayGrowthModifier,
                        TimeOfDay.Dusk => plant.DuskGrowthModifier,
                        TimeOfDay.Night => plant.NightGrowthModifier,
                        _ => 0f
                    };

                    plant.Growth += Raylib.GetFrameTime() * plant.GrowthRate * (state.TimeOfDay == TimeOfDay.Day ? plant.DayGrowthModifier : plant.NightGrowthModifier);
                    if (field.WaterLevel > 0)
                    {
                        plant.Growth += Raylib.GetFrameTime() * plant.WaterConsumptionRate;
                        field.WaterLevel -= Raylib.GetFrameTime() * plant.WaterConsumptionRate;
                        field.WaterLevel = Math.Max(0, field.WaterLevel);
                    }

                    if (plant.Growth > plant.GrowthRequired)
                    {
                        var mySprite = entity.GetComponents<Sprite>().First(x => x.Key == TextureKey.Plant1);
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
