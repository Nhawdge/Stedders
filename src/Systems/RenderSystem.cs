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
            this.BackgroundTexture = Raylib.LoadTexture("Assets/background.png");
        }

        public override void Update()
        {
            Raylib.DrawTextureEx(BackgroundTexture, Vector2.Zero, 0f,100, Raylib.WHITE);
            foreach (var entity in Engine.Entities.OrderBy(x => x.GetComponent<Sprite>().Position.Y))
            {
                //Console.WriteLine("Rendered" + entity.ShortId());
                var myRenders = entity.GetComponents<Sprite>();
                foreach (var myRender in myRenders)
                    Raylib.DrawTexturePro(myRender.Texture, myRender.Source, myRender.Destination, myRender.Origin, myRender.Rotation, myRender.Color);

                //Raylib.DrawRectangleLines((int)myRender.Position.X,(int) myRender.Position.Y,(int) myRender.Destination.width, (int)myRender.Destination.height,Raylib.BLACK);
                //Raylib.DrawCircleLines((int)(myRender.Position.X + myRender.Origin.X),(int) (myRender.Position.Y + myRender.Origin.Y), 10f, Raylib.BLACK);

                // Health bar
                //var myHealth = entity.GetComponent<Unit>();
                //if (myHealth is not null)
                //{
                //    var healthBarWidth = myRender.Destination.width * .9f;
                //    var healthBarHeight = 15;
                //    var healthBarPositionWidth = myRender.Position.X + myRender.Destination.width / 2 - healthBarWidth / 2;
                //    var healthBarPositionHeight = myRender.Position.Y - healthBarHeight - 10;
                //    var healthBarRect = new Rectangle(healthBarPositionWidth, healthBarPositionHeight, healthBarWidth, healthBarHeight);
                //    Raylib.DrawRectangleRec(healthBarRect, Raylib.RED);

                //    var healthBarFillWidth = (int)(healthBarWidth * (myHealth.Health / myHealth.MaxHealth));
                //    var healthBarFillRect = new Rectangle(healthBarPositionWidth, healthBarPositionHeight, healthBarFillWidth, healthBarHeight);
                //    Raylib.DrawRectangleRec(healthBarFillRect, Raylib.GREEN);
                //}


            }
        }
    }
}
