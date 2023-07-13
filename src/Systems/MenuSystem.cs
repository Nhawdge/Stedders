using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Systems
{
    public class MenuSystem : GameSystem
    {
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
                Raylib.DrawText(title, Raylib.GetScreenWidth() / 2 - titleWidth / 2, 100, fontSize, Raylib.WHITE);

                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_ARROW);

                var width = 60;
                var height = 30;
                var positionX = Raylib.GetScreenWidth() / 2 - width / 2;
                var positionY = Raylib.GetScreenHeight() / 2 - height / 2;

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
                if (RayGui.GuiButton(rect with { y = rect.y + (height *4) + 10 }, TranslationManager.GetTranslation("exit")))
                {
                    Raylib.CloseWindow();
                    Environment.Exit(0);
                }
            }
            else if (state.State == States.Game)
            {
                //var playerHq = Engine.Entities.Where(x => x.HasTypes(typeof(Headquarters), typeof(Faction))).FirstOrDefault(x => x.GetComponent<Faction>().Team == Factions.Player);
                //if (playerHq is null)
                //{
                //    return;
                //}
                //var myHq = playerHq.GetComponent<Headquarters>();
                //Raylib.DrawText($"{myHq.Wealth.ToString("C")}", 10, 20, 12, Raylib.BLACK);

                #region Units

                //var bottom = Raylib.GetScreenHeight() - 70;
                //var offset = Raylib.GetScreenWidth() / 3;

                //if (RayGui.GuiButton(new Rectangle(offset, bottom, 64, 64), ""))
                //{
                //    var playerBase = Engine.Entities.Find(x => x.GetComponent<Faction>().Team == Data.Factions.Player);
                //    if (playerBase is not null)
                //    {
                //        playerBase.Components.Add(new SpawnCommand(Units.Bob));
                //    }
                //}
                //Raylib.DrawRectangleLines(offset, bottom, 64, 64, Raylib.BLACK);
                //Raylib.DrawTextureEx(Engine.TextureManager.TextureStore[Textures.Bob], new Vector2(offset, bottom), 0f, 2, Raylib.WHITE);
                //Raylib.DrawText($"{UnitsManager.Stats[Units.Bob].Name}\n{UnitsManager.Stats[Units.Bob].Cost.ToString("C")}", offset, bottom - 36, 12, Raylib.BLACK);

                //offset += 74;
                //if (RayGui.GuiButton(new Rectangle(offset, bottom, 64, 64), ""))
                //{
                //    var playerBase = Engine.Entities.Find(x => x.GetComponent<Faction>().Team == Data.Factions.Player);
                //    if (playerBase is not null)
                //    {
                //        playerBase.Components.Add(new SpawnCommand(Units.Kevin));
                //    }
                //}
                //Raylib.DrawRectangleLines(offset, bottom, 64, 64, Raylib.BLACK);
                //Raylib.DrawTextureEx(Engine.TextureManager.TextureStore[Textures.Kevin], new Vector2(offset, bottom), 0f, 2, Raylib.WHITE);
                //Raylib.DrawText($"{UnitsManager.Stats[Units.Kevin].Name}\n{UnitsManager.Stats[Units.Kevin].Cost.ToString("C")}", offset, bottom - 36, 12, Raylib.BLACK);


                //offset += 74;
                //if (RayGui.GuiButton(new Rectangle(offset, bottom, 64, 64), ""))
                //{
                //    var playerBase = Engine.Entities.Find(x => x.GetComponent<Faction>().Team == Data.Factions.Player);
                //    if (playerBase is not null)
                //    {
                //        playerBase.Components.Add(new SpawnCommand(Units.Tim));
                //    }
                //}
                //Raylib.DrawRectangleLines(offset, bottom, 64, 64, Raylib.BLACK);
                //Raylib.DrawTextureEx(Engine.TextureManager.TextureStore[Textures.Tim], new Vector2(offset, bottom), 0f, 2, Raylib.WHITE);
                //Raylib.DrawText($"{UnitsManager.Stats[Units.Tim].Name}\n{UnitsManager.Stats[Units.Tim].Cost.ToString("C")}", offset, bottom - 36, 12, Raylib.BLACK);

                #endregion Units

                //// Tool tip
                //var mousePos = Raylib.GetMousePosition();

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

                var title = TranslationManager.GetTranslation("credits-full");
                var fontSize = 30;
                var titleWidth = Raylib.MeasureText(title, fontSize);
                RayGui.GuiTextBox(centerPane, title, fontSize, false);

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