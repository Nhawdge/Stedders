using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Systems
{
    internal class UnitCreationSystem : GameSystem
    {
        public UnitCreationSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var withSpawnCommand = Engine.Entities.Where(x => x.HasTypes(typeof(SpawnCommand), typeof(Headquarters)));
                var unitsToAdd = new List<Entity>();

                foreach (var entity in withSpawnCommand)
                {
                    //var mySpawn = entity.GetComponent<SpawnCommand>();
                    //var myHq = entity.GetComponent<Headquarters>();
                    //var myTeam = entity.GetComponent<Faction>();
                    //if (myHq.Wealth >= UnitsManager.Stats[mySpawn.UnitToSpawn].Cost)
                    //{
                    //    myHq.Wealth -= UnitsManager.Stats[mySpawn.UnitToSpawn].Cost;
                    //    var newUnit = ArchetypeGenerator.GenerateUnit(Engine.TextureManager, mySpawn.UnitToSpawn, myTeam.Team, entity);
                    //    unitsToAdd.Add(newUnit);
                    //}
                    //entity.Components.Remove(mySpawn);

                }
                Engine.Entities.AddRange(unitsToAdd);
            }
        }
    }
}
