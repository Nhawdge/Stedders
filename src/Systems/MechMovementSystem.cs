using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
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


            var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
            if (playerMech is not null)
            {
                var legs = playerMech.GetComponents<Sprite>().First(x => x.MechPiece == MechPieces.Legs);
                var torso = playerMech.GetComponents<Sprite>().FirstOrDefault(x => x.MechPiece == MechPieces.Torso);

                legs.Rotation = (legs.Rotation + 360) % 360;
                var newPosition = new Vector2(legs.Position.X, legs.Position.Y);

                var directionTexture = TextureManager.Instance.GetTexture(Utilities.TextureKey.MechDirection);

                Raylib.DrawTexturePro(directionTexture,
                    new Rectangle(0, 0, directionTexture.width, directionTexture.height),
                    new Rectangle(newPosition.X, newPosition.Y, 32, 32),
                    new Vector2(16, 100), legs.Rotation, Raylib.WHITE);

                var throttle = playerMech.GetComponent<Player>().Throttle;
                if (torso is not null)
                {

                    switch (torso.Rotation)
                    {
                        case > 45 and < 135:
                            torso.Play("IdleR");
                            torso.IsFlipped = false;
                            break;
                        case > 135 and < 225:
                            torso.Play("Idle");
                            break;
                        case > 225 and < 315:
                            torso.Play("IdleR");
                            torso.IsFlipped = true;
                            break;
                        case < -45:
                        default:
                            torso.Play("IdleU");
                            break;
                    }
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
                    playerMech.Components.Add(new SoundAction(SoundKey.Mech2Walking));
                    playerMech.Components.Add(new SoundAction(SoundKey.Mech2EngineIdle, false));
                }
                else
                {
                    playerMech.Components.Add(new SoundAction(SoundKey.Mech2Walking, true));
                    playerMech.Components.Add(new SoundAction(SoundKey.Mech2EngineIdle));
                    legs.Play("Idle");
                }

                var directionAsVector = new Vector2((float)Math.Cos(legs.RotationAsRadians), (float)Math.Sin(legs.RotationAsRadians));
                newPosition += directionAsVector * throttle;
                legs.Position = newPosition;
                if (torso is not null)
                {
                    torso.Position = newPosition with { Y = newPosition.Y - 30 /** torso.Scale*/ };
                }
            }
        }
    }
}