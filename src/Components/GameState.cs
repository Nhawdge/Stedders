namespace Stedders.Components
{
    public class GameState : Component
    {
        public States State;
        public TimeOfDay TimeOfDay = TimeOfDay.Day;
        public float CurrentTime = 0f;
        public float DayDuration = 60f;
        public float NightDuration = 60f;
        public int Day = 0;

        public float TimeSinceLastSpawn = 0f;
        public float SpawnInterval = 15f;

        public float Currency = 0f;
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
        Game,
        GameOver
    }
}