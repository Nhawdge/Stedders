using Stedders.Components;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Entities
{
    public static class ArchetypeGenerator
    {
        public static Entity GeneratePlayerMech(GameEngine engine)
        {
            var player = new Entity();
            player.Components.Add(new Player());

            var legs = new Sprite(engine.TextureManager.GetTexture(TextureKey.Mech2), "Assets/Mech2", 3, true) { MechPiece = MechPieces.Legs, CanRotate = false, ZIndex = 3 };
            player.Components.Add(legs);
            var torso = new Sprite(engine.TextureManager.GetTexture(TextureKey.Mech2Top), "Assets/Mech2Top", 3, true) { MechPiece = MechPieces.Torso, CanRotate = false, ZIndex = 2 };
            player.Components.Add(torso);
            var startPos = new Vector2(2118, 2760);

            legs.Rotation = 0f;
            legs.Position = startPos;
            torso.Position = startPos with { Y = startPos.Y - 30 /* * torso.Scale*/ };

            return player;
        }

        public static Entity GenerateEnemy(GameEngine engine, Vector2 position)
        {
            var enemy = new Entity();
            var sprite = new Sprite(engine.TextureManager.GetTexture(TextureKey.Enemy1), "Assets/Enemy1", 3, true)
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

        public static Entity GeneratePlant(GameEngine engine, Vector2 position)
        {
            var plant = new Entity();
            plant.Components.Add(new Sprite(engine.TextureManager.GetTexture(TextureKey.Plant1), "Assets/Plant1", 3, true)
            {
                Position = position
            });
            plant.Components.Add(new Plant("Wiggle Root"));

            return plant;
        }

        public static Entity GenerateBarn(GameEngine engine, Vector2 position)
        {
            var barn = new Entity();
            var sprite = new Sprite(engine.TextureManager.GetTexture(TextureKey.Barn), "Assets/Barn", 3, true) { Position = position };

            barn.Components.Add(sprite);
            var barnComponent = new Barn();

            barnComponent.Equipment.Add(EquipmentManager.GenerateLaser(engine));
            barnComponent.Equipment.Add(EquipmentManager.GenerateLaser(engine));
            barnComponent.Equipment.Add(EquipmentManager.GenerateHarvester(engine));
            barnComponent.Equipment.Add(EquipmentManager.GenerateSeeder(engine));

            barnComponent.Equipment.Add(new Equipment
            {
                Name = "Water - WIP",
                MaxAmmo = 100,
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false)
                { IsFlipped = true, OriginPos = Render.OriginAlignment.LeftBottom, },
                IconKey = TextureKey.WaterCannon,
            });
            barnComponent.Equipment.Add(new Equipment
            {
                Name = "Water - WIP",
                MaxAmmo = 100,
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false)
                { IsFlipped = true, OriginPos = Render.OriginAlignment.LeftBottom, },
                IconKey = TextureKey.WaterCannon,
            });
            barn.Components.Add(barnComponent);

            return barn;
        }

        public static Entity GenerateSilo(GameEngine engine, Vector2 position)
        {
            var silo = new Entity();
            silo.Components.Add(new Silo());
            silo.Components.Add(new Sprite(engine.TextureManager.GetTexture(TextureKey.Silo), "Assets/Silo", 3, true) { Position = position });
            return silo;
        }

        internal static Entity GenerateField(GameEngine engine, Vector2 position, bool startWithPlant = false)
        {
            var field = new Entity();
            var sprite = new Sprite(engine.TextureManager.GetTexture(TextureKey.Field1), "Assets/Field1", 3, true) { Position = position };
            var fieldComponent = new Field();
            field.Components.Add(sprite);
            field.Components.Add(fieldComponent);
            if (startWithPlant)
            {
                field.Components.Add(new Plant("Wiggle Root"));
                field.Components.Add(new Sprite(engine.TextureManager.GetTexture(TextureKey.Plant1), "Assets/Plant1", 3, true)
                {
                    Position = position
                });
            }
            return field;
        }
    }
}
