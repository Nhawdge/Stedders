using Raylib_CsLo;

namespace Stedders.Utilities
{
    public class SoundManager
    {
        internal static SoundManager Instance { get; } = new();
        private SoundManager() { }
        public Dictionary<SoundKey, Sound> SoundStore { get; set; } = new();
        public Dictionary<MusicKey, Music> MusicStore { get; set; } = new();

        public void LoadSounds()
        {
            SoundStore.Add(SoundKey.FlowerGrowth, Raylib.LoadSound("Assets/Sound/Stedders_Flower_Growing.wav"));
            SoundStore.Add(SoundKey.Laser, Raylib.LoadSound("Assets/Sound/Laser.wav"));

            SoundStore.Add(SoundKey.Enemy1Death1, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Death_V1.wav"));
            SoundStore.Add(SoundKey.Enemy1Death2, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Death_V2.wav"));
            SoundStore.Add(SoundKey.Enemy1Death3, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Death_V3.wav"));
            SoundStore.Add(SoundKey.Enemy1Eating1, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Eating_Plant.wav"));
            SoundStore.Add(SoundKey.Enemy1Eating2, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Eating_Plant_2.wav"));
            SoundStore.Add(SoundKey.Enemy1Eating3, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Eating_Plant_3.wav"));
            SoundStore.Add(SoundKey.Enemy1Spawn1, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Spawn_V1.wav"));
            SoundStore.Add(SoundKey.Enemy1Spawn2, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Spawn_V2.wav"));
            SoundStore.Add(SoundKey.Enemy1Spawn3, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Spawn_V3.wav"));
            SoundStore.Add(SoundKey.Enemy1Move, Raylib.LoadSound("Assets/Sound/Enemy/Stedders_Enemy_Walking.wav"));

            SoundStore.Add(SoundKey.Mech2Walking, Raylib.LoadSound("Assets/Sound/Mech/Stedders_Mech_Walking_V2.wav"));
            SoundStore.Add(SoundKey.Mech2EngineIdle, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Engine_Idle.wav"));
            SoundStore.Add(SoundKey.Mech2EngineStop, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Engine_Power_Down.wav"));
            SoundStore.Add(SoundKey.Mech2EngineStart, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Engine_Power_On.wav"));
            SoundStore.Add(SoundKey.Harvester1, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Harvest_V1.wav"));
            SoundStore.Add(SoundKey.Harvester2, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Harvest_V2.wav"));
            SoundStore.Add(SoundKey.Harvester3, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Harvest_V3.wav"));
            SoundStore.Add(SoundKey.Seeder1, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Planting_Seed_V1.wav"));
            SoundStore.Add(SoundKey.Seeder2, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Planting_Seed_V2.wav"));
            SoundStore.Add(SoundKey.Seeder3, Raylib.LoadSound("Assets/Sound/Mech/Stedders_SD_Mech_Planting_Seed_V3.wav"));

            SoundStore.Add(SoundKey.UiHover, Raylib.LoadSound("Assets/Sound/UI/UI_Hover_V1.wav"));
            SoundStore.Add(SoundKey.UiClick, Raylib.LoadSound("Assets/Sound/UI/UI_Press_V1.wav"));

            // Music

            MusicStore.Add(MusicKey.Ambiance, Raylib.LoadMusicStream("Assets/Sound/Stedders_Day_Map_Ambience.wav"));
            MusicStore.Add(MusicKey.Menu, Raylib.LoadMusicStream("Assets/Sound/Stedders_Menu_Music.wav"));
            MusicStore.Add(MusicKey.GamePlay, Raylib.LoadMusicStream("Assets/Sound/Stedders_Gameplay_Music.wav"));
        }

        public Sound GetSound(SoundKey key)
        {
            if (SoundStore.Count <= 0)
            {
                LoadSounds();
            }
            return SoundStore[key];
        }
        public Music GetMusic(MusicKey key)
        {
            if (MusicStore.Count <= 0)
            {
                LoadSounds();
            }
            return MusicStore[key];
        }
    }

    public enum SoundKey
    {
        FlowerGrowth,
        Laser,

        Enemy1Death1,
        Enemy1Death2,
        Enemy1Death3,
        Enemy1Eating1,
        Enemy1Eating2,
        Enemy1Eating3,
        Enemy1Spawn1,
        Enemy1Spawn2,
        Enemy1Spawn3,
        Enemy1Move,

        Mech2Walking,
        Mech2EngineIdle,
        Mech2EngineStart,
        Mech2EngineStop,

        Harvester1,
        Harvester2,
        Harvester3,

        Seeder1,
        Seeder2,
        Seeder3,

        UiHover,
        UiClick,
    }

    public enum MusicKey // probably C# (Get it??)
    {
        Ambiance,
        Menu,
        GamePlay,
    }
}