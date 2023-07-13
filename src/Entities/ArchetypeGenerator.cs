using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Entities
{
    public static class ArchetypeGenerator
    {
        public static Entity BuildPlayerMech(GameEngine engine)
        {
            var player = new Entity();
            player.Components.Add(new Player());

            var legs = new Sprite(engine.TextureManager.GetTexture(TextureKey.Mech2), "Assets/Mech2", 1, true) { MechPiece = MechPieces.Legs, CanRotate = false };
            player.Components.Add(legs);
            var torso = new Sprite(engine.TextureManager.GetTexture(TextureKey.Mech2Top), "Assets/Mech2Top", 1, true) { MechPiece = MechPieces.Torso, CanRotate = false };
            player.Components.Add(torso);
            var startPos = new Vector2(2129, 135);

            legs.Rotation = 180f;
            legs.Position = startPos;
            torso.Position = startPos with { Y = startPos.Y - 30 * torso.Scale };

            player.Components.Add(new Equipment { 
                Name = "Laser 1", 
                MaxAmmo = 100, 
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false),
                Button = MouseButton.MOUSE_BUTTON_LEFT });
            player.Components.Add(new Equipment { 
                Name = "Laser 2", 
                MaxAmmo = 100, 
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false) 
                    { IsFlipped = true, OriginPos = Render.OriginAlignment.LeftBottom, },
                Button = MouseButton.MOUSE_BUTTON_RIGHT }); ;
            return player;
        }

        public static Entity BuildEnemy(GameEngine engine, Vector2 position)
        {
            var enemy = new Entity();
            var sprite = new Sprite(engine.TextureManager.GetTexture(TextureKey.Enemy1), "Assets/Enemy1", 1, true)
            {
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
            plant.Components.Add(new Sprite(engine.TextureManager.GetTexture(TextureKey.Plant1), "Assets/Plant1", 1, true)
            {
                Position = position
            });
            plant.Components.Add(new Plant("Wiggle Root"));

            return plant;
        }

    }
}
