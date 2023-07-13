using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;

namespace Stedders.Systems
{
    internal class SoundSystem : GameSystem
    {
        public SoundSystem(GameEngine gameEngine) : base(gameEngine) { }

        public override void Update()
        {
            Raylib.SetMasterVolume(1f);
            Engine.Singleton.CurrentMusic = Engine.SoundManager.GetMusic(MusicKey.Ambiance);
            if (!Engine.Singleton.MusicIsPlaying)
            {
                Raylib.PlayMusicStream(Engine.Singleton.CurrentMusic);
                Engine.Singleton.MusicIsPlaying = true;
                Raylib.SetMusicVolume(Engine.Singleton.CurrentMusic, 1f);
            }
            else
            {
                Raylib.UpdateMusicStream(Engine.Singleton.CurrentMusic);
            }

            var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(SoundAction)));
            foreach (var entity in allEntities)
            {
                var soundsToRemove = new List<SoundAction>();
                var soundsActions = entity.GetComponents<SoundAction>();
                foreach (var soundAction in soundsActions)
                {
                    var sound = Engine.SoundManager.GetSound(soundAction.SoundKey);
                    Raylib.SetSoundVolume(sound, 0.25f);

                    Raylib.PlaySoundMulti(Engine.SoundManager.GetSound(soundAction.SoundKey));

                    soundAction.IsPlaying = true;
                    soundsToRemove.Add(soundAction);
                }
                soundsToRemove.ForEach(x => entity.Components.Remove(x));
            }
        }
    }
}
