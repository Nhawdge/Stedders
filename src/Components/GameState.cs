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
    }

    public enum TimeOfDay
    {
        Day,
        Night
    }

    public enum States
    {
        MainMenu,
        Start,
        Game,
        GameOver
    }
}