using Members.KJY.Scripts.World;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    [CreateAssetMenu(fileName = "Map Entry", menuName = "KJY/Program/Program Entry/Map Entry", order = 0)]
    public class MapEntry : ProgramEntry
    {
        [field:SerializeField] public WorldMap WorldMap { get; private set; }
        [field:SerializeField] public Vector2 Position { get; private set; }
        
        
    }
}