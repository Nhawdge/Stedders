using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using System.IO.IsolatedStorage;
using System.Numerics;
using System.Runtime.InteropServices;

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
                Ammo = 0,
                Damage = 25,
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false),
                IconKey = TextureKey.LaserCannon,
                SoundKey = SoundKey.Laser,
                Fire = (allEntities, entity, item) =>
                {
                    if (item.IsOverheated)
                    {
                        return (Enumerable.Empty<Entity>(), Enumerable.Empty<Entity>());
                    }
                    var allEnemies = allEntities.Where(x => x.HasTypes(typeof(NpcAi), typeof(Sprite), typeof(Health)));

                    var mySprite = entity.GetComponents<Render>().First(x => x.MechPiece == MechPieces.Torso);

                    var dest = mySprite.Destination;
                    dest.width += item.Range;
                    item.Range = Math.Min(item.MaxRange, item.Range + 1000 * Raylib.GetFrameTime());
                    item.Ammo += 20 * Raylib.GetFrameTime();
                    if (item.Ammo >= item.MaxAmmo)
                    {
                        item.Name = "Overheated";

                        item.IsOverheated = true;
                        item.Ammo = item.MaxAmmo;
                    }
                    var offset = item.Button == MouseButton.MOUSE_BUTTON_LEFT ? 20 : 30;
                    Raylib.DrawTexturePro(item.Sprite.Texture, item.Sprite.Source, dest,
                        new Vector2(item.Sprite.Origin.X, item.Sprite.Origin.Y + offset),
                        mySprite.Rotation - 90, Raylib.WHITE);

                    var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), engine.Camera);

                    var mouseDirectionAsRadians = (float)Math.Atan2(mousePos.Y - mySprite.Position.Y, mousePos.X - mySprite.Position.X);
                    var lineStart = mySprite.Position;
                    var lineEnd = new Vector2(
                        mySprite.Position.X + (float)Math.Cos(mouseDirectionAsRadians) * (item.Range + 50),
                        mySprite.Position.Y + (float)Math.Sin(mouseDirectionAsRadians) * (item.Range + 50));

                    engine.Singleton.Components.Add(new SoundAction(SoundKey.Laser));

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
                    return (Enumerable.Empty<Entity>(), Enumerable.Empty<Entity>());
                },
                Idle = (entity, item) =>
                {
                    if (!item.IsFiring)
                    {
                        engine.Singleton.Components.Add(new SoundAction(SoundKey.Laser, true));
                    }

                    item.Ammo -= Raylib.GetFrameTime() * (item.IsOverheated ? 10 : 15);
                    if (item.Ammo <= 0)
                    {
                        item.IsOverheated = false;
                        item.Ammo = 0;
                        item.Name = "Laser";
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
                Ammo = 0,
                Range = 75,
                MaxRange = 75,
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false),
                CanReload = false,
                IconKey = TextureKey.Harvester,
                Fire = (allEntities, player, item) =>
                {
                    var playerComponent = player.GetComponent<Player>();
                    var myPos = player.GetComponents<Sprite>().First(x => x.MechPiece == MechPieces.Torso);
                    var plants = allEntities.Where(x => x.HasTypes(typeof(Plant), typeof(Sprite)));
                    var nearestPlant = plants.OrderBy(x => (x.GetComponent<Sprite>().Position - myPos.Position).Length()).FirstOrDefault();
                    var mySprite = player.GetComponents<Render>().First(x => x.MechPiece == MechPieces.Torso);

                    if (nearestPlant is not null)
                    {
                        var plantSprite = nearestPlant.GetComponent<Sprite>();
                        var plant = nearestPlant.GetComponent<Plant>();
                        var lineStart = myPos.Position;
                        var lineEnd = plantSprite.Position;
                        //lineEnd.Y += 10;
                        var lineDistance = (lineEnd - lineStart).Length();
                        if (lineDistance < item.Range)
                        {
                            var offset = item.Button == MouseButton.MOUSE_BUTTON_LEFT ? 20 : 30;

                            var dest = mySprite.Destination;
                            dest.width = lineDistance - (plantSprite.Destination.width / 2);
                            var plantDirectionAsRadians = (float)Math.Atan2(plantSprite.Destination.Y - mySprite.Position.Y, plantSprite.Destination.x - mySprite.Position.X);
                            var plantDirectionAsDegrees = plantDirectionAsRadians * 180 / MathF.PI;
                            var lineEnd2 = new Vector2(
                                mySprite.Position.X + (float)Math.Cos(plantDirectionAsRadians) * item.Range,
                                mySprite.Position.Y + (float)Math.Sin(plantDirectionAsRadians) * item.Range);

                            Raylib.DrawTexturePro(
                                item.Sprite.Texture,
                                item.Sprite.Source,
                                dest,
                                new Vector2(item.Sprite.Origin.X, item.Sprite.Origin.Y + offset),
                                plantDirectionAsDegrees,
                                Raylib.GREEN);
                            engine.Singleton.Components.Add(new SoundAction(SoundKey.Harvester1));

                            //Raylib.DrawLine((int)lineStart.X, (int)lineStart.Y, (int)lineEnd.X, (int)lineEnd.Y, Raylib.GREEN);
                            if (Raylib.CheckCollisionPointLine(plantSprite.Position, lineStart, lineEnd, 30))
                            {
                                if (item.Ammo <= item.MaxAmmo)
                                {
                                    plant.PlantBody -= item.Damage * Raylib.GetFrameTime();
                                    item.Ammo += Math.Min(item.Damage * Raylib.GetFrameTime(), item.MaxAmmo);
                                    engine.Singleton.GetComponent<GameState>().Stats.BiomassHarvested += item.Damage * Raylib.GetFrameTime();
                                }
                                else
                                {
                                    item.Ammo = Math.Min(item.Ammo, item.MaxAmmo);
                                }
                            }
                        }
                    }
                    return (Enumerable.Empty<Entity>(), Enumerable.Empty<Entity>());
                },
                Idle = (player, item) =>
                {
                    if (!item.IsFiring)
                        engine.Singleton.Components.Add(new SoundAction(SoundKey.Harvester1, true));
                }
            };
        }

        internal static Equipment GenerateSeeder(GameEngine engine)
        {
            return new Equipment
            {
                Name = "Seeder",
                MaxAmmo = 10,
                Ammo = 10,
                Sprite = new Render(engine.TextureManager.GetTexture(TextureKey.Laser), 0, 0, 1, false),
                IconKey = TextureKey.SeedCannon,
                CooldownPerShot = 1,
                ShotCoolDownRate = 1,
                CanReload = true,
                Fire = (entities, player, item) =>
                {
                    if (item.ShotCooldown > 0)
                    {
                        return (Enumerable.Empty<Entity>(), Enumerable.Empty<Entity>());
                    }

                    if (item.Ammo > 0)
                    {
                        var target = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), engine.Camera);

                        var allFields = entities.Where(x => x.HasTypes(typeof(Field)));
                        allFields.ToList().ForEach(x =>
                        {
                            var dest = x.GetComponent<Sprite>().Destination;
                            Raylib.DrawRectangleLines(
                                (int)dest.x,
                                (int)dest.y,
                                (int)dest.width,
                                (int)dest.height, Raylib.RED);
                        });
                        var nearestField = allFields
                            .OrderBy(x => (x.GetComponent<Sprite>().Position - target).Length())
                            .FirstOrDefault();
                        if (nearestField != null)
                        {
                            var field = nearestField.GetComponents<Sprite>()
                                .FirstOrDefault(x => x.AnimationDataPath == "Assets/Field1");
                            var fieldComponent = nearestField.GetComponent<Field>();
                            if (field != null && fieldComponent.HasCrop == false)
                            {
                                if (Raylib.CheckCollisionPointRec(target, field.Destination))
                                {
                                    item.Ammo--;
                                    item.ShotCooldown = item.CooldownPerShot;
                                    nearestField.Components.Add(new Plant("Wiggle Root"));
                                    nearestField.Components.Add(new Sprite(engine.TextureManager.GetTexture(TextureKey.Plant1), "Assets/Plant1", 3, false)
                                    {
                                        Position = field.Position
                                    });
                                    fieldComponent.HasCrop = true;
                                    engine.Singleton.Components.Add(new SoundAction(SoundKey.Seeder1));
                                }
                            }
                        }
                        return (Enumerable.Empty<Entity>(), Enumerable.Empty<Entity>());
                    }
                    else
                    {
                        return (Enumerable.Empty<Entity>(), Enumerable.Empty<Entity>());
                    }
                },
                Idle = (player, item) =>
                {
                    if (!item.IsFiring)
                        engine.Singleton.Components.Add(new SoundAction(SoundKey.Seeder1, true));
                }

            };
        }

        internal static Equipment GenerateWaterCannon(GameEngine engine)
        {
            return new Equipment
            {
                Name = "Water",
                Ammo = 10,
                MaxAmmo = 10,
                CanReload = true,
                CooldownPerShot = 1.5f,
                CostPerShot = 1,
                Damage = 10,
                IconKey = TextureKey.WaterCannon,
                Fire = (entities, player, item) =>
                {
                    if (item.ShotCooldown > 0)
                    {
                        return (Enumerable.Empty<Entity>(), Enumerable.Empty<Entity>());
                    }
                    item.ShotCooldown = item.CooldownPerShot;
                    var torso = player.GetComponents<Sprite>().FirstOrDefault(x => x.MechPiece == MechPieces.Torso);
                    var entitiesToAdd = new List<Entity>();
                    if (torso is null)
                    {
                        return (entitiesToAdd, Enumerable.Empty<Entity>());
                    }

                    var waterBlob = new Entity();
                    var sprite = new Sprite(engine.TextureManager.GetTexture(TextureKey.WaterBlob), "Assets/WaterBlob", 2, true)
                    {
                        Position = torso.Position,
                    };
                    waterBlob.Components.Add(sprite);
                    var health = new Health()
                    {
                        CurrentHealth = 1,
                        MaxHealth = 1,
                    };
                    waterBlob.Components.Add(health);

                    var target = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), engine.Camera);
                    var motion = new Motion()
                    {
                        Target = target,
                        Speed = 400f,
                        OnTarget = () =>
                        {
                            sprite.Play("Splash");
                            health.RegenRate = -1;
                            var fieldsInRange = entities.Where(x => x.HasTypes(typeof(Field)))
                                .Where(x => (x.GetComponent<Sprite>().Position - sprite.Position).Length() < 100);
                            foreach (var field in fieldsInRange)
                            {
                                var fieldComponent = field.GetComponent<Field>();
                                fieldComponent.WaterLevel += item.Damage;
                            }
                        }
                    };
                    waterBlob.Components.Add(motion);

                    entitiesToAdd.Add(waterBlob);
                    return (entitiesToAdd, Enumerable.Empty<Entity>());
                },
            };
        }
    }
}
