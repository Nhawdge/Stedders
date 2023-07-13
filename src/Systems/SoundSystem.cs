using Raylib_CsLo;
using Stedders.Components;

namespace Stedders.Systems
{
    internal class SoundSystem : GameSystem
    {
        public SoundSystem(GameEngine gameEngine) : base(gameEngine) { }

        public override void Update()
        {
            Raylib.SetMasterVolume(1f);
            var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(SoundAction)));
            foreach (var entity in allEntities)
            {
                var soundsToRemove = new List<SoundAction>();
                var soundsActions = entity.GetComponents<SoundAction>();
                foreach (var soundAction in soundsActions)
                {
                    var sound = Engine.SoundManager.GetMusic(soundAction.SoundKey);
                    if (soundAction.IsPlaying)
                    {
                        Raylib.UpdateMusicStream(sound);
                        continue;
                    }
                    Raylib.SetMusicVolume(sound, 1f);
                    Raylib.PlayMusicStream(sound);

                    //Raylib.PlaySound(Engine.SoundManager.GetSound(soundAction.SoundKey));

                    soundAction.IsPlaying = true;
                }
                soundsToRemove.ForEach(x => entity.Components.Remove(x));
            }
        }
    }
}
