using Stedders.Components;
using Stedders.Entities;
using System.Numerics;
using System.Text.Json;

namespace Stedders.Utilities
{
    internal class MapManager
    {
        public Dictionary<string, Map> MapStore { get; set; } = new();
        private GameEngine Engine { get; }

        internal static MapManager Instance { get; } = new();
        private MapManager()
        {
            Engine = GameEngine.Instance;
            LoadAllMaps();
        }

        public void LoadAllMaps()
        {
            var mapName = "LDTK/Stedders";
            // TODO Maybe optimize?
            var mapData = new MapData();
            var mapFile = File.ReadAllText($"Assets/Maps/{mapName}.ldtk");
            var data = JsonSerializer.Deserialize<MapData>(mapFile);

            foreach (var level in data.levels)
            {
                var cells = new List<MapCell>();
                var entities = new List<Entity>();
                foreach (var instance in level.layerInstances)
                {
                    if (Enum.TryParse<MapPartsKey>(instance.__identifier, true, out var identifier))
                    {
                        Console.WriteLine("Passed: " + identifier);

                        foreach (var tile in instance.gridTiles)
                        {
                            var pos = new Vector2(tile.px[0], tile.px[1]);
                            cells.Add(new MapCell(TextureKey.FreestyleRanchTileset, pos, tile.src[0], tile.src[1])
                            {
                                Key = identifier,
                            });
                        }
                        foreach (var entityInstance in instance.entityInstances)
                        {
                            var pos = new Vector2(entityInstance.px[0], entityInstance.px[1]);
                            if (entityInstance.__identifier == "Barn")
                            {
                                entities.Add(ArchetypeGenerator.GenerateBarn(pos * 3));
                            }
                            if (entityInstance.__identifier == "Silo")
                            {
                                entities.Add(ArchetypeGenerator.GenerateSilo(pos * 3));
                            }
                        }
                    }
                }
                cells = cells.OrderBy(x => x.Key).ToList();
                MapStore.Add(level.identifier, new Map()
                {
                    Name = level.identifier,
                    Cells = cells,
                    EntitiesToAdd = entities
                });
            }
        }

        public Map GetMap(string key)
        {
            return MapStore.First(x => x.Key == key).Value;
        }
    }

    public enum MapPartsKey
    {
        Base,
        Decor,
        Entities,
    }
}
