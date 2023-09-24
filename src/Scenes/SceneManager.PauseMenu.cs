using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        private BaseScene LoadPauseMenu()
        {
            //state.GuiOpen = true;
            Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_ARROW);
            var pauseScene = new BaseScene();

            var menu = new Entity();
            menu.Components.Add(new UiTitle() { Text = "Stedders" });
            menu.Components.Add(new UiButton
            {
                Text = "Resume",
                OnClick = () =>
                {
                    SceneManager.Instance.LoadScene(SceneKey.FreestyleRanch.World);
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
                Text = "Stats",
                OnClick = () =>
                {
                    SceneManager.Instance.LoadScene(SceneKey.Menu.Stats);
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

            pauseScene.Entities.Add(menu);

            return pauseScene;
        }

    }
}
