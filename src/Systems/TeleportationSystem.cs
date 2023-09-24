using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;

namespace Stedders.Systems
{
    internal class TeleportationSystem : GameSystem
    {
        public TeleportationSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }
        public override void Update() { }

        public override void UpdateNoCamera()
        {
            var player = Engine.Entities.Where(x => x.HasTypes(typeof(Player))).FirstOrDefault();
            var teleporters = Engine.Entities.Where(x => x.HasTypes(typeof(Teleporter)));

            if (player == null || !teleporters.Any())
            {
                return;
            }


            var playerPos = player.GetComponent<Sprite>();

            var nearest = teleporters.OrderBy(x =>
            {
                return (x.GetComponent<Teleporter>().Position - playerPos.Position).Length();
            }).FirstOrDefault();

            if (nearest != null)
            {
                var distance = (nearest.GetComponent<Teleporter>().Position - playerPos.Position).Length();
                if (distance < 50)
                {
                    var teleporter = nearest.GetComponent<Teleporter>();
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_F))
                    {
                        SceneManager.Instance.ChangeScene(teleporter.DestinationSceneName);
                    }
                    var state = Engine.Singleton.GetComponent<GameState>();

                    var rightSide = new Rectangle(Raylib.GetScreenWidth() - 200, 0, 200, Raylib.GetScreenHeight() + 300);
                    RayGui.GuiLabel(rightSide, "F to enter");
                    //state.Scene = teleporter.Scene;
                    //state.PlayerPosition = teleporter.Destination;
                    //Engine.Entities.Remove(player);
                }
            }

        }
    }
}
