using Raylib_CsLo;
using Stedders.Components;
using static Stedders.Components.Sprite;

namespace Stedders.Systems
{
    internal class EquipmentSystem : GameSystem
    {
        public EquipmentSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(Equipment)));

                foreach (var entity in allEntities)
                {
                    var equipment = entity.GetComponents<Equipment>();
                    foreach (var item in equipment)
                    {
                        if (item.Ammo < item.MaxAmmo)
                        {
                        }
                        if (item.IsFiring)
                        {
                            item.IsFiring = false;
                            var mySprite = entity.GetComponents<Render>().First(x => x.MechPiece == MechPieces.Torso);

                            //Raylib.DrawTexture(equipment.Sprite.Texture, (int)mySprite.Position.X, (int)mySprite.Position.Y, Raylib.WHITE);
                            var dest = mySprite.Destination;
                            dest.width += item.Range;
                            item.Range = Math.Min(item.MaxRange, item.Range + 1000 * Raylib.GetFrameTime());
                            item.Ammo -= 1 * Raylib.GetFrameTime();
                            Raylib.DrawTexturePro(item.Sprite.Texture, item.Sprite.Source, dest, item.Sprite.Origin, mySprite.Rotation - 90, Raylib.WHITE);
                        }
                        else
                        {
                            item.Range = Math.Max(0, item.Range - (500 * Raylib.GetFrameTime()));
                        }
                    }
                }
            }
        }
    }
}
