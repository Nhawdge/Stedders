using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
using System;
using System.Numerics;

namespace Stedders.Entities
{
    public static class ArchetypeGenerator
    {
        public static Entity GeneratePlayer(Vector2 startPos)
        {
            var player = new Entity();  
            player.Components.Add(new Player());

            var sprite = new Sprite(TextureKey.OneBitKenney, "Assets/KenneyAssets/onebitkenney", 3, true)
            {
                CanRotate = false,
                ZIndex = 3,
                Color = Raylib.ORANGE, 
            };
            sprite.Play("Stedder1");
            player.Components.Add(sprite);

            sprite.Rotation = 0f;
            sprite.Position = startPos;
            
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

        public static Entity GenerateBarn(Vector2 position)
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

        internal static Entity GenerateAnchor(Vector2 pos, Vector2 dest, string destinationSceneName)
        {
            var anchor = new Entity();
            anchor.Components.Add(new Teleporter()
            {
                Position = pos,
                Destination = dest,
                DestinationSceneName = destinationSceneName
            });
            return anchor;
        }
    }
}
