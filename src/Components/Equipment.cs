using Raylib_CsLo;

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

    }
}
