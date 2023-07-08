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

        public void RunGame()
        {
            // Load systems

            Raylib.InitWindow(1380, 768, "Stedders");
            Raylib.SetTargetFPS(60);

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
            };

            Camera = new Camera2D();
            Camera.zoom = 0.1f;
            //Camera.target.X = -(Raylib.GetScreenWidth() / 2);
            //Camera.target.Y = -(Raylib.GetScreenHeight() / 2);

            Load();

            while (!Raylib.WindowShouldClose())
            {
                GameLoop();
            }

        }

        public void Load()
        {
            Singleton.Components.Add(new GameState());
            TextureManager = new TextureManager();
            TextureManager.LoadTextures();
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
            Raylib.EndMode3D();
            foreach (var system in Systems)
            {
                system.UpdateNoCamera();
            }
            Raylib.EndDrawing();
        }
    }
}