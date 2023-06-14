namespace Stedders.Components
{
    public class GameState : Component
    {
        public States State;
    }

    public enum States
    {
        MainMenu,
        Start,
        Game,
        GameOver
    }
}