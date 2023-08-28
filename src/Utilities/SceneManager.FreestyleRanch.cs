using Stedders.Components;
using Stedders.Entities;
using System.Numerics;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        private BaseScene FreestyleRanch()
        {
            var mainGame = new BaseScene();
            var map = new Entity();
            var buildMap = MapManager.Instance.GetMap("FreestyleRanch");
            map.Components.Add(buildMap);
            mainGame.Entities.Add(map);
            mainGame.Entities.AddRange(buildMap.EntitiesToAdd);

            var state = Engine.Singleton.GetComponent<GameState>();

            state.Currency = 0;
            state.Day = 0;
            state.CurrentTime = 0;

            //mainGame.Entities.Add(ArchetypeGenerator.GeneratePlayerMech(new Vector2(100, 400)));

            //todo fix this
            //state.DialoguePhase = ("intro", 0);
            state.NextState = States.Game;

            return mainGame;
        }

        private BaseScene FreestyleRanchBarn()
        {
            var scene = new BaseScene();
            var map = new Entity();
            var buildMap = MapManager.Instance.GetMap("FreestyleRanch_Barn");
            map.Components.Add(buildMap);
            scene.Entities.Add(map);
            scene.Entities.AddRange(buildMap.EntitiesToAdd);

            var state = Engine.Singleton.GetComponent<GameState>();

            state.Currency = 0;
            state.Day = 0;
            state.CurrentTime = 0;


            //todo fix this
            //state.DialoguePhase = ("intro", 0);
            state.NextState = States.Game;

            return scene;
        }

    }
}
