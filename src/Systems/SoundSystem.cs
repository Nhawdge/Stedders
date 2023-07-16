using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;

namespace Stedders.Systems
{
    internal class SoundSystem : GameSystem
    {
        public SoundSystem(GameEngine gameEngine) : base(gameEngine)
        {
            var state = Engine.Singleton.GetComponent<GameState>();

            state.CurrentMusic.Add(new MusicPlayer(MusicKey.Menu, Engine.SoundManager.GetMusic(MusicKey.Menu), 1f));
            state.CurrentMusic.Add(new MusicPlayer(MusicKey.Ambiance, Engine.SoundManager.GetMusic(MusicKey.Ambiance), 1f));
            state.CurrentMusic.Add(new MusicPlayer(MusicKey.GamePlay, Engine.SoundManager.GetMusic(MusicKey.GamePlay), 1f));
        }

        public override void Update()
        {

            var state = Engine.Singleton.GetComponent<GameState>();
            Raylib.SetMasterVolume(state.MainVolume);
            if (!state.MusicSetSinceStateChange)
            {
                foreach (var music in state.CurrentMusic)
                {
                    music.IsPlaying = false;
                    Raylib.StopMusicStream(music.Music);
                }
                state.MusicSetSinceStateChange = true;
                if (state.State is not States.Game)
                {
                    state.CurrentMusic.First(x => x.Key == MusicKey.Menu).IsPlaying = true;
                }
                else if (state.State == States.Game)
                {
                    state.CurrentMusic.First(x => x.Key == MusicKey.GamePlay).IsPlaying = true;
                    state.CurrentMusic.First(x => x.Key == MusicKey.Ambiance).IsPlaying = true;
                }
                foreach (var music in state.CurrentMusic)
                {
                    if (music.IsPlaying)
                    {
                        Raylib.PlayMusicStream(music.Music);
                        music.IsPlaying = true;
                        Raylib.SetMusicVolume(music.Music, music.Volume);
                    }
                }
            }

            foreach (var music in state.CurrentMusic)
            {
                if (music.IsPlaying)
                {
                    Raylib.UpdateMusicStream(music.Music);
                }
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
