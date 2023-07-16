﻿using Stedders.Components;

namespace Stedders.Systems
{
    internal class SceneConditionSystem : GameSystem
    {
        public SceneConditionSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Game)
            {
                var allBuildings = Engine.Entities.Where(x => x.HasTypes(typeof(Building)));
                if (allBuildings.Count() == 0)
                {
                    state.DialoguePhase = ("lose", 1);
                    state.State = States.Dialogue;
                    state.NextState = States.GameOver;
                }
            }
        }
    }
}
