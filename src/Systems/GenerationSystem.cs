using Raylib_CsLo;
using Stedders;
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
                Engine.Entities.Add(ArchetypeGenerator.BuildPlayerMech(this.Engine));
                var y = 430;
                for (var j = 0; j < 5; j++)
                {
                    var x = 595;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GeneratePlant(this.Engine, new Vector2(x, y)));
                        x += 30;
                    }
                    y += 30;
                }

                y = 430;
                for (var j = 0; j < 5; j++)
                {
                    var x = 740;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GeneratePlant(this.Engine, new Vector2(x, y)));
                        x += 30;
                    }
                    y += 30;
                }

                y = 430;
                for (var j = 0; j < 5; j++)
                {
                    var x = 885;
                    for (var i = 0; i < 3; i++)
                    {
                        Engine.Entities.Add(ArchetypeGenerator.GeneratePlant(this.Engine, new Vector2(x, y)));
                        x += 30;
                    }
                    y += 30;
                }

                //Engine.Entities.Add(ArchetypeGenerator.GenerateBase(Engine.TextureManager, Data.Factions.Enemy1));

                state.State = States.Game;
            }

            else if (state.State == States.Game)
            {

            }
        }
    }
}