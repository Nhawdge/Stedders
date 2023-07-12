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

            var legs = new Sprite(engine.TextureManager.TextureStore[TextureKey.Mech2], "Assets/Mech2", 1, true) { MechPiece = MechPieces.Legs, CanRotate = false };
            player.Components.Add(legs);
            var torso = new Sprite(engine.TextureManager.TextureStore[TextureKey.Mech2Top], "Assets/Mech2Top", 1, true) { MechPiece = MechPieces.Torso, CanRotate = false };
            player.Components.Add(torso);
            legs.Position = new Vector2(637, 396);

            //player.Components.Add(new Render(Raylib.LoadTexture("Assets/Mech1.png"), 0, 1, 2, true) { SpriteWidth = 192, SpriteHeight = 192, MechPiece = MechPieces.Torso });
            //player.Components.Add(new Sprite(engine.TextureManager.TextureStore[Utilities.TextureKey.Mech2], "Assets/Mech2.json", 10, true) { MechPiece = MechPieces.Torso });

            player.Components.Add(new Equipment { Name = "Water Gun", MaxAmmo = 100, Sprite = new Render(Raylib.LoadTexture("Assets/waterbeam.png"), 0, 0, 1, false) });
            return player;
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
