namespace Stedders.Components
{
    internal class MoveableUnit : Unit
    {
        public bool ShouldMoveRight;
        //public Units UnitType;
        //public UnitStats Stats;
        public float ActionTimer;
        public bool IsAttacking = false;

        //public MoveableUnit(Units unitType, Factions team)
        //{
        //    UnitType = unitType;
        //    Stats = UnitsManager.Stats[unitType];
        //    if (team == Factions.Player)
        //    {
        //        ShouldMoveRight = true;
        //    }
        //}
    }
}
