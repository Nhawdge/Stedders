using QuickType;
using Stedders.Components;
using Stedders.Entities;
using System.Numerics;

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
            var data = MapData.FromJson(mapFile);

            foreach (var level in data.Levels)
            {
                var cells = new List<MapCell>();
                var entities = new List<Entities.Entity>();
                var canSpawnInstance = level.FieldInstances.FirstOrDefault(x => x.Identifier == "CanSpawnEnemies");
                var scaleInstance = level.FieldInstances.FirstOrDefault(x => x.Identifier == "Scale");

                var canSpawn = canSpawnInstance?.Value.Bool ?? false;

                var scale = scaleInstance?.Value.Integer ?? 1;

                foreach (var instance in level.LayerInstances)
                {
                    if (System.Enum.TryParse<MapPartsKey>(instance.Identifier, true, out var identifier))
                    {
                        Console.WriteLine("Passed: " + identifier);

                        foreach (var tile in instance.GridTiles)
                        {
                            var pos = new Vector2(tile.Px[0], tile.Px[1]);
                            cells.Add(new MapCell(TextureKey.FreestyleRanchTileset, pos, tile.Src[0], tile.Src[1])
                            {
                                Key = identifier,
                            });
                        }
                        foreach (var entityInstance in instance.EntityInstances)
                        {
                            var pos = new Vector2(entityInstance.Px[0], entityInstance.Px[1]);
                            if (entityInstance.Identifier == "Barn")
                            {
                                entities.Add(ArchetypeGenerator.GenerateBarn(pos * scale));
                            }
                            if (entityInstance.Identifier == "Silo")
                            {
                                entities.Add(ArchetypeGenerator.GenerateSilo(pos * scale));
                            }
                            if (entityInstance.Identifier == "Player")
                            {
                                entities.Add(ArchetypeGenerator.GeneratePlayerMech(pos));
                            }
                            if (entityInstance.Identifier == "Anchor")
                            {
                                entities.Add(ArchetypeGenerator.GenerateAnchor(pos, Vector2.Zero, ""));
                            }
                        }
                    }
                }
                cells = cells.OrderBy(x => x.Key).ToList();

                MapStore.Add(level.Identifier, new Map()
                {
                    Id = level.Iid,
                    Name = level.Identifier,
                    Cells = cells,
                    EntitiesToAdd = entities,
                    CanSpawnEnemies = canSpawn,
                    MapEdges = new Vector2(level.PxWid * scale, level.PxHei * scale),
                    Scale = scale,
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
