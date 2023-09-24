using Raylib_CsLo;
using Stedders.Components;
using Stedders.Resources;
using Stedders.Utilities;
using System.Numerics;
using static Stedders.Utilities.SceneManager;

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


            state.GuiOpen = false;
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
            RayGui.GuiSliderBar(new Rectangle(x, y, 100, 20), string.Empty, "Throttle", Math.Abs(speed), 0, player.MaxThrottle);

            y += spread;
            var equippedItems = player.Inventory.OrderBy(x => x.Button);
            foreach (var item in equippedItems)
            {
                RayGui.GuiSliderBar(new Rectangle(x, y, 100, 20), string.Empty, item.Name, item.Ammo, 0, item.MaxAmmo);
                y += spread;
            }

            var dayRect = new Rectangle(x, y, 50, 30);
            RayGui.GuiLabel(dayRect, $"Day: {state.Day} Time: {state.CurrentTime.ToString("#")}");
            y += spread;

            //RayGui.GuiLabel(dayRect with { y = dayRect.y + spread + 2 }, $"Biomass: {player.BioMassContainer.ToString("0.#")}/{player.MaxBioMassContainer.ToString("0")}");
            RayGui.GuiLabel(dayRect with { y = dayRect.y + spread * 2 + 2 }, $"{state.Currency.ToString("C")}");

            // Barn stuff
            var topButton = new Rectangle(Raylib.GetScreenWidth() / 2 - 100, 20, 200, 60);

            var barn = Engine.Entities.Where(x => x.HasTypes(typeof(Barn))).FirstOrDefault();
            var playerSprite = playerMech.GetComponents<Render>().FirstOrDefault(x => x.MechPiece == MechPieces.Legs);
            if (barn is not null)
            {
                var barnComponent = barn.GetComponent<Barn>();
                var barnPos = barn.GetComponent<Render>().Position;
                var posDiff = barnPos - playerSprite.Position;
                var distance = posDiff.Length();
                if (distance < barnComponent.Range)
                {
                    if (RayGui.GuiButton(topButton, TranslationManager.GetTranslation(barnComponent.IsOpen ? "close" : "open")))
                    {
                        barnComponent.IsOpen = !barnComponent.IsOpen;
                        state.GuiOpen = barnComponent.IsOpen;
                    }
                }
                else
                {
                    barnComponent.IsOpen = false;
                    state.GuiOpen = false;
                }

                if (barnComponent.IsOpen)
                {
                    state.GuiOpen = true;
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
                        var itemTexture = TextureManager.Instance.GetTexture(item.IconKey);
                        Raylib.DrawTexturePro(itemTexture,
                            new Rectangle(0, 0, itemTexture.width, itemTexture.height),
                            itemRect with { y = itemRect.y + 10, width = 50, height = 50 }, Vector2.Zero, 0f, Raylib.WHITE);
                        var buttonRect = itemRect with { x = itemRect.x + 55, y = itemRect.y + 10, width = width - 170 };
                        var itemText = item.Name;
                        if (item.CanReload)
                        {
                            itemText += $" {item.Ammo}/{item.MaxAmmo}";
                        }
                        if (RayGui.GuiButton(buttonRect, itemText))
                        {
                            var currentLeft = player.Inventory.FirstOrDefault(x => x.Button == MouseButton.MOUSE_BUTTON_LEFT);
                            if (currentLeft is not null)
                            {
                                inventorytoAdd.Add(currentLeft);
                                player.Inventory.Remove(currentLeft);
                            }
                            item.Button = MouseButton.MOUSE_BUTTON_LEFT;
                            player.Inventory.Add(item);
                            inventoryToRemove.Add(item);
                        }
                        if (item.CanReload)
                        {
                            if (item.Ammo == item.MaxAmmo)
                            {
                                RayGui.GuiDisable();
                            }
                            if (RayGui.GuiButton(buttonRect with { x = buttonRect.x + buttonRect.width + 5, width = 90 }, "Reload"))
                            {
                                while (item.Ammo < item.MaxAmmo && state.Currency >= item.CostPerShot)
                                {
                                    state.Currency -= item.CostPerShot;
                                    item.Ammo++;
                                    Engine.Singleton.GetComponent<GameState>().Stats.MoneySpent += item.CostPerShot;
                                }
                            }
                            RayGui.GuiEnable();
                        }
                        var mousePos = Raylib.GetMousePosition();
                        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT))
                        {
                            if (Raylib.CheckCollisionPointRec(mousePos, buttonRect))
                            {
                                var currentRight = ResourceManager.Instance.PlayerInventory.FirstOrDefault(x => x.Button == MouseButton.MOUSE_BUTTON_RIGHT);
                                if (currentRight is not null)
                                {
                                    inventorytoAdd.Add(currentRight);
                                    player.Inventory.Remove(currentRight);
                                }
                                item.Button = MouseButton.MOUSE_BUTTON_RIGHT;
                                player.Inventory.Add(item);
                                inventoryToRemove.Add(item);
                            }
                        }
                        index++;
                    }
                    inventorytoAdd.ForEach(barnComponent.Equipment.Add);
                    inventoryToRemove.ForEach(x => barnComponent.Equipment.Remove(x));
                    RayGui.GuiLabel(new Rectangle(container.x, container.height - 10, 500, 50), TranslationManager.GetTranslation("equip-help"));
                }
            }
            var silo = Engine.Entities.Where(x => x.HasTypes(typeof(Silo))).FirstOrDefault();
            if (silo is not null)
            {
                var siloComponent = silo.GetComponent<Silo>();
                var siloPos = silo.GetComponent<Render>().Position;
                var posDiff = siloPos - playerSprite.Position;
                var distance = posDiff.Length();
                if (distance < siloComponent.Range)
                {
                    if (RayGui.GuiButton(topButton, TranslationManager.GetTranslation("sell-biomass")))
                    {
                        var harvester = player.Inventory.FirstOrDefault(x => x.Name == "Harvester");
                        if (harvester is not null)
                        {
                            siloComponent.BioMass += harvester.Ammo;
                            state.Currency += harvester.Ammo * 10;
                            Engine.Singleton.GetComponent<GameState>().Stats.MoneyEarned += harvester.Ammo * 10;
                            Engine.Singleton.GetComponent<GameState>().Stats.MostMoney = Math.Max(Engine.Singleton.GetComponent<GameState>().Stats.MostMoney, state.Currency);

                            harvester.Ammo = 0f;
                        }
                    }
                }

            }
        }
    }
}
