using Raylib_CsLo;
using Stedders.Utilities;
using System.Diagnostics;

namespace Stedders.Components
{
    public class GameState : Component
    {
        private States _state;
        public States State
        {
            get => this._state;
            set
            {
                this.LastState = _state;
                this.MusicSetSinceStateChange = false;
                this._state = value;
            }
        }
        public States LastState = States.MainMenu;
        public TimeOfDay TimeOfDay = TimeOfDay.Day;
        public float CurrentTime = 0f;
        public float DayDuration = 300f;
        public int Day = 0;

        public float TimeSinceLastSpawn = 0f;
        public float SpawnInterval = 15f;

        public float Currency = 0f;

        public (string, int) DialoguePhase { get; set; }

        public HashSet<MusicPlayer> CurrentMusic { get; set; } = new();
        public float MainVolume { get; set; } = 0.5f;

        public bool MusicSetSinceStateChange = false;

        public float IntroAnimationTiming = 0f;
    }

    public enum TimeOfDay
    {
        Dawn,
        Day,
        Dusk,
        Night
    }

    public enum States
    {
        MainMenu,
        Start,
        Dialogue,
        Pause,
        Credits,
        HowTo,
        Options,
        Game,
        GameOver
    }

    public record MusicPlayer(MusicKey Key, Music Music, float Volume)
    {
        public bool IsPlaying { get; set; } = false;
    };

}