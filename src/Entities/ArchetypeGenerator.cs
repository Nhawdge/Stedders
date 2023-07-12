using Raylib_CsLo;
using Stedders.Components;

namespace Stedders.Entities
{
    public static class ArchetypeGenerator
    {
        public static Entity BuildPlayerMech(GameEngine engine)
        {
            var player = new Entity();

            player.Components.Add(new Player());

            var legs = new Sprite(engine.TextureManager.TextureStore[Utilities.TextureKey.Mech2], "Assets/Mech2", 10, true) { MechPiece = MechPieces.Legs, CanRotate = false };
            player.Components.Add(legs);
            player.Components.Add(new Render(Raylib.LoadTexture("Assets/Mech1.png"), 0, 1, 2, true) { SpriteWidth = 192, SpriteHeight = 192, MechPiece = MechPieces.Torso });
            legs.Play("Idle");
            //player.Components.Add(new Sprite(engine.TextureManager.TextureStore[Utilities.TextureKey.Mech2], "Assets/Mech2.json", 10, true) { MechPiece = MechPieces.Torso });

            player.Components.Add(new Equipment { Name = "Water Gun", MaxAmmo = 100, Sprite = new Render(Raylib.LoadTexture("Assets/waterbeam.png"), 0, 0, 10, false) });
            return player;
        }
    }
}
