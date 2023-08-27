using Stedders.Components;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Entities
{
    public static class ArchetypeGenerator
    {
        public static Entity GeneratePlayerMech()
        {
            var player = new Entity();
            player.Components.Add(new Player());
            player.Components.Add(new SoundAction(SoundKey.Mech2EngineStart));

            var legs = new Sprite(TextureKey.Mech2, "Assets/Art/fastmechwalksample", 3, true)
            {
                MechPiece = MechPieces.Legs,
                CanRotate = false,
                ZIndex = 3,
            };
            player.Components.Add(legs);

            //var torso = new Sprite(engine.TextureManager.GetTexture(TextureKey.Mech2Top), "Assets/Art/Mech2Top", 3, true) { MechPiece = MechPieces.Torso, CanRotate = false, ZIndex = 2 };
            //player.Components.Add(torso);

            var startPos = new Vector2(2118, 2760);

            legs.Rotation = 0f;
            legs.Position = startPos;
            //torso.Position = startPos with { Y = startPos.Y - 30 /* * torso.Scale*/ };

            return player;
        }

        public static Entity GenerateEnemy(Vector2 position)
        {
            var enemy = new Entity();
            var sprite = new Sprite(TextureKey.Enemy1, "Assets/Art/Enemy1", 3, true)
            {
                ZIndex = 1,
                CanRotate = false,
                Position = position
            };
            enemy.Components.Add(sprite);
            enemy.Components.Add(new Health() { CurrentHealth = 100, MaxHealth = 100 });
            enemy.Components.Add(new NpcAi());

            return enemy;
        }

        public static Entity GeneratePlant(Vector2 position)
        {
            var plant = new Entity();
            plant.Components.Add(new Sprite(TextureKey.Plant1, "Assets/Art/Plant1", 3, false)
            {
                Position = position
            });
            plant.Components.Add(new Plant("Wiggle Root"));

            return plant;
        }

        public static Entity GenerateBarn( Vector2 position)
        {
            var barn = new Entity();
            var sprite = new Sprite(TextureKey.Barn, "Assets/Art/Barn", 3, true) { Position = position };

            barn.Components.Add(sprite);
            var barnComponent = new Barn();

            barnComponent.Equipment.Add(EquipmentManager.GenerateLaser());
            barnComponent.Equipment.Add(EquipmentManager.GenerateLaser());
            barnComponent.Equipment.Add(EquipmentManager.GenerateHarvester());
            barnComponent.Equipment.Add(EquipmentManager.GenerateSeeder());
            barnComponent.Equipment.Add(EquipmentManager.GenerateWaterCannon());
            barnComponent.Equipment.Add(EquipmentManager.GenerateWaterCannon());



            barn.Components.Add(barnComponent);

            return barn;
        }

        public static Entity GenerateSilo(Vector2 position)
        {
            var silo = new Entity();
            silo.Components.Add(new Silo());
            silo.Components.Add(new Sprite(TextureKey.Silo, "Assets/Art/Silo", 3, true) { Position = position });
            return silo;
        }

        internal static Entity GenerateField(Vector2 position, bool startWithPlant = false)
        {
            var field = new Entity();
            var sprite = new Sprite(TextureKey.Field1, "Assets/Art/Field1", 3, false) { Position = position };
            var fieldComponent = new Field();
            field.Components.Add(sprite);
            field.Components.Add(fieldComponent);
            if (startWithPlant)
            {
                fieldComponent.HasCrop = true;
                field.Components.Add(new Plant("Wiggle Root"));
                field.Components.Add(new Sprite(TextureKey.Plant1, "Assets/Art/Plant1", 3, false)
                {
                    Position = position
                });
            }
            return field;
        }
    }
}
