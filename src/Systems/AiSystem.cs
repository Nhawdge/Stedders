using Raylib_CsLo;
using Stedders.Components;

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

                foreach (var enemy in enemies)
                {
                    var myPos = enemy.GetComponent<Sprite>();
                    var myAi = enemy.GetComponent<NpcAi>();
                    var nearestPlant = plants.OrderBy(x => (x.GetComponent<Sprite>().Position - myPos.Position).Length()).FirstOrDefault();
                    var nearestPlantPos = nearestPlant.GetComponent<Render>();
                    var plantPosDiff = nearestPlantPos.Position - myPos.Position;
                    var plantDistance = plantPosDiff.Length();

                    var myHealth = enemy.GetComponent<Health>();
                    myAi.TimeSinceLastActionChanged += Raylib.GetFrameTime();
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
                        var healthPercent = myHealth.CurrentHealth / myHealth.MaxHealth;

                        #region Ai numbers, so much...

                        if (healthPercent > 80)
                        {
                            options[EnemyAction.Idle] += 2;
                            options[EnemyAction.Move] += 10;
                            options[EnemyAction.Attack] += 5;
                            options[EnemyAction.Eat] += 10;
                            options[EnemyAction.Flee] += -10;
                        }
                        else if (healthPercent is < 80 and > 30)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 10;
                            options[EnemyAction.Attack] += 10;
                            options[EnemyAction.Eat] += 2;
                            options[EnemyAction.Flee] += 3;
                        }
                        else if (healthPercent is < 30)
                        {
                            options[EnemyAction.Idle] += -5;
                            options[EnemyAction.Move] += 15;
                            options[EnemyAction.Attack] += 3;
                            options[EnemyAction.Eat] += 2;
                            options[EnemyAction.Flee] += 20;
                        }

                        if (plantDistance > 1000)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 10;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 0;
                            options[EnemyAction.Flee] += 0;
                        }
                        else if (plantDistance is < 1000 and > 100)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 10;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 0;
                            options[EnemyAction.Flee] += 0;
                        }
                        else if (plantDistance is < 100)
                        {
                            options[EnemyAction.Idle] += 0;
                            options[EnemyAction.Move] += 10;
                            options[EnemyAction.Attack] += 0;
                            options[EnemyAction.Eat] += 10;
                            options[EnemyAction.Flee] += 0;
                        }
                        #endregion

                        myAi.Action = options.OrderByDescending(x => x.Value).First().Key;
                        Console.WriteLine($"Changing mind to {myAi.Action}");

                    }


                    if (myAi.Action == EnemyAction.Idle)
                    {
                        continue;
                    }
                    else if (myAi.Action == EnemyAction.Move)
                    {

                        if (nearestPlant == null)
                        {
                            continue;
                        }

                        var angle = Math.Atan2(plantPosDiff.Y, plantPosDiff.X);

                        myPos.Position.X = myPos.Position.X + (float)Math.Cos(angle) * myAi.Speed * Raylib.GetFrameTime();
                        myPos.Position.Y = myPos.Position.Y + (float)Math.Sin(angle) * myAi.Speed * Raylib.GetFrameTime();

                        switch (angle)
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
                    }
                    else if (myAi.Action == EnemyAction.Eat)
                    {

                    }
                }
            }
        }
    }
}
