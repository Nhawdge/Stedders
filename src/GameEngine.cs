using Raylib_CsLo;
using Stedders.Components;
using Stedders.Entities;
using Stedders.Systems;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders
{
    public class GameEngine
    {
        public List<Entity> Entities
        {
            get
            {
                return ActiveScene.Entities;
            }
            set
            {
                ActiveScene.Entities = value;
            }
        }

        public List<GameSystem> Systems { get; set; } = new();
        public Camera2D Camera;
        public Entity Singleton = new();
        internal BaseScene ActiveScene { get; set; }

        

        public void RunGame()
        {
            Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);

            Raylib.InitWindow(1368, 768, "Stedders");
            //Raylib.SetWindowMonitor(0);

#if DEBUG
            Raylib.SetWindowPosition(-1652, 110);
#endif
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
            ActiveScene = SceneManager.Instance.LoadScene(SceneManager.SceneKey.Menu.MainMenu);
            Singleton.Components.Add(new GameState());
            Entities.Add(Singleton);
            RayGui.GuiLoadStyle("Assets/Other/cyber.rgs");
            RayGui.GuiSetFont(Raylib.LoadFont("Assets/Other/Roboto-Black.ttf"));


            Systems = new List<GameSystem>
            {
                new RenderSystem(this),
                new InputSystem(this),
                new TimeKeeperSystem(this),
                new GenerationSystem(this),
                new MenuSystem(this),
                new AiSystem(this),
                new HealthSystem(this),
                new CameraSystem(this),
                new MechMovementSystem(this),
                new MechUISystem(this),
                new EquipmentSystem(this),
                new MunitionsSystem(this),
                new PlantGrowthSystem(this),
                new DialogueSystem(this),
                new SoundSystem(this),
                new MotionSystem(this),
                new SceneConditionSystem(this),
                new TeleportationSystem(this),
            };
        }

        public void GameLoop()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode2D(Camera);
            Raylib.ClearBackground(Raylib.BLACK);
            for (int i = 0; i < Systems.Count; i++)
            {
                Systems[i].Update();
            }
            Raylib.EndMode2D();
            for (int i = 0; i < Systems.Count; i++)
            {
                Systems[i].UpdateNoCamera();
            }
            Raylib.EndDrawing();
        }

        public static GameEngine Instance { get; } = new();
    }
}