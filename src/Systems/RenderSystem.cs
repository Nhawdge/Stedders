using Raylib_CsLo;
using Stedders.Components;
using System.Numerics;

namespace Stedders.Systems
{
    public class RenderSystem : GameSystem
    {
        public Texture BackgroundTexture { get; private set; }
        public RenderSystem(GameEngine gameEngine) : base(gameEngine)
        {
            this.BackgroundTexture = Raylib.LoadTexture("Assets/FreestyleRanchBackground.png");
        }

        public override void Update()
        {
            Raylib.DrawTextureEx(BackgroundTexture, Vector2.Zero, 0f, 3, Raylib.WHITE);
            foreach (var entity in Engine.Entities.Where(x => x.HasTypes(typeof(Render)))
                .OrderBy(x => x.GetComponent<Render>().ZIndex))
            {
                var myRenders = entity.GetComponents<Render>();
                foreach (var myRender in myRenders)
                {
                    Raylib.DrawTexturePro(myRender.Texture, myRender.Source, myRender.Destination, myRender.Origin, myRender.RenderRotation, myRender.Color);
                    //Raylib.DrawRectangleLines((int)myRender.Destination.X, (int)myRender.Destination.Y, (int)myRender.Destination.width, (int)myRender.Destination.height, Raylib.BLACK);
                }
            }
            //foreach (var entity in Engine.Entities.Where(x => x.HasTypes(typeof(Sprite)))
            //    .OrderBy(x => x.GetComponent<Render>().Position.Y))
            //{
            //    var myRenders = entity.GetComponents<Render>();
            //    foreach (var myRender in myRenders)
            //        Raylib.DrawTexturePro(myRender.Texture, myRender.Source, myRender.Destination, myRender.Origin, myRender.RenderRotation, myRender.Color);
            //}
        }
    }
}
