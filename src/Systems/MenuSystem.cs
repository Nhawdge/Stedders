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
            var state = Engine.Singleton.GetComponent<GameState>();
            
            if (state.State == States.MainMenu)
            {
                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_ARROW);

                var width = 60;
                var height = 30;
                var positionWidth = Raylib.GetScreenWidth() / 2 - width / 2;
                var positionHeight = Raylib.GetScreenHeight() / 2 - height / 2;

                var rect = new Rectangle(positionWidth, positionHeight, width, height);

                //var key = Raylib.GetKeyPressed_();

                //if (key != KeyboardKey.KEY_NULL)
                //{
                //    Console.WriteLine($"Key:{key}, {selectedInput}");
                //}
                //if (selectedInput == "a")
                //{
                //    if (key != 0)
                //    {
                //        holder += ((char)key).ToString();
                //    }
                //}
                //else if (selectedInput == "b")
                //{
                //    if (key == KeyboardKey.KEY_BACKSPACE)
                //    {
                //        holder2 = holder2.Substring(0, int.Max(0, holder2.Length - 1));
                //    }
                //    else if (key != 0)
                //    {
                //        holder2 += ((char)key).ToString();
                //        Console.WriteLine($"Key:{key}");
                //    }
                //}

                //var a = RayGui.GuiTextBox(rect, holder, 12, selectedInput == "a");
                //if (a)
                //{
                //    Console.WriteLine($"A:{a}");
                //    selectedInput = "a";
                //}

                //var b = RayGui.GuiTextBox(rect with { Y = rect.Y * 2 }, holder2, 12, selectedInput == "b");
                //if (b)
                //{
                //    Console.WriteLine($"B:{b}");
                //    selectedInput = "b";
                //}

                if (RayGui.GuiButton(rect, "Play"))
                {
                    state.State = States.Start;
                    Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_CROSSHAIR);
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
        }

    }
}