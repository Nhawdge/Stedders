using Raylib_CsLo;
using Stedders.Components;

namespace Stedders.Systems
{
    public class InputSystem : GameSystem
    {
        public Dictionary<KeyboardKey, Action> KeyboardMapping = new Dictionary<KeyboardKey, Action>();
        public Dictionary<MouseButton, Action> MouseMapping = new Dictionary<MouseButton, Action>();
        public InputSystem(GameEngine gameEngine) : base(gameEngine)
        {
            KeyboardMapping.Add(KeyboardKey.KEY_D, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }

                var playerRenderLegs = playerMech.GetComponents<Sprite>().FirstOrDefault(x => x.MechPiece == MechPieces.Legs);
                playerRenderLegs!.Rotation += 1f;
            });

            KeyboardMapping.Add(KeyboardKey.KEY_A, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }

                var playerRenderLegs = playerMech.GetComponents<Sprite>().FirstOrDefault(x => x.MechPiece == MechPieces.Legs);
                playerRenderLegs!.Rotation -= 1f;
            });
            KeyboardMapping.Add(KeyboardKey.KEY_W, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                var player = playerMech.GetComponent<Player>();

                player.Throttle = Math.Min(player.Throttle + 0.1f, player.MaxThrottle);
            });
            KeyboardMapping.Add(KeyboardKey.KEY_S, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                var player = playerMech.GetComponent<Player>();

                player.Throttle = Math.Max(player.Throttle - 0.1f, player.MinThrottle);
            });
            KeyboardMapping.Add(KeyboardKey.KEY_SPACE, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                var player = playerMech.GetComponent<Player>();

                player.Throttle = 0f;
            });

            KeyboardMapping.Add(KeyboardKey.KEY_ESCAPE, () =>
            {
                var state = Engine.Singleton.GetComponent<GameState>();
                if (state.State == States.Game)
                {
                    state.State = States.Pause;
                }
                //else if (state.State == States.Pause)
                //{
                //    state.State = States.Game;
                //}
            });

            KeyboardMapping.Add(KeyboardKey.KEY_ONE, () =>
            {
                var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Engine.Camera);
                Console.WriteLine($"Screen: {Raylib.GetMousePosition()}, World: {mousePos}");
            });

            /// MOUSE MAP

            MouseMapping.Add(MouseButton.MOUSE_BUTTON_LEFT, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                    return;

                var player = playerMech.GetComponent<Player>();
                var weapon = playerMech.GetComponents<Equipment>().FirstOrDefault(x => x.Button == MouseButton.MOUSE_BUTTON_LEFT);
                if (weapon is not null)
                    weapon.IsFiring = true;

            });
            MouseMapping.Add(MouseButton.MOUSE_BUTTON_RIGHT, () =>
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                    return;

                var player = playerMech.GetComponent<Player>();
                var weapon = playerMech.GetComponents<Equipment>().FirstOrDefault(x => x.Button == MouseButton.MOUSE_BUTTON_RIGHT);
                if (weapon is not null)
                    weapon.IsFiring = true;
            });
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

            foreach (var mapping in MouseMapping)
            {
                if (Raylib.IsMouseButtonDown(mapping.Key))
                {
                    mapping.Value();
                }
            }

            var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
            if (playerMech is not null)
            {
                var playerRenderTorso = playerMech.GetComponents<Render>().First(x => x.MechPiece == MechPieces.Torso);

                var mousePos = Raylib.GetMousePosition();
                var mouseWorldPos = Raylib.GetScreenToWorld2D(mousePos, Engine.Camera);
                var difference = mouseWorldPos - playerRenderTorso.Position;
                var angle = Math.Atan2(difference.Y, difference.X);
                playerRenderTorso.Rotation = (float)(angle * 180 / Math.PI) + 90;
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
