using Raylib_CsLo;
using Stedders;
using Stedders.Components;
using Stedders.Entities;

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
                //Engine.Entities.Add(ArchetypeGenerator.GenerateBase(Engine.TextureManager, Data.Factions.Enemy1));

                state.State = States.Game;
            }

            else if (state.State == States.Game)
            {

            }
        }
    }
}