using Raylib_CsLo;
using Stedders;
using Stedders.Components;
using Stedders.Entities;
using Stedders.Utilities;
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


                Engine.Entities.Add(ArchetypeGenerator.BuildEnemy(Engine, new Vector2(100, 100)));

                state.State = States.Dialogue;
            }

            else if (state.State == States.Game)
            {

            }
        }
    }
}