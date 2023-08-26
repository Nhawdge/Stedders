using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using Stedders.Utilities;
using System.Numerics;
using System.Runtime.ConstrainedExecution;

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

            if (state.State == States.Loading)
            {
                //Engine.Entities.Add(ArchetypeGenerator.GenerateBarn(Engine, new Vector2(1700, 1600)));
                //Engine.Entities.Add(ArchetypeGenerator.GenerateSilo(Engine, new Vector2(1925, 1300)));
                state.State = States.MainMenu;
            }
            if (state.State == States.Start)
            {
                //Engine.Entities.RemoveAll(x => true);
                //Engine.Entities.Add(Engine.Singleton);
                state.Currency = 0;
                state.Day = 0;
                state.CurrentTime = 0;


                Engine.Entities.Add(ArchetypeGenerator.GeneratePlayerMech(this.Engine));

                var startX = 981;
                var startY = 744;
                var xPadding = 48;
                var yPadding = 48;

                var y = startY;
                for (var j = 0; j < 5; j++)
                {
                    var x = startX;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GenerateField(this.Engine, new Vector2(x, y)));
                        x += xPadding;
                    }
                    y += yPadding;
                }

                y = 936;
                for (var j = 0; j < 5; j++)
                {
                    var x = 3300 + 145;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GenerateField(this.Engine, new Vector2(x, y), true));
                        x += xPadding;
                    }
                    y += yPadding;
                }
                //< 3003.7996, 2085.5286 >
                y = 2085;
                for (var j = 0; j < 5; j++)
                {
                    var x = 3003 + 145 + 145;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GenerateField(this.Engine, new Vector2(x, y)));
                        x += xPadding;
                    }
                    y += yPadding;
                }

                //Engine.Entities.Add(ArchetypeGenerator.GenerateEnemy(Engine, new Vector2(200, 200)));
                //Engine.Entities.Add(ArchetypeGenerator.GenerateBarn(Engine, new Vector2(1700, 1600)));
                //Engine.Entities.Add(ArchetypeGenerator.GenerateSilo(Engine, new Vector2(1925, 1300)));

                state.State = States.Dialogue;
                state.DialoguePhase = ("intro", 0);
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
                    var spawnSoundOptions = new List<SoundKey>() { SoundKey.Enemy1Spawn1, SoundKey.Enemy1Spawn2, SoundKey.Enemy1Spawn3 };
                    Engine.Singleton.Components.Add(new SoundAction(spawnSoundOptions[rand.Next(0, spawnSoundOptions.Count - 1)]));

                    Engine.Entities.Add(ArchetypeGenerator.GenerateEnemy(Engine, new Vector2(rand.Next(0, 4000), rand.Next(0, 3000))));
                }

            }
        }
    }
}