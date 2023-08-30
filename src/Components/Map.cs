using Stedders.Entities;
using Stedders.Utilities;
using System.Numerics;

namespace Stedders.Components
{
    internal class Map : Component
    {
        public Guid Id { get; set; }
        internal List<MapCell> Cells = new();
        internal List<Entity> EntitiesToAdd = new();
        internal string Name;

        public bool CanSpawnEnemies { get;  set; }
    }

    internal class MapCell
    {
        public MapPartsKey Key;
        public Tilemap Tilemap;

        public MapCell(TextureKey key, Vector2 dest, int x = 0, int y = 0)
        {
            Tilemap = new Tilemap(key, x, y, 3, false)
            {
                Position = dest
            };
        }
    }
}
