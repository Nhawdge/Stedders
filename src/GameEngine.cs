using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using Stedders.Systems;
using Stedders.Utilities;

namespace Stedders
{
    public class GameEngine
    {
        public List<Entity> Entities { get; set; } = new();
        public List<GameSystem> Systems { get; set; } = new();
        public Camera2D Camera;
        public Entity Singleton = new();
        public TextureManager TextureManager = new();
        public SoundManager SoundManager = new();

        public void RunGame()
        {
            Raylib.InitWindow(1380, 768, "Stedders");
            //Raylib.SetWindowPosition(-1652, 110);
            Raylib.SetWindowPosition(200, 110);
            Raylib.SetTargetFPS(60);
            Raylib.InitAudioDevice();
            Raylib.SetExitKey(0);

            Load();

            Camera = new Camera2D();
            Camera.zoom = 1f;

            while (!Raylib.WindowShouldClose())
            {
                GameLoop();
            }
        }

        public void Load()
        {
            Systems = new List<GameSystem>
            {
                new RenderSystem(this),
                new InputSystem(this),
                new GenerationSystem(this),
                new MenuSystem(this),
                new UnitCreationSystem(this),
                new AiSystem(this),
                new UnitSystem(this),
                new HealthSystem(this),
                new CameraSystem(this),
                new MechMovementSystem(this),
                new MechUISystem(this),
                new EquipmentSystem(this),
                new MunitionsSystem(this),
                new PlantGrowthSystem(this),
                new TimeKeeperSystem(this),
                new DialogueSystem(this),
                new SoundSystem(this),
            };

            Singleton.Components.Add(new GameState());
            Entities.Add(Singleton);
            RayGui.GuiLoadStyle("Assets/cyber.rgs");
        }

        public void GameLoop()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode2D(Camera);
            Raylib.ClearBackground(Raylib.WHITE);
            foreach (var system in Systems)
            {
                system.Update();
            }
            Raylib.EndMode2D();
            foreach (var system in Systems)
            {
                system.UpdateNoCamera();
            }
            Raylib.EndDrawing();
        }
    }
}