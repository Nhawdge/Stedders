using Raylib_CsLo;
using Stedders.Components;

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

                var player = playerMech.GetComponent<Player>();
                var speed = player.Throttle;

                RayGui.GuiDummyRec(new Rectangle(5, 5, 210, 100), "");

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
            }
        }
    }
}
