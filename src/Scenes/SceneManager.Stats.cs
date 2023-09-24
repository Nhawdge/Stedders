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
        private BaseScene Stats ()
        {
            var scene = new BaseScene();
            //state.GuiOpen = true;
            var boxWidth = 650;
            var boxHeight = 700;
            var centerPane = new Rectangle(Raylib.GetScreenWidth() / 2 - boxWidth / 2, Raylib.GetScreenHeight() / 2 - boxHeight / 2, boxWidth, boxHeight);
            RayGui.GuiDummyRec(centerPane, "");

            var textPane = centerPane with { x = centerPane.x + 15, height = centerPane.height - 150 };
            var statsText = $"Stats\n";
            //statsText += $"Enemies Killed: {state.Stats.TotalEnemiesKilled}";
            //statsText += $"\nMoney Earned: {state.Stats.MoneyEarned.ToString("C")}";
            //statsText += $"\nMoney Spent: {state.Stats.MoneySpent.ToString("C")}";
            //statsText += $"\nMost Money: {state.Stats.MostMoney.ToString("C")}";
            //statsText += $"\nHighest Day: {state.Stats.LongestDay}";
            //statsText += $"\nBiomass Harvested: {state.Stats.BiomassHarvested.ToString("0")}";
            //statsText += $"\nBiomass Eaten: {state.Stats.BiomassEaten.ToString("0")}";

            RayGui.GuiLabel(textPane, statsText);

            var backRect = new Rectangle(centerPane.x + centerPane.width / 2 - 100, centerPane.y + centerPane.height - 100, 200, 60);
            if (RayGui.GuiButton(backRect, TranslationManager.GetTranslation("back")))
            {
                // TODO
                //state.State = state.LastState;
            }
            return scene;
        }

    }
}
