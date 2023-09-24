using Raylib_CsLo;
using Stedders.Components;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Systems
{
    public class MenuSystem : GameSystem
    {
        public Font RobotoFont = Raylib.LoadFont("Assets/Roboto.ttf");
        public MenuSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }
        public override void Update() { }

        public override void UpdateNoCamera()
        {
#if DEBUG
            Raylib.DrawText(Raylib.GetFPS().ToString(), Raylib.GetScreenWidth() - 50, 20, 20, Raylib.WHITE);
#endif
            var state = Engine.Singleton.GetComponent<GameState>();
            var animationTime = 1;
            if (state.IntroAnimationTiming < animationTime)
            {
                state.IntroAnimationTiming += Raylib.GetFrameTime();
                RayGui.GuiFade((float)EasingHelpers.easeInSine(state.IntroAnimationTiming / animationTime));
            }
            else
            {
                RayGui.GuiFade(1f);
            }

            foreach (var entity in Engine.Entities.Where(x => x.HasTypes(typeof(UiTitle))))
            {
                var buttonWidth = 200;
                var buttonHeight = 60;

                var buttonPadding = 10;

                var centerOfScreen = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);

                var count = entity.Components.Where(x => x is UiTitle).Count();

                var container = new Rectangle(
                    centerOfScreen.X - (buttonWidth / 2) - buttonPadding,
                    centerOfScreen.Y - (buttonHeight * count) / 2 + (buttonPadding * 2),
                    buttonWidth + (buttonPadding * 2),
                    (buttonHeight * (count + 1)) + (buttonPadding * 2)
                );

                var buttonPos = container with { x = container.x + buttonPadding, y = container.y + buttonPadding, width = buttonWidth, height = buttonHeight };

                RayGui.GuiDummyRec(container, "");

                var index = 0;

                foreach (var component in entity.Components.Where(x => x is UiTitle))
                {
                    if (component is UiButton button)
                    {
                        if (RayGui.GuiButton(buttonPos, button.Text))
                        {
                            button.OnClick();
                        }
                    }
                    else if (component is UiTitle title)
                    {
                        RayGui.GuiLabel(buttonPos, title.Text);
                    }
                    //RayGui.GuiDummyRec(buttonPos, "");
                    buttonPos = buttonPos with { y = buttonPos.y + (buttonHeight + buttonPadding) };
                    index++;
                }
            }
        }
    }
}