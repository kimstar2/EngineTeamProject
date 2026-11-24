using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    [CreateAssetMenu(fileName = "Adventure Entry", menuName = "KJY/Program/Program Entry/Adventure Entry", order = 0)]
    public class AdventureOSEntry : ProgramEntry
    {
        [field: SerializeField] public MapEntry FirstMap { get; private set; }
    }
}