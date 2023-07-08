using Raylib_CsLo;
using Stedders.Components;

namespace Stedders.Entities
{
    public static class ArchetypeGenerator
    {
        public static Entity BuildPlayerMech()
        {
            var player = new Entity();

            player.Components.Add(new Player());

            player.Components.Add(new Sprite(Raylib.LoadTexture("Assets/Mech1.png"), 0, 0, 2, true) { SpriteWidth = 192, SpriteHeight = 192, MechPiece = Sprite.MechPieces.Legs });
            player.Components.Add(new Sprite(Raylib.LoadTexture("Assets/Mech1.png"), 0, 1, 2, true) { SpriteWidth = 192, SpriteHeight = 192, MechPiece = Sprite.MechPieces.Torso });

            player.Components.Add(new Equipment { Name = "Water Gun", MaxAmmo = 100, Sprite = new Sprite(Raylib.LoadTexture("Assets/waterbeam.png"), 0, 0, 10, false) });
            return player;
        }
    }
}
