using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using System.Security.Authentication.ExtendedProtection;

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
            Scenes.Add(key, nextScene);
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
        }
    }
}
