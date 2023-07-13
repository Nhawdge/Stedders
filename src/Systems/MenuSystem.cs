using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Systems
{
    public class MenuSystem : GameSystem
    {
        public Font RobotoFont = Raylib.LoadFont("Assets/Roboto.ttf");
        public MenuSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }
        public override void Update() { }

        public override void UpdateNoCamera()
        {
            Raylib.DrawText(Raylib.GetFPS().ToString(), Raylib.GetScreenWidth() - 50, 20, 20, Raylib.WHITE);
            var state = Engine.Singleton.GetComponent<GameState>();

            if (state.State == States.MainMenu)
            {
                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
                RayGui.GuiDummyRec(centerPane, "");

                var title = TranslationManager.GetTranslation("stedders");
                var fontSize = 80;
                var titleWidth = Raylib.MeasureText(title, fontSize);

                Raylib.DrawTextEx(RobotoFont,
                    title,
                    new Vector2(Raylib.GetScreenWidth() / 2 - titleWidth / 2, 100),
                    fontSize, 10f, Raylib.WHITE);


                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_ARROW);

                var width = 120;
                var height = 60;
                var positionX = Raylib.GetScreenWidth() / 2 - width / 2;
                var positionY = Raylib.GetScreenHeight() / 2 - height / 2 - 130;

                var rect = new Rectangle(positionX, positionY, width, height);

                if (RayGui.GuiButton(rect, TranslationManager.GetTranslation("play")))
                {
                    state.State = States.Start;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
                if (RayGui.GuiButton(rect with { y = rect.y + height + 10 }, TranslationManager.GetTranslation("credits")))
                {
                    state.State = States.Credits;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
                if (RayGui.GuiButton(rect with { y = rect.y + (height * 4) + 10 }, TranslationManager.GetTranslation("exit")))
                {
                    Raylib.CloseWindow();
                    Environment.Exit(0);
                }
            }
            else if (state.State == States.Game)
            {

            }
            else if (state.State == States.Pause)
            {
                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_ARROW);

                var width = 60;
                var height = 30;
                var positionWidth = Raylib.GetScreenWidth() / 2 - width / 2;
                var positionHeight = Raylib.GetScreenHeight() / 2 - height / 2;

                var rect = new Rectangle(positionWidth, positionHeight, width, height);

                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
                RayGui.GuiDummyRec(centerPane, "");

                if (RayGui.GuiButton(rect, TranslationManager.GetTranslation("resume")))
                {
                    state.State = States.Game;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
            }
            else if (state.State == States.Credits)
            {
                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
                RayGui.GuiDummyRec(centerPane, "");

                var text = TranslationManager.GetTranslation("credits-full");

                var fontSize = 30;

                RayGui.GuiTextBox(centerPane, text, fontSize, false);

                var backRect = new Rectangle(centerPane.x + centerPane.width / 2 - 50, centerPane.y + centerPane.height - 50, 100, 30);
                if (RayGui.GuiButton(backRect, TranslationManager.GetTranslation("back")))
                {
                    state.State = States.MainMenu;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
            }
        }

    }
}