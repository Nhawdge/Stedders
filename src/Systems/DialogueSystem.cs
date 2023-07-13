﻿using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Systems
{
    internal class DialogueSystem : GameSystem
    {
        public DialogueSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            return;
        }
        public override void UpdateNoCamera()
        {
            var state = Engine.Singleton.GetComponent<GameState>();
            if (state.State == States.Dialogue)
            {
                //RayGui.GuiLabel(new Rectangle(200, 200, 200, 200), "Dialogue");
                RayGui.GuiDummyRec(new Rectangle(10, Raylib.GetScreenHeight() - 210, Raylib.GetScreenWidth() - 20, 200), "");
                var personTexture = Engine.TextureManager.GetTexture(TextureKey.Person1);
                Raylib.DrawTexturePro(personTexture, new Rectangle(0, 0, personTexture.width, personTexture.height),
                    new Rectangle(10, Raylib.GetScreenHeight() - 210, 200, 200), Vector2.Zero, 0f, Raylib.WHITE);


                RayGui.GuiLabel(
                    new Rectangle(220, Raylib.GetScreenHeight() - 190, Raylib.GetScreenWidth() - 230, 150),
                    TranslationManager.GetTranslation("intro-1"));

                var nextClicked = RayGui.GuiButton(new Rectangle(Raylib.GetScreenWidth() - 200, Raylib.GetScreenHeight() - 50, 190, 40),
                                   TranslationManager.GetTranslation("next"));

                if (nextClicked)
                {
                    state.State = States.Game;
                }
            }
        }
    }
}