using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS
{
    [CreateAssetMenu(fileName = "PC Data", menuName = "KJY/Adventure OS/PCData", order = 0)]
    public class PCData : ScriptableObject
    {
        [field: SerializeField] public string CharacterName {get; private set;}
        [field: SerializeField] public Sprite CharacterSprite {get; private set;}

        public void SetName(string newName)
        {
            CharacterName = newName;
        }
    }
}