﻿using Stedders.Components;
using Stedders.Entities;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        private BaseScene NewLand()
        {
            var mainGame = new BaseScene();
            mainGame.KeyboardMapping = GetBaseKeyboardMap();
            mainGame.MouseMapping = GetBaseMouseMap();
            var map = new Entity();
            var buildMap = MapManager.Instance.GetMap("NewLand");
            mainGame.WorldId = buildMap.Id;
            map.Components.Add(buildMap);
            mainGame.MapEdge = buildMap.MapEdges;
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

    }
}
