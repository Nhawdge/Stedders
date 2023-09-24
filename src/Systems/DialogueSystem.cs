using Raylib_CsLo;
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

            var dialogues = Engine.Entities.Where(x => x.HasTypes(typeof(Dialogue)));

            var first = dialogues.FirstOrDefault();
            if ( first is null)
            {
                return;
            }
            var firstDialogue = first.GetComponent<Dialogue>();

            RayGui.GuiDummyRec(new Rectangle(10, Raylib.GetScreenHeight() - 210, Raylib.GetScreenWidth() - 20, 200), "");
            var personTexture = TextureManager.Instance.GetTexture(TextureKey.Person1);
            Raylib.DrawTexturePro(personTexture, new Rectangle(0, 0, personTexture.width, personTexture.height),
                new Rectangle(10, Raylib.GetScreenHeight() - 210, 200, 200), Vector2.Zero, 0f, Raylib.WHITE);

            var text = TranslationManager.GetTranslation($"{firstDialogue.DialogueKey}");
            var textSplit = text.Split("|");
            if (textSplit.Length > firstDialogue.DialoguePhase)
            {
                text = textSplit[firstDialogue.DialoguePhase];
            }
            else
            {
                //state.State = state.NextState;
            }

            var rect = new Rectangle(220, Raylib.GetScreenHeight() - 190, Raylib.GetScreenWidth() - 230 - 15, 150);

            RayGui.GuiLabel(rect, text);

            var nextClicked = RayGui.GuiButton(
                new Rectangle(Raylib.GetScreenWidth() - 230,
                                Raylib.GetScreenHeight() - 60,
                                190,
                                40),
                               TranslationManager.GetTranslation("next"));
            if (nextClicked)
            {
                firstDialogue.DialoguePhase++;
            }
        }
    }
}
