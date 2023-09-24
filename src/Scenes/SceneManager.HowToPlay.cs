using Raylib_CsLo;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        private BaseScene HowToPlay()
        {
            var scene = new BaseScene();
            //state.GuiOpen = true;
            var boxWidth = 650;
            var boxHeight = 700;
            var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - boxWidth / 2, Raylib.GetScreenHeight() / 2 - boxHeight / 2, boxWidth, boxHeight);
            RayGui.GuiDummyRec(centerPane, "");

            var text = TranslationManager.GetTranslation("howto-full");

            RayGui.GuiLabel(centerPane with { x = centerPane.x + 15, height = centerPane.height - 150 }, text);

            var backRect = new Rectangle(centerPane.x + centerPane.width / 2 - 100, centerPane.y + centerPane.height - 100, 200, 60);

            if (RayGui.GuiButton(backRect, TranslationManager.GetTranslation("back")))
            {
                //state.State = state.LastState;
                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
            }
            return scene;
        }
    }
}
