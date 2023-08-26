using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Utilities
{
    internal class SceneManager
    {
        internal static SceneManager Instance { get; } = new();
        public GameEngine Engine { get; }

        private SceneManager()
        {
            Engine = GameEngine.Instance;
        }

        internal BaseScene LoadScene(SceneKey key)
        {
            return key switch
            {
                SceneKey.MainMenu => LoadMainMenu(),
                SceneKey.Game => LoadMainGame(),
                _ => throw new NotImplementedException(),
            };
        }

        private BaseScene LoadMainMenu()
        {
            var mainMenu = new BaseScene();
            var map = new Entity();
            var buildMap = MapManager.Instance.GetMap("FreestyleRanch");
            map.Components.Add(buildMap);

            mainMenu.Entities.Add(map);
            mainMenu.Entities.AddRange(buildMap.EntitiesToAdd);

            return mainMenu;
        }

        private BaseScene LoadMainGame()
        {
            var mainGame = new BaseScene();
            return mainGame;
        }

        public enum SceneKey
        {
            MainMenu,
            Game,
        }

    }
}
