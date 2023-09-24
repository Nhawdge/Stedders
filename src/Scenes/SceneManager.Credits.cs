using Raylib_CsLo;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        private BaseScene Credits()
        {
            var scene = new BaseScene();
            //state.GuiOpen = true;
            var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
            RayGui.GuiDummyRec(centerPane, "");

            var text = TranslationManager.GetTranslation("credits-full");

            RayGui.GuiLabel(centerPane with { height = centerPane.height - 200 }, text);

            var backRect = new Rectangle(centerPane.x + centerPane.width / 2 - 50, centerPane.y + centerPane.height - 50, 100, 30);
            if (RayGui.GuiButton(backRect, TranslationManager.GetTranslation("back")))
            {
                //state.State = state.LastState;
                //Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
            }

            return scene;
        }
    }
}
