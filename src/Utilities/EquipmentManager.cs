using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using System.Numerics;

namespace Stedders.Utilities
{
    internal static class EquipmentManager
    {
        public static Equipment GenerateLaser(GameEngine engine)
        {
            return new Equipment
            {
                Name = "Laser",
                MaxAmmo = 100,
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false),
                IconKey = TextureKey.LaserCannon,
                Fire = (allEnemies, entity, item) =>
                {
                    var mySprite = entity.GetComponents<Render>().First(x => x.MechPiece == MechPieces.Torso);

                    //Raylib.DrawTexture(equipment.Sprite.Texture, (int)mySprite.Position.X, (int)mySprite.Position.Y, Raylib.WHITE);
                    var dest = mySprite.Destination;
                    dest.width += item.Range;
                    item.Range = Math.Min(item.MaxRange, item.Range + 1000 * Raylib.GetFrameTime());
                    item.Ammo -= 1 * Raylib.GetFrameTime();
                    Raylib.DrawTexturePro(item.Sprite.Texture, item.Sprite.Source, dest, item.Sprite.Origin, mySprite.Rotation - 90, Raylib.WHITE);
                    var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), engine.Camera);

                    var mouseDirectionAsRadians = (float)Math.Atan2(mousePos.Y - mySprite.Position.Y, mousePos.X - mySprite.Position.X);
                    var lineStart = mySprite.Position;
                    var lineEnd = new Vector2( 
                        mySprite.Position.X + (float)Math.Cos(mouseDirectionAsRadians) * (item.Range + 50),
                        mySprite.Position.Y + (float)Math.Sin(mouseDirectionAsRadians) * (item.Range + 50));

                    var lineSegmentVector = lineEnd - lineStart;
                     
                    ///Raylib.DrawRectangle((int)dest.x, (int)dest.y, (int)dest.width, (int)dest.height, Raylib.RED);

                    Raylib.DrawLine((int)lineStart.X, (int)lineStart.Y, (int)lineEnd.X, (int)lineEnd.Y, Raylib.GREEN);

                    foreach (var enemy in allEnemies)
                    {
                        var enemyPos = enemy.GetComponent<Sprite>();
                        var enemyAi = enemy.GetComponent<NpcAi>();
                        var enemyHealth = enemy.GetComponent<Health>();

                        var targetVector = enemyPos.Position;

                        float dotProduct = Vector2.Dot(targetVector, lineSegmentVector) / lineSegmentVector.LengthSquared();

                        var nearestPoint = targetVector + dotProduct * lineSegmentVector;

                        //Raylib.DrawCircle((int)targetVector.X, (int)targetVector.Y, 10f, Raylib.BLACK);
                        //Raylib.DrawCircle((int)nearestPoint.X, (int)nearestPoint.Y, 10f, Raylib.BLUE);
                        enemyPos.Color = Raylib.WHITE;
                        if (Raylib.CheckCollisionPointLine(targetVector, lineStart, lineEnd, 30))
                        {
                            enemyHealth.CurrentHealth -= item.Damage * Raylib.GetFrameTime();
                            enemyPos.Color = Raylib.RED;

                            Console.WriteLine($"{enemyHealth.CurrentHealth}/{enemyHealth.MaxHealth}");
                        }
                    }
                }
            };
        }
        public static Equipment GenerateHarvester(GameEngine engine)
        {
            return new Equipment
            {
                Name = "Harvester",
                MaxAmmo = 100,
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false)
                { IsFlipped = true, OriginPos = Render.OriginAlignment.LeftBottom, },
                IconKey = TextureKey.Harvester,
                Fire = (allEnemies, player, item) =>
                {
                    Console.WriteLine("Harvesting");
                }
            };
        }
    }
}
