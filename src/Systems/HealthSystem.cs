using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Systems
{
    internal class HealthSystem : GameSystem
    {
        public HealthSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var healables = Engine.Entities.Where(x => x.HasTypes(typeof(Headquarters)) || x.HasTypes(typeof(MoveableUnit)));

                var entitiesToRemove = new List<Entity>();
                foreach (var heals in healables)
                {
                    var myUnit = heals.GetComponent<Unit>();
                    myUnit.Health += Raylib.GetFrameTime() * myUnit.RegenerationRate * 5;
                    myUnit.Health = Math.Min(myUnit.Health, myUnit.MaxHealth);
                    if (myUnit.Health <= 0)
                    {
                        Console.WriteLine($"{heals.ShortId()} should die");
                        entitiesToRemove.Add(heals);
                    }
                }
                foreach (var entity in entitiesToRemove)
                {
                    Engine.Entities.Remove(entity);
                }
            }
        }
    }
}
