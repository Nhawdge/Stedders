using Stedders.Utilities;

namespace Stedders.Components
{
    internal class SoundAction : Component
    {
        public SoundAction(SoundKey soundKey, bool shouldstop = false)
        {
            SoundKey = soundKey;
            ShouldStop = shouldstop;
        }

        public SoundKey SoundKey { get; set; }
        public bool ShouldStop { get; set; }
        public bool IsPlaying { get; set; } = false;
    }
}
