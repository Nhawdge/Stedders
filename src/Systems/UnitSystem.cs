using Raylib_CsLo;
using Stedders.Components;

namespace Stedders.Systems
{
    internal class UnitSystem : GameSystem
    {
        public UnitSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var unitsToMove = Engine.Entities.Where(x => x.HasTypes(typeof(MoveableUnit)));
                foreach (var unit in unitsToMove)
                {

                    var myRender = unit.GetComponent<Sprite>();
                    var myMove = unit.GetComponent<MoveableUnit>();
                    var myTeam = unit.GetComponent<Faction>();


                    //myRender.Position.X += myMove.Stats.Speed * (myMove.ShouldMoveRight ? 1 : -1);
                    //myMove.Stats = myMove.Stats with { Speed = UnitsManager.Stats[myMove.UnitType].Speed };
                    //myMove.ActionTimer += Raylib.GetFrameTime();

                    //// enemies 
                    //var enemies = Engine.Entities.Where(x => x.GetComponent<Faction>().Team != myTeam.Team);
                    //var nearestTarget = enemies.OrderBy(x => Math.Abs(x.GetComponent<Render>().Position.X - myRender.Position.X)).FirstOrDefault();
                    //if (nearestTarget is not null)
                    //{
                    //    var targetRender = nearestTarget.GetComponent<Render>();
                    //    var distanceToTarget = Math.Abs(targetRender.Position.X - myRender.Position.X);
                    //    if (distanceToTarget <= myMove.Stats.Range)
                    //    {
                    //        myMove.Stats = myMove.Stats with { Speed = 0 };
                    //        // attack
                    //        if (myMove.ActionTimer > 3)
                    //        {
                    //            var targetHealth = nearestTarget.GetComponent<Unit>();
                    //            targetHealth.Health -= myMove.Stats.Damage;
                    //            myMove.ActionTimer = 0;
                    //            Console.WriteLine($"{nearestTarget.ShortId()} has {targetHealth.Health}/{targetHealth.MaxHealth}");
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}
