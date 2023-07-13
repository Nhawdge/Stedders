using Stedders.Utilities;

namespace Stedders.Components
{
    internal class SoundAction : Component
    {
        public SoundAction(SoundKey soundKey, bool shouldLoop = false)
        {
            SoundKey = soundKey;
            ShouldLoop = shouldLoop;
        }

        public SoundKey SoundKey { get; set; }
        public bool ShouldLoop { get; set; }
        public bool IsPlaying { get; set; } = false;
    }
}
