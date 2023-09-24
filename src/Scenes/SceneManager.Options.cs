using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stedders.Utilities
{
    internal partial class SceneManager
    {
        private BaseScene Options()
        {
            var scene = new BaseScene();

            //state.GuiOpen = true;
            var boxWidth = 650;
            var boxHeight = 700;
            var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - boxWidth / 2, Raylib.GetScreenHeight() / 2 - boxHeight / 2, boxWidth, boxHeight);
            RayGui.GuiDummyRec(centerPane, "");

            var offSets = new List<int>();

            var text = TranslationManager.GetTranslation("volume");
            var text2 = TranslationManager.GetTranslation("musicvolume");
            var text3 = TranslationManager.GetTranslation("sfxvolume");
            offSets.Add(Raylib.MeasureText(text, RayGui.GuiGetFont().baseSize));
            offSets.Add(Raylib.MeasureText(text2, RayGui.GuiGetFont().baseSize));
            offSets.Add(Raylib.MeasureText(text3, RayGui.GuiGetFont().baseSize));

            var xOffset = offSets.Max();

            //state.MainVolume = RayGui.GuiSlider(
            //    centerPane with { x = centerPane.x + xOffset, y = centerPane.y / 2 + 50, height = 30, width = 400 },
            //    text, state.MainVolume.ToString("#0%"),
            //    state.MainVolume, 0, 1);

            //state.MusicVolume = RayGui.GuiSlider(
            //    centerPane with { x = centerPane.x + xOffset, y = centerPane.y / 2 + 50 * 2, height = 30, width = 400 },
            //    text2, state.MusicVolume.ToString("#0%"),
            //    state.MusicVolume, 0, 1);

            //state.SfxVolume = RayGui.GuiSlider(
            //    centerPane with { x = centerPane.x + xOffset, y = centerPane.y / 2 + 50 * 3, height = 30, width = 400 },
            //    text3, state.SfxVolume.ToString("#0%"),
            //    state.SfxVolume, 0, 1);

            ////var text = TranslationManager.GetTranslation("howto-full");

            //RayGui.GuiLabel(centerPane with { x = centerPane.x + 15, height = centerPane.height - 150 }, text);

            var backRect = new Rectangle(centerPane.x + centerPane.width / 2 - 100, centerPane.y + centerPane.height - 100, 200, 60);
            if (RayGui.GuiButton(backRect, TranslationManager.GetTranslation("back")))
            {
                //state.State = state.LastState;
            }
            return scene;
        }
    }

}