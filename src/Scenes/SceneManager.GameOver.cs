using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        private BaseScene GameOver()
        {
            var scene = new BaseScene();
            var menu = new Entity();
            var state = Engine.Singleton.GetComponent<GameState>();

            //state.GuiOpen = true;
            Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_ARROW);

            var width = 200;
            var height = 60;
            var positionWidth = Raylib.GetScreenWidth() / 2 - width / 2;
            var positionHeight = Raylib.GetScreenHeight() / 2 - height / 2;

            var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
            RayGui.GuiDummyRec(centerPane, "");

            var rect = centerPane with { x = centerPane.x + centerPane.width / 4, y = centerPane.y + 30, height = 60, width = 200, };

            if (RayGui.GuiButton(rect with { y = rect.y - 0 }, TranslationManager.GetTranslation("restart")))
            {
                //state.State = States.Start;
                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
            }
            if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 1 }, TranslationManager.GetTranslation("howto")))
            {
                //state.State = States.HowTo;
            }
            if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 2 }, TranslationManager.GetTranslation("stats")))
            {
                //state.State = States.Stats;
            }

            if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 4 }, TranslationManager.GetTranslation("exit")))
            {
                Raylib.CloseWindow();
                Environment.Exit(0);
            }

            return scene;
        }
    }
}
