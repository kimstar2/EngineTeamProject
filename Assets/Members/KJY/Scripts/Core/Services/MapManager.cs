using System.Collections.Generic;
using Members.JJW._01.Code;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.World;
using UnityEngine;

namespace Members.KJY.Scripts.Core.Services
{
    public class MapManager : MonoBehaviour , IMapManager
    {
        private readonly Dictionary<MapEntry, WorldMap> _mapDict = new();
        public IReadOnlyDictionary<MapEntry, WorldMap> MapDict => _mapDict;
        public PlayerWindowBoundsJJW ActivePlayer { get; private set; }

        public bool TryGet(MapEntry mapEntry, out WorldMap worldMap)
        {
            return MapDict.TryGetValue(mapEntry, out worldMap);
        }

        public void Add(MapEntry mapEntry, WorldMap worldMap)
        {
            _mapDict.Add(mapEntry,worldMap);
        }

        public void Remove(MapEntry mapEntry)
        {
            _mapDict.Remove(mapEntry);
        }

        public void SetPlayer(PlayerWindowBoundsJJW player)
        {
            ActivePlayer = player;
        }
    }
}