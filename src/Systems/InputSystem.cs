using Raylib_CsLo;
using Stedders.Components;

namespace Stedders.Systems
{
    public class InputSystem : GameSystem
    {
        public Dictionary<KeyboardKey, Action> KeyboardMapping = new Dictionary<KeyboardKey, Action>();
        public InputSystem(GameEngine gameEngine) : base(gameEngine)
        {
            KeyboardMapping.Add(KeyboardKey.KEY_D, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }

                var playerRenderLegs = playerMech.GetComponents<Render>().FirstOrDefault(x => x.MechPiece == Render.MechPieces.Legs);
                playerRenderLegs!.Rotation += 1f;
            });

            KeyboardMapping.Add(KeyboardKey.KEY_A, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }

                var playerRenderLegs = playerMech.GetComponents<Render>().FirstOrDefault(x => x.MechPiece == Render.MechPieces.Legs);
                playerRenderLegs!.Rotation -= 1f;
            });
            KeyboardMapping.Add(KeyboardKey.KEY_W, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                playerMech.GetComponent<Player>().Throttle += 0.1f;

            });
            KeyboardMapping.Add(KeyboardKey.KEY_S, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                playerMech.GetComponent<Player>().Throttle -= 0.1f;
            });
            KeyboardMapping.Add(KeyboardKey.KEY_ONE, () => { Console.WriteLine("1 Pressed"); });
        }

        public override void Update()
        {
            foreach (var mapping in KeyboardMapping)
            {
                if (Raylib.IsKeyDown(mapping.Key))
                {
                    mapping.Value();
                }
            }
            var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
            if (playerMech is null)
            {
                return;
            }

            var playerRenderTorso = playerMech.GetComponents<Render>().FirstOrDefault(x => x.MechPiece == Render.MechPieces.Torso);

            var mousePos = Raylib.GetMousePosition();
            var mouseWorldPos = Raylib.GetScreenToWorld2D(mousePos, Engine.Camera);
            var difference = mouseWorldPos - playerRenderTorso.Position;
            var angle = Math.Atan2(difference.Y, difference.X);
            playerRenderTorso.Rotation = (float)(angle * 180 / Math.PI) + 90;
        }
    }
}
