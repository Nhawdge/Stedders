using Raylib_CsLo;
using Stedders.Components;
using Stedders.Systems;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        internal static SceneManager Instance { get; } = new();
        public GameEngine Engine { get; }
        internal Dictionary<string, BaseScene> Scenes { get; set; } = new();

        private SceneManager()
        {
            Engine = GameEngine.Instance;
        }

        internal BaseScene LoadScene(string key)
        {
            return key switch
            {
                SceneKey.Menu.MainMenu => MainMenu(),
                SceneKey.Menu.HowToPlay => HowToPlay(),
                SceneKey.FreestyleRanch.World => FreestyleRanch(),
                SceneKey.FreestyleRanch.Barn => FreestyleRanchBarn(),
                SceneKey.NewLand.World => NewLand(),
                _ => throw new NotImplementedException(),
            };
        }


        internal void ChangeScene(string key, bool forceReload = false)
        {
            if (Scenes.TryGetValue(key, out var scene) && forceReload == false)
            {
                Engine.ActiveScene = scene;
            }
            var nextScene = LoadScene(key);
            if (Scenes.TryAdd(key, nextScene))
            {

            };
            Engine.ActiveScene = nextScene;
        }

        public static class SceneKey
        {
            public static class Menu
            {
                public const string MainMenu = "MainMenu";
                public const string HowToPlay = "HowToPlay";
                public const string Options = "Options";
                public const string Credits = "Credits";

                public const string PauseMenu = "PauseMenu";
                public const string Stats = "Stats";
                public const string GameOver = "GameOver";
            }

            public static class FreestyleRanch
            {
                public const string World = "FreestyleRanch";
                public const string Barn = "FreestyleRanch_Barn";
            }
            public static class NewLand
            {
                public const string World = "NewLand";
            }
        }

        public static Dictionary<KeyboardKey, Action> GetBaseKeyboardMap()
        {
            Dictionary<KeyboardKey, Action> KeyboardMapping = new();

            var state = GameEngine.Instance.Singleton.GetComponent<GameState>();
            KeyboardMapping.Add(KeyboardKey.KEY_D, () =>
            {
                var playerMech = GameEngine.Instance.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                var player = playerMech.GetComponent<Player>();

                player.Movement.X = 1;
            });

            KeyboardMapping.Add(KeyboardKey.KEY_A, () =>
            {
                var playerMech = GameEngine.Instance.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                var player = playerMech.GetComponent<Player>();

                player.Movement.X = -1;
            });
            KeyboardMapping.Add(KeyboardKey.KEY_W, () =>
            {
                var playerMech = GameEngine.Instance.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                var player = playerMech.GetComponent<Player>();

                player.Movement.Y = -1;
            });
            KeyboardMapping.Add(KeyboardKey.KEY_S, () =>
            {
                var playerMech = GameEngine.Instance.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                var player = playerMech.GetComponent<Player>();

                player.Movement.Y = 1;
            });

            KeyboardMapping.Add(KeyboardKey.KEY_ESCAPE, () =>
            {
                var state = GameEngine.Instance.Singleton.GetComponent<GameState>();
                //if (state.State == States.Game)
                //{
                //    state.State = States.Pause;
                //}
            });

            KeyboardMapping.Add(KeyboardKey.KEY_ONE, () =>
            {
                var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), GameEngine.Instance.Camera);
                Console.WriteLine($"Screen: {Raylib.GetMousePosition()}, World: {mousePos}");
            });

            return KeyboardMapping;
        }

        public static Dictionary<MouseButton, Action> GetBaseMouseMap()
        {
            var state = GameEngine.Instance.Singleton.GetComponent<GameState>();

            Dictionary<MouseButton, Action> MouseMapping = new();
            MouseMapping.Add(MouseButton.MOUSE_BUTTON_LEFT, () =>
                {
                    var playerMech = GameEngine.Instance.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                    if (playerMech is null)
                        return;

                    if (state.GuiOpen == true)
                        return;

                    var player = playerMech.GetComponent<Player>();
                    var weapon = player.Inventory.FirstOrDefault(x => x.Button == MouseButton.MOUSE_BUTTON_LEFT);
                    if (weapon is not null)
                        weapon.IsFiring = true;

                });
            MouseMapping.Add(MouseButton.MOUSE_BUTTON_RIGHT, () =>
            {
                var playerMech = GameEngine.Instance.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                    return;

                if (state.GuiOpen == true)
                    return;

                var player = playerMech.GetComponent<Player>();
                var weapon = player.Inventory.FirstOrDefault(x => x.Button == MouseButton.MOUSE_BUTTON_RIGHT);
                if (weapon is not null)
                    weapon.IsFiring = true;
            });
            return MouseMapping;
        }

    }
}
