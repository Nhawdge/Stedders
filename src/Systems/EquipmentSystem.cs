using CsvHelper.Configuration.Attributes;
using Raylib_CsLo;
using Stedders.Components;

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
                var allEnemies = Engine.Entities.Where(x => x.HasTypes(typeof(NpcAi), typeof(Sprite), typeof(Health)));
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
                            //rotate dest by rotation

                            //var rotatedDest =   



                            Raylib.DrawRectangle((int)dest.x, (int)dest.y, (int)dest.width, (int)dest.height, Raylib.RED);
                            foreach (var enemy in allEnemies)
                            {

                                var enemyPos = enemy.GetComponent<Sprite>();
                                var enemyAi = enemy.GetComponent<NpcAi>();
                                var enemyHealth = enemy.GetComponent<Health>();
                                if (Raylib.CheckCollisionRecs(enemyPos.Destination, dest))
                                {
                                    enemyHealth.CurrentHealth -= item.Damage * Raylib.GetFrameTime();
                                    Console.WriteLine($"{enemyHealth.CurrentHealth}/{enemyHealth.MaxHealth}");
                                }
                            }
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
