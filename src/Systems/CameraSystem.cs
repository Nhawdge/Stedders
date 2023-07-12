using Raylib_CsLo;
using Stedders.Components;
using System.Numerics;
using static Raylib_CsLo.Raylib;
using static Stedders.Components.Sprite;

namespace Stedders.Systems
{
    internal class CameraSystem : GameSystem
    {
        public CameraSystem(GameEngine gameEngine) : base(gameEngine)
        {
            Engine.Camera.offset = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2);
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();

            if (state.State == States.Game)
            {
                float LeftEdge = 0 + GetScreenWidth() / 2;
                float RightEdge = 14096 - GetScreenWidth() / 2;
                float TopEdge = 0 + GetScreenHeight() / 2;
                float BottomEdge = 14096 - GetScreenHeight() / 2;
                var mousePos = GetScreenToWorld2D(GetMousePosition(), Engine.Camera);

                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                var playerRenderTorso = playerMech.GetComponents<Render>().FirstOrDefault(x => x.MechPiece == MechPieces.Torso);

                if (playerRenderTorso != null)
                {
                    var playerPos = playerRenderTorso.Position;
                    var xdiff = (playerPos.X - mousePos.X) * 0.25;
                    var ydiff = (playerPos.Y - mousePos.Y) * 0.25;

                    var x = playerPos.X - xdiff;
                    if (x < LeftEdge)
                    {
                        x = LeftEdge;
                    }
                    else if (x > RightEdge)
                    {
                        x = RightEdge;
                    }
                    var y = playerPos.Y - ydiff;
                    if (y < TopEdge)
                    {
                        y = TopEdge;
                    }
                    else if (y > BottomEdge)
                    {
                        y = BottomEdge;
                    }

                    Engine.Camera = Engine.Camera with
                    {
                        target = new Vector2((float)x, (float)y),
                        offset = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2)
                    };

                }
            }
        }
    }
}