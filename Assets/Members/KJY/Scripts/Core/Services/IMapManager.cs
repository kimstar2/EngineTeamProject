using System.Collections.Generic;
using Members.JJW._01.Code;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.World;

namespace Members.KJY.Scripts.Core.Services
{
    public interface IMapManager
    {
        public bool TryGet(MapEntry mapEntry, out WorldMap worldMap);
        public void Remove(MapEntry mapEntry);
        public void Add(MapEntry mapEntry, WorldMap worldMap);
        
        public IReadOnlyDictionary<MapEntry, WorldMap> MapDict { get; }
        PlayerWindowBoundsJJW ActivePlayer { get;  }
        public void SetPlayer(PlayerWindowBoundsJJW player);
    }
}