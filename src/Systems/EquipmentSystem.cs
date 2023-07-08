using Raylib_CsLo;
using Stedders.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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
                    var equipment = entity.GetComponent<Equipment>();
                    if (equipment.Ammo < equipment.MaxAmmo)
                    {
                        //equipment.Ammo += equipment.RechargeRate;
                    }
                    if (equipment.IsFiring)
                    {
                        equipment.IsFiring = false;
                        var mySprite = entity.GetComponents<Sprite>().First(x => x.MechPiece == MechPieces.Torso);

                        //Raylib.DrawTexture(equipment.Sprite.Texture, (int)mySprite.Position.X, (int)mySprite.Position.Y, Raylib.WHITE);
                        var dest = mySprite.Destination;
                        dest.width += equipment.Range;
                        equipment.Range = Math.Min(equipment.MaxRange, (equipment.Range + 1000 * Raylib.GetFrameTime()));
                        equipment.Ammo -= 1 * Raylib.GetFrameTime();
                        Raylib.DrawTexturePro(equipment.Sprite.Texture, equipment.Sprite.Source, dest, equipment.Sprite.Origin, mySprite.Rotation - 90, Raylib.WHITE);

                    }
                    else
                    {
                        equipment.Range = Math.Max(0, equipment.Range - (500 * Raylib.GetFrameTime()));
                    }
                }
            }
        }
    }
}
