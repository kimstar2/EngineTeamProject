using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    [CreateAssetMenu(fileName = "Advance Event", menuName = "KJY/Adventure OS/Advance Event", order = 0)]
    public class AdvanceEventSO : ScriptableObject
    {
        [field:SerializeField] public bool CanMoveNext { get; private set; }

        public void On()
        {
            Debug.Log("on");
            CanMoveNext = true;
        }
        
        public void Reset()
        {
            CanMoveNext = false;
        }
    }
}