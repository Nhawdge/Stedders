using Raylib_CsLo;
using Stedders.Components;
using System.Numerics;

namespace Stedders.Systems
{
    internal class PlayerMovementSystem : GameSystem
    {
        public PlayerMovementSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();

            var playerEntity = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
            if (playerEntity is not null)
            {
                var player = playerEntity.GetComponent<Player>();
                var playerSprite = playerEntity.GetComponent<Sprite>();

                playerSprite.Rotation = (playerSprite.Rotation + 360) % 360;
                var newPosition = new Vector2(playerSprite.Position.X, playerSprite.Position.Y);

                // Change to track tasks
                //var directionTexture = TextureManager.Instance.GetTexture(Utilities.TextureKey.MechDirection);
                //Raylib.DrawTexturePro(directionTexture,
                //    new Rectangle(0, 0, directionTexture.width, directionTexture.height),
                //    new Rectangle(newPosition.X, newPosition.Y, 32, 32),
                //    new Vector2(16, 100), playerSprite.Rotation, Raylib.WHITE);

                playerSprite.Play("Stedder1");

                //var directionAsVector = new Vector2((float)Math.Cos(playerSprite.RotationAsRadians), (float)Math.Sin(playerSprite.RotationAsRadians));
                if (player.Movement.Length() > 0)
                {
                    newPosition += Vector2.Normalize(player.Movement) * player.Speed * Raylib.GetFrameTime();
                }

                if (newPosition.X > Engine.ActiveScene.MapEdge.X)
                {
                    newPosition.X = Engine.ActiveScene.MapEdge.X;
                }
                else if (newPosition.X < 0)
                {
                    newPosition.X = 0;
                }
                if (newPosition.Y > Engine.ActiveScene.MapEdge.Y)
                {
                    newPosition.Y = Engine.ActiveScene.MapEdge.Y;
                }
                else if (newPosition.Y < 0)
                {
                    newPosition.Y = 0;
                }
                playerSprite.Position = newPosition;

            }
        }
    }
}