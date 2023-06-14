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

            player.Components.Add(new Render(Raylib.LoadTexture("Assets/Mech1.png"), 0, 0, 5, true) { SpriteWidth = 192, SpriteHeight = 192, MechPiece = Render.MechPieces.Legs });
            player.Components.Add(new Render(Raylib.LoadTexture("Assets/Mech1.png"), 0, 1, 5, true) { SpriteWidth = 192, SpriteHeight = 192, MechPiece = Render.MechPieces.Torso });

            return player;
        }
    }
}
