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
        public Vector2 MapEdges { get; internal set; }
        public long Scale { get; internal set; }
    }

    internal class MapCell
    {
        public MapPartsKey Key;
        public Tilemap Tilemap;

        public MapCell(TextureKey key, Vector2 dest, long x = 0, long y = 0)
        {
            Tilemap = new Tilemap(key, x, y, 3, false)
            {
                Position = dest
            };
        }
    }
}
