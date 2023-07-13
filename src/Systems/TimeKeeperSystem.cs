using Raylib_CsLo;
using Stedders.Components;

namespace Stedders.Systems
{
    internal class TimeKeeperSystem : GameSystem
    {
        public TimeKeeperSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update() { }
        public override void UpdateNoCamera()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                state.CurrentTime += Raylib.GetFrameTime();
                if (state.TimeOfDay == TimeOfDay.Day)
                {
                    if (state.CurrentTime > state.DayDuration)
                    {
                        state.TimeOfDay = TimeOfDay.Night;
                        state.CurrentTime = 0;
                    }
                }
                else
                {
                    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, 128));
                    if (state.CurrentTime > state.NightDuration)
                    {
                        state.TimeOfDay = TimeOfDay.Day;
                        state.CurrentTime = 0;
                        state.Day++;
                    }
                }
            }
        }
    }
}
