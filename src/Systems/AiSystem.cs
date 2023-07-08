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
                var enemyBases = Engine.Entities.Where(x => x.GetComponent<NpcAi>() is not null);

                foreach (var entityBase in enemyBases)
                {
                    var myHq = entityBase.GetComponent<Headquarters>();
                    var myAi = entityBase.GetComponent<NpcAi>();
                    var myRender = entityBase.GetComponent<Sprite>();
                    var myTeam = entityBase.GetComponent<Faction>();

                    //if (myHq.Wealth >= UnitsManager.Stats[Units.Bob].Cost * 2)
                    //{
                    //    entityBase.Components.Add(new SpawnCommand(Units.Bob));
                    //}
                }
            }
        }
    }
}
