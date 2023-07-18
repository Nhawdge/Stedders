using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Systems
{
    internal class AiSystem : GameSystem
    {
        public AiSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var enemies = Engine.Entities.Where(x => x.HasTypes(typeof(NpcAi)));
                var plants = Engine.Entities.Where(x => x.HasTypes(typeof(Plant)));
                var entitiesToRemove = new List<Entity>();

                foreach (var enemy in enemies)
                {
                    var myPos = enemy.GetComponent<Sprite>();
                    myPos.Color = Raylib.WHITE;
                    var myAi = enemy.GetComponent<NpcAi>();
                    var nearestPlant = plants.OrderBy(x => (x.GetComponent<Sprite>().Position - myPos.Position).Length()).FirstOrDefault();
                    var plantDistance = float.MaxValue;
                    if (nearestPlant is not null)
                    {
                        var nearestPlantPos = nearestPlant.GetComponent<Render>();
                        var plantPosDiff = nearestPlantPos.Position - myPos.Position;
                        plantDistance = plantPosDiff.Length();
                    }

                    var myHealth = enemy.GetComponent<Health>();
                    myAi.TimeSinceLastActionChanged += Raylib.GetFrameTime();
                    var posDifference = (myAi.TargetPosition - myPos.Position).Length();
                    if (posDifference < 10 && myAi.Action == EnemyAction.Move)
                    {
                        Console.WriteLine("Early change");
                        myAi.TimeSinceLastActionChanged += 10;
                    }
                    if (myAi.TimeSinceLastActionChanged > myAi.ThinkingTime)
                    {
                        var options = new Dictionary<EnemyAction, int>
                        {
                            { EnemyAction.Idle, 0 },
                            { EnemyAction.Move, 0 },
                            { EnemyAction.Attack, 0 },
                            { EnemyAction.Eat, 0 },
                            { EnemyAction.Flee, 0 }
                        };

                        myAi.TimeSinceLastActionChanged = 0;
                        var healthPercent = myHealth.CurrentHealth / myHealth.MaxHealth * 100;

                        #region Ai numbers, so much...

                        // Health
                        if (healthPercent > 80)
                        {
                            options[EnemyAction.Idle] += 2;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 5;
                            options[EnemyAction.Eat] += 10;
                            options[EnemyAction.Flee] += -10;
                        }
                        else if (healthPercent is < 80 and > 30)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 10;
                            options[EnemyAction.Eat] += 2;
                            options[EnemyAction.Flee] += 3;
                        }
                        else if (healthPercent is < 30)
                        {
                            options[EnemyAction.Idle] += -5;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 3;
                            options[EnemyAction.Eat] += 2;
                            options[EnemyAction.Flee] += 20;
                        }

                        // Plant
                        if (nearestPlant is null)
                        {
                            options[EnemyAction.Idle] += 2;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 50;
                            options[EnemyAction.Eat] += 10;
                            options[EnemyAction.Flee] += 0;
                        }

                        if (plantDistance > 1000)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 0;
                            options[EnemyAction.Flee] += 0;
                        }
                        else if (plantDistance is < 1000 and > 100)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 0;
                            options[EnemyAction.Flee] += 0;
                        }
                        else if (plantDistance is < 100 and > 20)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 0;
                            options[EnemyAction.Flee] += 0;
                        }
                        else if (plantDistance is < 20)
                        {
                            Console.WriteLine($"Plant in {plantDistance}");
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 20;
                            options[EnemyAction.Flee] += 0;
                        }

                        // BELLY

                        var bellyPercent = (myAi.Belly / myAi.BellyMax) * 100;
                        if (bellyPercent < 30)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 20;
                            options[EnemyAction.Flee] += 0;
                        }
                        else if (bellyPercent < 60)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 15;
                            options[EnemyAction.Flee] += 0;
                        }
                        else if (bellyPercent is > 60 and < 150)
                        {
                            options[EnemyAction.Idle] += 10;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 10;
                            options[EnemyAction.Flee] += 10;
                        }
                        else if (bellyPercent > 150)
                        {
                            options[EnemyAction.Idle] += 10;
                            options[EnemyAction.Move] += 0;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += -10;
                            options[EnemyAction.Flee] += 30;
                        }

                        #endregion

                        myAi.Action = options.OrderByDescending(x => x.Value).First().Key;
                        Console.WriteLine($"Changing mind to {myAi.Action} based on\nHP:{healthPercent}%\nBelly:{bellyPercent}%");
                        options.OrderByDescending(x => x.Value)
                            .Select(x => $"{x.Key}\t{x.Value}")
                            .ToList().ForEach(x => Console.WriteLine(x));
                    }

                    if (myAi.Action == EnemyAction.Idle)
                    {
                        myPos.Play("IdleD");
                        continue;
                    }
                    else if (myAi.Action == EnemyAction.Move)
                    {
                        if (myAi.TargetPosition == Vector2.Zero)
                        {
                            continue;
                        }

                        var posDiff = myAi.TargetPosition - myPos.Position;

                        var angle = Math.Atan2(posDiff.Y, posDiff.X);

                        myPos.Position.X += (float)Math.Cos(angle) * myAi.Speed * Raylib.GetFrameTime();
                        myPos.Position.Y += (float)Math.Sin(angle) * myAi.Speed * Raylib.GetFrameTime();

                        enemy.Components.Add(new SoundAction(SoundKey.Enemy1Move));

                        var angleInDegrees = angle * (180 / Math.PI) + 90;
                        switch (angleInDegrees)
                        {
                            case > 45 and < 135:
                                myPos.Play("IdleR");
                                myPos.IsFlipped = false;
                                break;
                            case > 135 and < 225:
                                myPos.Play("IdleD");
                                break;
                            case > 225 and < 315:
                                myPos.Play("IdleR");
                                myPos.IsFlipped = true;
                                break;
                            case < -45:
                            default:
                                myPos.Play("IdleU");
                                break;
                        }
                    }
                    else if (myAi.Action == EnemyAction.Eat)
                    {
                        if (nearestPlant == null)
                        {
                            continue;
                        }
                        var nearestPlantPos = nearestPlant.GetComponent<Render>();
                        var plantPosDiff = nearestPlantPos.Position - myPos.Position;
                        if (plantPosDiff.Length() > 10)
                        {
                            myAi.TargetPosition = nearestPlant.GetComponent<Render>().Position;
                            myAi.Action = EnemyAction.Move;
                            continue;
                        }

                        myPos.Play("Eating");
                        var plant = nearestPlant.GetComponent<Plant>();
                        plant.PlantBody -= myAi.EatingSpeed * Raylib.GetFrameTime();
                        myAi.Belly += myAi.EatingSpeed * Raylib.GetFrameTime();
                        var sounds = new List<SoundKey>() { SoundKey.Enemy1Eating1, SoundKey.Enemy1Eating2, SoundKey.Enemy1Eating3 };
                        enemy.Components.Add(new SoundAction(sounds[new Random().Next(0, sounds.Count - 1)]));

                        Engine.Singleton.GetComponent<GameState>().Stats.BiomassEaten += myAi.EatingSpeed * Raylib.GetFrameTime();
                    }
                    else if (myAi.Action == EnemyAction.Flee)
                    {
                        var pos = myPos.Position;
                        var leftEdgeDistance = pos.X;
                        var rightEdgeDistance = Engine.MapEdge.X - pos.X;
                        var topEdgeDistance = pos.Y;
                        var bottomEdgeDistance = Engine.MapEdge.Y - pos.Y;
                        var target = new Vector2(
                            rightEdgeDistance > leftEdgeDistance ? Engine.MapEdge.X : 0,
                            topEdgeDistance > bottomEdgeDistance ? Engine.MapEdge.Y : 0);

                        myAi.TargetPosition = target;
                        var targetDiff = target - pos;

                        if (targetDiff.Length() < 10)
                        {
                            entitiesToRemove.Add(enemy);
                        }

                        var angle = Math.Atan2(targetDiff.Y, targetDiff.X);
                        var angleInDegrees = angle * (180 / Math.PI) + 90;

                        myPos.Position.X += (float)Math.Cos(angle) * myAi.Speed * Raylib.GetFrameTime();
                        myPos.Position.Y += (float)Math.Sin(angle) * myAi.Speed * Raylib.GetFrameTime();

                        switch (angleInDegrees)
                        {
                            case > 45 and < 135:
                                myPos.Play("IdleR");
                                myPos.IsFlipped = false;
                                break;
                            case > 135 and < 225:
                                myPos.Play("IdleD");
                                break;
                            case > 225 and < 315:
                                myPos.Play("IdleR");
                                myPos.IsFlipped = true;
                                break;
                            case < -45:
                            default:
                                myPos.Play("IdleD");
                                break;
                        }

                        //Console.WriteLine($"\n\n\nLE: {leftEdgeDistance}, RE: {rightEdgeDistance},\nTE: {topEdgeDistance}, BE: {bottomEdgeDistance}");
                    }
                    else if (myAi.Action == EnemyAction.Attack)
                    {
                        var nearestBuilding = Engine.Entities
                            .Where(x => x.HasTypes(typeof(Building)))
                            .OrderBy(x => (x.GetComponent<Render>().Position - myPos.Position).Length())
                            .FirstOrDefault();
                        if (nearestBuilding == null)
                        {
                            continue;
                        }
                        var distance = (nearestBuilding.GetComponent<Render>().Position - myPos.Position).Length();
                        if (distance < myAi.AttackRange)
                        {
                            myPos.Play("Eating");
                            var building = nearestBuilding.GetComponent<Building>();
                            building.Health -= myAi.AttackDamage * Raylib.GetFrameTime();
                        }
                        else
                        {
                            myAi.Action = EnemyAction.Move;
                            myAi.TargetPosition = nearestBuilding.GetComponent<Render>().Position;
                        }
                    }
                }
                entitiesToRemove.ForEach(x => Engine.Entities.Remove(x));
            }
        }
    }
}
