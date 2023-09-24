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

                Engine.Entities.Add(ArchetypeGenerator.GenerateEnemy( new Vector2(rand.Next(0, 4000), rand.Next(0, 3000))));
            }
        }
    }
}