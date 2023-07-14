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
#if DEBUG
            Raylib.DrawText(Raylib.GetFPS().ToString(), Raylib.GetScreenWidth() - 50, 20, 20, Raylib.WHITE);
#endif
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

                var width = 200;
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
                }
                if (RayGui.GuiButton(rect with { y = rect.y + (height * 2) + 20 }, TranslationManager.GetTranslation("howto")))
                {
                    state.State = States.HowTo;
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

                var width = 200;
                var height = 60;
                var positionWidth = Raylib.GetScreenWidth() / 2 - width / 2;
                var positionHeight = Raylib.GetScreenHeight() / 2 - height / 2;

                var rect = new Rectangle(positionWidth, positionHeight, width, height);

                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
                RayGui.GuiDummyRec(centerPane, "");

                if (RayGui.GuiButton(rect with { y = rect.y - 100 }, TranslationManager.GetTranslation("resume")))
                {
                    state.State = States.Game;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
                if (RayGui.GuiButton(rect with { y = rect.y + (height * 0) + 10 }, TranslationManager.GetTranslation("howto")))
                {
                    state.State = States.HowTo;
                    state.LastState = States.Pause;
                }

                if (RayGui.GuiButton(rect with { y = rect.y + (height * 2) + 10 }, TranslationManager.GetTranslation("exit")))
                {
                    Raylib.CloseWindow();
                    Environment.Exit(0);
                }
            }
            else if (state.State == States.Credits)
            {
                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
                RayGui.GuiDummyRec(centerPane, "");

                var text = TranslationManager.GetTranslation("credits-full");

                RayGui.GuiLabel(centerPane with { height = centerPane.height - 200 }, text);

                var backRect = new Rectangle(centerPane.x + centerPane.width / 2 - 50, centerPane.y + centerPane.height - 50, 100, 30);
                if (RayGui.GuiButton(backRect, TranslationManager.GetTranslation("back")))
                {
                    state.State = state.LastState;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
            }
            else if (state.State == States.HowTo)
            {
                var boxWidth = 650;
                var boxHeight = 700;
                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - boxWidth / 2, Raylib.GetScreenHeight() / 2 - boxHeight / 2, boxWidth, boxHeight);
                RayGui.GuiDummyRec(centerPane, "");

                var text = TranslationManager.GetTranslation("howto-full");

                RayGui.GuiLabel(centerPane with { x = centerPane.x + 15, height = centerPane.height - 150 }, text);

                var backRect = new Rectangle(centerPane.x + centerPane.width / 2 - 50, centerPane.y + centerPane.height - 50, 100, 30);
                if (RayGui.GuiButton(backRect, TranslationManager.GetTranslation("back")))
                {
                    state.State = state.LastState;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
            }
        }
    }
}