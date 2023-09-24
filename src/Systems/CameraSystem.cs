using Stedders.Components;
using System.Numerics;
using static Raylib_CsLo.Raylib;

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

            //if (state.State is States.MainMenu or States.Dialogue)
            //{
            //    Engine.Camera = Engine.Camera with
            //    {
            //        target = new Vector2(2021, 1712),
            //        offset = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2)
            //    };
            //}

            float LeftEdge = 0 + GetScreenWidth() / 2;
            float RightEdge = Engine.ActiveScene.MapEdge.X - GetScreenWidth() / 2;
            float TopEdge = 0 + GetScreenHeight() / 2;
            float BottomEdge = Engine.ActiveScene.MapEdge.Y - GetScreenHeight() / 2;
            var mousePos = GetScreenToWorld2D(GetMousePosition(), Engine.Camera);

            var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
            if (playerMech is null)
            {
                return;
            }
            var playerLegs = playerMech.GetComponents<Render>().FirstOrDefault();

            if (playerLegs != null)
            {
                var playerPos = playerLegs.Position;
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