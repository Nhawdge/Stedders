using Raylib_CsLo;
using Stedders.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Stedders.Systems
{
    internal class MotionSystem : GameSystem
    {
        public MotionSystem(GameEngine gameEngine) : base(gameEngine)
        {
        }

        public override void Update()
        {
            var state = Engine.Singleton.GetComponent<GameState>();

            var allEntities = Engine.Entities.Where(x => x.HasTypes(typeof(Render), typeof(Motion)));
            foreach (var entity in allEntities)
            {
                var motion = entity.GetComponent<Motion>();
                var pos = entity.GetComponent<Render>();
                var diff = motion.Target - pos.Position;

                var length = diff.Length();
                if (length < motion.Speed * Raylib.GetFrameTime())
                {
                    pos.Position = motion.Target;
                }
                if (length < 1)
                {
                    motion.OnTarget();
                }
                else
                {
                    var direction = Math.Atan2(diff.Y, diff.X);
                    pos.Position.X += (float)(Math.Cos(direction) * motion.Speed * Raylib.GetFrameTime());
                    pos.Position.Y += (float)(Math.Sin(direction) * motion.Speed * Raylib.GetFrameTime());
                }

            }
        }
    }
}
