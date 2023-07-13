using Raylib_CsLo;
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
                    var legs = playerMech.GetComponents<Sprite>().First(x => x.MechPiece == MechPieces.Legs);
                    var torso = playerMech.GetComponents<Sprite>().First(x => x.MechPiece == MechPieces.Torso);

                    legs.Rotation = (legs.Rotation + 360) % 360;
                    var newPosition = new Vector2(legs.Position.X, legs.Position.Y);
                    Raylib_CsLo.Raylib.DrawLine(
                        (int)newPosition.X,
                        (int)newPosition.Y,
                        (int)(newPosition.X + Math.Cos(legs.RotationAsRadians) * 100),
                        (int)(newPosition.Y + Math.Sin(legs.RotationAsRadians) * 100),
                        Raylib.RED);
                    var throttle = playerMech.GetComponent<Player>().Throttle;

                    switch (torso.Rotation)
                    {
                        case > 45 and < 135:
                            torso.Play("IdleR");
                            torso.IsFlipped = false;
                            break;
                        case > 135 and < 225:
                            torso.Play("IdleD");
                            break;
                        case > 225 and < 315:
                            torso.Play("IdleR");
                            torso.IsFlipped = true;
                            break;
                        default:
                            torso.Play("IdleD");
                            break;
                    }
                    if (throttle != 0)
                    {
                        switch (legs.Rotation)
                        {
                            case > 45 and < 135:
                                legs.Play("WalkR");
                                legs.IsFlipped = false;
                                break;
                            case > 135 and < 225:
                                legs.Play("WalkD");
                                break;
                            case > 225 and < 315:
                                legs.Play("WalkR");
                                legs.IsFlipped = true;
                                break;
                            default:
                                legs.Play("WalkD");
                                break;
                        }
                    }
                    else
                    {
                        legs.Play("Idle");
                    }

                    var directionAsVector = new Vector2((float)Math.Cos(legs.RotationAsRadians), (float)Math.Sin(legs.RotationAsRadians));
                    newPosition += directionAsVector * throttle;
                    legs.Position = newPosition;
                    torso.Position = newPosition with { Y = newPosition.Y - 30 * torso.Scale };
                }
            }

        }
    }
}
