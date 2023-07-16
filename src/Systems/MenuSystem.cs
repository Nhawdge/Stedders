using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
using System.Numerics;
using System.Reflection;

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
                state.IntroAnimationTiming += 50 * Raylib.GetFrameTime();
                var titleTiming = Math.Min(state.IntroAnimationTiming, 255);

                var title = TranslationManager.GetTranslation("stedders");
                var fontSize = 80;
                var titleWidth = Raylib.MeasureText(title, fontSize);
                var color = new Color(255, 255, 255, (int)titleTiming);
                Raylib.DrawTextEx(RobotoFont,
                    title,
                    new Vector2(Raylib.GetScreenWidth() / 2 - titleWidth / 2, 100 + 255 - titleTiming),
                    fontSize, 10f, color);

                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_ARROW);

                var menuFade = Math.Max(Math.Min(state.IntroAnimationTiming - 255, 255) * 5, 0);
                RayGui.GuiFade(menuFade / 255);

                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
                RayGui.GuiDummyRec(centerPane, "");
                var width = 200;
                var height = 60;
                var positionX = Raylib.GetScreenWidth() / 2 - width / 2;
                var positionY = Raylib.GetScreenHeight() / 2 - height / 2 - 140;

                var rect = new Rectangle(positionX, positionY, width, height);

                if (RayGui.GuiButton(rect, TranslationManager.GetTranslation("play")))
                {
                    state.State = States.Start;
                    RayGui.GuiFade(255);
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
                if (RayGui.GuiButton(rect with { y = rect.y + height + 10 }, TranslationManager.GetTranslation("howto")))
                {
                    RayGui.GuiFade(255);
                    state.State = States.HowTo;
                }
                if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 2 }, TranslationManager.GetTranslation("options")))
                {
                    RayGui.GuiFade(255);
                    state.State = States.Options;
                }
                if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 3 }, TranslationManager.GetTranslation("credits")))
                {
                    RayGui.GuiFade(255);
                    state.State = States.Credits;
                }
                if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 4 }, TranslationManager.GetTranslation("exit")))
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

                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - 200, Raylib.GetScreenHeight() / 2 - 200, 400, 400);
                RayGui.GuiDummyRec(centerPane, "");

                var rect = centerPane with { x = centerPane.x + centerPane.width / 4, y = centerPane.y + 30, height = 60, width = 200, };

                if (RayGui.GuiButton(rect with { y = rect.y - 0 }, TranslationManager.GetTranslation("resume")))
                {
                    state.State = States.Game;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
                }
                if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 1 }, TranslationManager.GetTranslation("howto")))
                {
                    state.State = States.HowTo;
                }
                if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 2 }, TranslationManager.GetTranslation("options")))
                {
                    state.State = States.Options;
                }

                if (RayGui.GuiButton(rect with { y = rect.y + (height + 10) * 4 }, TranslationManager.GetTranslation("exit")))
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
            else if (state.State == States.Options)
            {
                var boxWidth = 650;
                var boxHeight = 700;
                var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - boxWidth / 2, Raylib.GetScreenHeight() / 2 - boxHeight / 2, boxWidth, boxHeight);
                RayGui.GuiDummyRec(centerPane, "");

                var text = TranslationManager.GetTranslation("volume");
                state.MainVolume = RayGui.GuiSlider(
                    centerPane with { x = centerPane.x + Raylib.MeasureText(text, RayGui.GuiGetFont().baseSize), y = centerPane.y / 2 + 50, height = 30, width = 400 },
                   text, state.MainVolume.ToString("#0%"),
                    state.MainVolume, 0, 1);

                ////var text = TranslationManager.GetTranslation("howto-full");

                //RayGui.GuiLabel(centerPane with { x = centerPane.x + 15, height = centerPane.height - 150 }, text);

                var backRect = new Rectangle(centerPane.x + centerPane.width / 2 - 50, centerPane.y + centerPane.height - 50, 100, 30);
                if (RayGui.GuiButton(backRect, TranslationManager.GetTranslation("back")))
                {
                    state.State = state.LastState;
                }
            }
        }
    }
}