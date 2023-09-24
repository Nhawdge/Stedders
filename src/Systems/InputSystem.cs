using Raylib_CsLo;
using Stedders.Components;
using System.Numerics;

namespace Stedders.Systems
{
    public class InputSystem : GameSystem
    {

        public InputSystem(GameEngine gameEngine) : base(gameEngine)
        {

        }

        public override void Update()
        {
            var playerEntity = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
            if (playerEntity is not null)
            {
                var player = playerEntity.GetComponents<Player>().FirstOrDefault();
                player.Movement = Vector2.Zero;
            }

            foreach (var mapping in Engine.ActiveScene.KeyboardMapping)
            {
                if (Raylib.IsKeyDown(mapping.Key))
                {
                    mapping.Value();
                }
            }

            foreach (var mapping in Engine.ActiveScene.MouseMapping)
            {
                if (Raylib.IsMouseButtonDown(mapping.Key))
                {
                    mapping.Value();
                }
            }

            if (playerEntity is not null)
            {
                var playerRenderTorso = playerEntity.GetComponents<Render>().FirstOrDefault();
                if (playerRenderTorso is not null)
                {
                    var mousePos = Raylib.GetMousePosition();
                    var mouseWorldPos = Raylib.GetScreenToWorld2D(mousePos, Engine.Camera);
                    var difference = mouseWorldPos - playerRenderTorso.Position;
                    var angle = Math.Atan2(difference.Y, difference.X);
                    playerRenderTorso.Rotation = (float)(angle * 180 / Math.PI) + 90;
                }
            }
            //if (Raylib.GetMouseWheelMove() > 0)
            //{
            //    this.Engine.Camera.zoom += .01f;
            //}
            //else if (Raylib.GetMouseWheelMove() < 0)
            //{
            //    this.Engine.Camera.zoom -= .01f;
            //}
        }
    }
}
