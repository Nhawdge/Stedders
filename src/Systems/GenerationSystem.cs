using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using System.Numerics;

namespace Stedders.Systems
{
    public class GenerationSystem : GameSystem
    {
        public GenerationSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();

            if (state.State == States.Start)
            {
                Engine.Entities.RemoveAll(x => true);
                state.Currency = 0;
                state.Day = 0;
                state.CurrentTime = 0;


                Engine.Entities.Add(ArchetypeGenerator.GeneratePlayerMech(this.Engine));

                var startX = 1940;
                var startY = 1490;

                var y = startY;
                for (var j = 0; j < 5; j++)
                {
                    var x = startX;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GeneratePlant(this.Engine, new Vector2(x, y)));
                        x += 30;
                    }
                    y += 30;
                }

                y = startY;
                for (var j = 0; j < 5; j++)
                {
                    var x = startX + 145;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GeneratePlant(this.Engine, new Vector2(x, y)));
                        x += 30;
                    }
                    y += 30;
                }

                y = startY;
                for (var j = 0; j < 5; j++)
                {
                    var x = startX + 145 + 145;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GeneratePlant(this.Engine, new Vector2(x, y)));
                        x += 30;
                    }
                    y += 30;
                }

                Engine.Entities.Add(ArchetypeGenerator.GenerateEnemy(Engine, new Vector2(200, 200)));
                Engine.Entities.Add(ArchetypeGenerator.GenerateBarn(Engine, new Vector2(1700, 1600)));
                Engine.Entities.Add(ArchetypeGenerator.GenerateSilo(Engine, new Vector2(1925, 1300)));

                state.State = States.Dialogue;
                state.DialoguePhase = ("intro", 1);
                state.NextState = States.Game;
            }

            else if (state.State == States.Game)
            {
                var rand = new Random();
                var spawnRateModifier = state.TimeOfDay switch
                {
                    TimeOfDay.Dawn => .25f,
                    TimeOfDay.Day => 0.1f,
                    TimeOfDay.Dusk => .5f,
                    TimeOfDay.Night => 1f,
                    _ => throw new NotImplementedException()
                };

                state.TimeSinceLastSpawn += Raylib.GetFrameTime() * spawnRateModifier;
                if (state.TimeSinceLastSpawn > state.SpawnInterval)
                {
                    state.TimeSinceLastSpawn = 0f;
                    Console.WriteLine("Spawning");
                    Engine.Entities.Add(ArchetypeGenerator.GenerateEnemy(Engine, new Vector2(rand.Next(0, 4000), rand.Next(0, 3000))));
                }

            }
        }
    }
}