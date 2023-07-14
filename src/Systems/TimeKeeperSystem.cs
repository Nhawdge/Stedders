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

                var dawnEnd = state.DayDuration / 4;
                var noon = state.DayDuration / 2;
                var duskEnd = noon + dawnEnd;
                var shade = 0;
                if (state.CurrentTime > state.DayDuration)
                {
                    state.CurrentTime = 0;
                    state.Day++;
                }

                if (state.CurrentTime < dawnEnd)
                {
                    state.TimeOfDay = TimeOfDay.Dawn;
                    shade = 64;
                }
                else if (state.CurrentTime < noon)
                {
                    state.TimeOfDay = TimeOfDay.Day;
                    shade = 0;
                }
                else if (state.CurrentTime < duskEnd)
                {
                    shade = 64;
                    state.TimeOfDay = TimeOfDay.Dusk;
                }
                else
                {
                    shade = 128;
                    state.TimeOfDay = TimeOfDay.Night;
                }

                Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, shade));
            }
        }
    }
}
