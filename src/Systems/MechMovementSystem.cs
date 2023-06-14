using Stedders.Components;
using System.Numerics;

namespace Stedders.Systems
{
    internal class MechMovementSystem : GameSystem
    {
        public MechMovementSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();

            if (state.State == States.Game)
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is not null)
                {
                    var legs = playerMech.GetComponents<Render>().FirstOrDefault(x => x.MechPiece == Render.MechPieces.Legs);
                    var torso = playerMech.GetComponents<Render>().FirstOrDefault(x => x.MechPiece == Render.MechPieces.Torso);

                    var newPosition = new Vector2(legs.Position.X, legs.Position.Y);
                    
                    var directionAsVector = new Vector2((float)Math.Cos(legs.RotationAsRadians), (float)Math.Sin(legs.RotationAsRadians));
                    newPosition += directionAsVector * playerMech.GetComponent<Player>().Throttle;
                    legs.Position = newPosition;
                    torso.Position = newPosition;
                }
            }

        }
    }
}
