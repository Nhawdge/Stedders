using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
using System.Collections.Immutable;
using System.Net.WebSockets;
using System.Numerics;

namespace Stedders.Systems
{
    internal class MechUISystem : GameSystem
    {
        public MechUISystem(GameEngine gameEngine) : base(gameEngine)
        {
        }
        public override void Update()
        {
            return;
        }
        public override void UpdateNoCamera()
        {
            var state = Engine.Singleton.GetComponent<GameState>();

            if (state.State == States.Game)
            {
                var playerMech = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
                if (playerMech is null)
                {
                    return;
                }
                // GUI Stuff
                var player = playerMech.GetComponent<Player>();
                var speed = player.Throttle;

                RayGui.GuiDummyRec(new Rectangle(5, 5, 240, 100), "");

                var spread = 22;
                var x = 10;
                var y = 10;
                RayGui.GuiSliderBar(new Rectangle(x, y, 100, 20), string.Empty, "Throttle", speed, player.MinThrottle, player.MaxThrottle);

                y += spread;
                var equippedItems = playerMech.GetComponents<Equipment>();
                foreach (var item in equippedItems)
                {
                    RayGui.GuiSliderBar(new Rectangle(x, y, 100, 20), string.Empty, item.Name, item.Ammo, 0, item.MaxAmmo);
                    y += spread;
                }

                var dayRect = new Rectangle(x, y, 50, 30);
                RayGui.GuiLabel(dayRect, $"Day: {state.Day} Time: {state.CurrentTime.ToString("#")}");
                y += spread;

                RayGui.GuiLabel(dayRect with { y = dayRect.y + spread + 2 }, $"Biomass: {player.BioMassContainer.ToString("0.#")}/{player.MaxBioMassContainer.ToString("0")}");
                RayGui.GuiLabel(dayRect with { y = dayRect.y + spread * 2 + 2 }, $"{state.Currency.ToString("C")}");

                // Barn stuff

                var barn = Engine.Entities.Where(x => x.HasTypes(typeof(Barn))).FirstOrDefault();
                var barnComponent = barn.GetComponent<Barn>();
                var barnPos = barn.GetComponent<Position>().Pos;
                var playerSprite = playerMech.GetComponents<Render>().FirstOrDefault(x => x.MechPiece == MechPieces.Legs);
                var posDiff = barnPos - playerSprite.Position;
                var distance = posDiff.Length();
                var topButton = new Rectangle(Raylib.GetScreenWidth() / 2 - 100, 20, 200, 60);
                if (distance < 50)
                {
                    if (RayGui.GuiButton(topButton, TranslationManager.GetTranslation(barnComponent.IsOpen ? "close" : "open")))
                    {
                        barnComponent.IsOpen = !barnComponent.IsOpen;
                    }
                }
                else
                {
                    barnComponent.IsOpen = false;
                }

                if (barnComponent.IsOpen)
                {
                    var width = 500;
                    var height = 600;
                    var container = new Rectangle(
                        Raylib.GetScreenWidth() / 2 - width / 2,
                        Raylib.GetScreenHeight() / 2 - height / 2,
                        width,
                        height);

                    RayGui.GuiDummyRec(container, "");

                    var index = 0;
                    var inventoryToRemove = new List<Equipment>();
                    var inventorytoAdd = new List<Equipment>();
                    foreach (var item in barnComponent.Equipment.OrderByDescending(x => x.Button))
                    {
                        var itemRect = new Rectangle(container.x + 10, container.y + 60 * index, 50, 50);
                        var itemTexture = Engine.TextureManager.GetTexture(item.IconKey);
                        Raylib.DrawTexturePro(itemTexture,
                            new Rectangle(0, 0, itemTexture.width, itemTexture.height),
                            itemRect with { y = itemRect.y + 10, width = 50, height = 50 }, Vector2.Zero, 0f, Raylib.WHITE);
                        var buttonRect = itemRect with { x = itemRect.x + 55, y = itemRect.y + 10, width = width - 70 };
                        if (RayGui.GuiButton(buttonRect, item.Name))
                        {
                            var currentLeft = playerMech.GetComponents<Equipment>().FirstOrDefault(x => x.Button == MouseButton.MOUSE_BUTTON_LEFT);
                            if (currentLeft is not null)
                            {
                                inventorytoAdd.Add(currentLeft);
                                playerMech.Components.Remove(currentLeft);
                            }
                            item.Button = MouseButton.MOUSE_BUTTON_LEFT;
                            playerMech.Components.Add(item);
                            inventoryToRemove.Add(item);

                        }
                        var mousePos = Raylib.GetMousePosition();
                        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT))
                        {
                            if (Raylib.CheckCollisionPointRec(mousePos, buttonRect))
                            {
                                var currentRight = playerMech.GetComponents<Equipment>().FirstOrDefault(x => x.Button == MouseButton.MOUSE_BUTTON_RIGHT);
                                if (currentRight is not null)
                                {
                                    inventorytoAdd.Add(currentRight);
                                    playerMech.Components.Remove(currentRight);
                                }
                                item.Button = MouseButton.MOUSE_BUTTON_RIGHT;
                                playerMech.Components.Add(item);
                                inventoryToRemove.Add(item);
                            }
                        }
                        index++;
                    }
                    inventorytoAdd.ForEach(x => barnComponent.Equipment.Add(x));
                    inventoryToRemove.ForEach(x => barnComponent.Equipment.Remove(x));
                    RayGui.GuiLabel(new Rectangle(container.x, container.height - 10, 500, 50), TranslationManager.GetTranslation("equip-help"));
                }

                var silo = Engine.Entities.Where(x => x.HasTypes(typeof(Silo))).FirstOrDefault();
                var siloComponent = silo.GetComponent<Silo>();
                var siloPos = silo.GetComponent<Position>().Pos;
                posDiff = siloPos - playerSprite.Position;
                distance = posDiff.Length();
                if (distance < 50)
                {
                    if (RayGui.GuiButton(topButton, TranslationManager.GetTranslation("sell-biomass")))
                    {
                        state.Currency += player.BioMassContainer * 10;
                        player.BioMassContainer = 0f;
                    }
                }
            }
        }
    }
}
