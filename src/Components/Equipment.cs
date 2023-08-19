using Raylib_CsLo;
using Stedders.Entities;
using Stedders.Utilities;

namespace Stedders.Components
{
    internal class Equipment : Component
    {
        public string Name { get; set; }
        public Render Sprite { get; set; }
        public KeyboardKey Key { get; set; }
        public MouseButton Button { get; set; }
        public float Ammo { get; set; } = 100;
        public float MaxAmmo { get; set; } = 100;
        public float Damage { get; set; } = 10;
        public float MaxRange { get; set; } = 150;
        public float Range { get; set; } = 1;
        public float Heat { get; set; }
        public float MaxHeat { get; set; }
        public float HeatPerShot { get; set; }
        public bool IsFiring { get; set; }
        public bool IsOverheated { get; set; }
        public float ShotCooldown { get; set; } = 0f;
        public float ShotCoolDownRate { get; set; } = 1f;
        public float CooldownPerShot { get; set; }
        public TextureKey IconKey { get; set; }
        public bool CanReload { get;  set; } = false;
        public float CostPerShot { get; set; } = 100;
        public SoundKey SoundKey { get; set; }

        public Action<Entity, Equipment> Idle = (_, _) => { };

        public Func<IEnumerable<Entity>, Entity, Equipment, (IEnumerable<Entity>, IEnumerable<Entity>)> Fire = (_, _, _) =>
        {
            return (Enumerable.Empty<Entity>(), Enumerable.Empty<Entity>());
        };

        // Saw blade - melee - free 
        // Laser - ranged - 
        // Rocket - ranged - 
        // Bomb - dropped
        // Flamethrower - ranged - "Ultimate"
    }
}
