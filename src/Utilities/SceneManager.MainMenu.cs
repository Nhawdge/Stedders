using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        public static BaseScene MainMenu()
        {
            var mainMenu = new BaseScene();

            // Play, How to play, Options, Credits, Exit 

            var menu = new Entity();
            menu.Components.Add(new UiTitle() { Text = "Stedders" });
            menu.Components.Add(new UiButton
            {
                Text = "Play",
                OnClick = () =>
                {
                    SceneManager.Instance.ChangeScene(SceneKey.FreestyleRanch.World);
                },
            });
            menu.Components.Add(new UiButton
            {
                Text = "How to play",
                OnClick = () =>
                {
                    SceneManager.Instance.LoadScene(SceneKey.Menu.HowToPlay);
                },
            });
            menu.Components.Add(new UiButton
            {
                Text = "Options",
                OnClick = () =>
                {
                    SceneManager.Instance.LoadScene(SceneKey.Menu.Options);
                },
            });
            menu.Components.Add(new UiButton
            {
                Text = "Credits",
                OnClick = () =>
                {
                    SceneManager.Instance.LoadScene(SceneKey.Menu.Credits);
                },
            });
            menu.Components.Add(new UiButton
            {
                Text = "Exit",
                OnClick = () =>
                {
                    Raylib.CloseWindow();
                    Environment.Exit(0);
                },
            });

            mainMenu.Entities.Add(menu);

            return mainMenu;
        }
    }
}
