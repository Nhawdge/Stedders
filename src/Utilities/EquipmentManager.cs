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
                Fire = (allEntities, entity, item) =>
                {
                    var allEnemies = allEntities.Where(x => x.HasTypes(typeof(NpcAi), typeof(Sprite), typeof(Health)));

                    var mySprite = entity.GetComponents<Render>().First(x => x.MechPiece == MechPieces.Torso);

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

                    Raylib.DrawLine((int)lineStart.X, (int)lineStart.Y, (int)lineEnd.X, (int)lineEnd.Y, Raylib.GREEN);

                    foreach (var enemy in allEnemies)
                    {
                        var enemyPos = enemy.GetComponent<Sprite>();
                        var enemyAi = enemy.GetComponent<NpcAi>();
                        var enemyHealth = enemy.GetComponent<Health>();

                        var targetVector = enemyPos.Position;

                        enemyPos.Color = Raylib.WHITE;
                        if (Raylib.CheckCollisionPointLine(targetVector, lineStart, lineEnd, 30))
                        {
                            enemyHealth.CurrentHealth -= item.Damage * Raylib.GetFrameTime();
                            enemyPos.Color = Raylib.RED;
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
                Fire = (allEntities, player, item) =>
                {
                    var playerComponent = player.GetComponent<Player>();
                    var myPos = player.GetComponents<Sprite>().First(x => x.MechPiece == MechPieces.Torso);
                    var plants = allEntities.Where(x => x.HasTypes(typeof(Plant), typeof(Sprite)));
                    var nearestPlant = plants.OrderBy(x => (x.GetComponent<Sprite>().Position - myPos.Position).Length()).FirstOrDefault();

                    if (nearestPlant is not null)
                    {
                        var plantPos = nearestPlant.GetComponent<Sprite>();
                        var plant = nearestPlant.GetComponent<Plant>();
                        var lineStart = myPos.Position;
                        var lineEnd = plantPos.Position;
                        Raylib.DrawLine((int)lineStart.X, (int)lineStart.Y, (int)lineEnd.X, (int)lineEnd.Y, Raylib.GREEN);
                        if (Raylib.CheckCollisionPointLine(plantPos.Position, lineStart, lineEnd, 30))
                        {
                            if (playerComponent.BioMassContainer <= playerComponent.MaxBioMassContainer)
                            {
                                plant.PlantBody -= item.Damage * Raylib.GetFrameTime();
                                playerComponent.BioMassContainer += item.Damage * Raylib.GetFrameTime();
                            }
                        }
                    }

                }
            };
        }
    }
}
